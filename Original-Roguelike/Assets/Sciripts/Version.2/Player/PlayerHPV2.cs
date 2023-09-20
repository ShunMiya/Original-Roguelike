using GameEndSystemV2;
using ItemSystemV2.Inventory;
using System;
using UISystemV2;
using UnityEngine;

namespace PlayerStatusSystemV2
{
    public class PlayerHPV2 : MonoBehaviour
    {
        private SqliteDatabase sqlDB;
        private SystemTextV2 systemText;
        private GameEndV2 gameEnd;
        private float Recovery = 0;

        private void Start()
        {
            systemText = FindObjectOfType<SystemTextV2>();
            gameEnd = FindObjectOfType<GameEndV2>();
        }
        public void TakeDamage(int damage, int R, float HitRate)
        {
            #region ñΩíÜó¶èàóù
            int HitCheck = UnityEngine.Random.Range(1, 101);
            if(HitCheck > HitRate)
            {
                systemText.TextSet("NoHit!");
                return;
            }
            #endregion

            if (sqlDB == null)
            {
                string databasePath = SQLDBInitializationV2.GetDatabasePath();
                sqlDB = new SqliteDatabase(databasePath);
            }
            string query = "SELECT * FROM PlayerStatus WHERE PlayerID = 1;";
            DataTable Data = sqlDB.ExecuteQuery(query);
            int CurrentHP = Convert.ToInt32(Data[0]["CurrentHP"]);
            int Defense = Convert.ToInt32(Data[0]["Defense"]);

            #region âÒîó¶èàóù
            int EvasionCheck = UnityEngine.Random.Range(1, 101);
            if (EvasionCheck < GameRule.EvasionRate)
            {
                systemText.TextSet("NoHit!");
                return;
            }
            #endregion

            #region É_ÉÅÅ[ÉWåàíËèàóù
            float damageModifier = UnityEngine.Random.Range(0.85f, 1.0f);
            int ModifierDamage = Mathf.RoundToInt(damage * damageModifier);

            int reducedDamage = Mathf.CeilToInt(ModifierDamage * Mathf.Pow(GameRule.DamageIndexValue, Defense));
            #endregion

            int newHP = CurrentHP - reducedDamage;

            string updateStatusQuery = "UPDATE PlayerStatus SET CurrentHP = " + newHP + " WHERE PlayerID = 1;";
            sqlDB.ExecuteNonQuery(updateStatusQuery);

            if (newHP <= 0)
            {
                systemText.TextSet("Player Dead!");
                gameEnd.GameOverPerformance();
                Time.timeScale = 0;

            }
            else if (newHP > 0)
            {
                systemText.TextSet("Player" + reducedDamage + "Damage! HP:" + newHP);
                int Rota = DirUtil.ReverseDirection(R);
                transform.rotation = Quaternion.Euler(0, Rota, 0);
            }
        }

        public void DirectDamage(int damage)
        {
            if (sqlDB == null)
            {
                string databasePath = SQLDBInitializationV2.GetDatabasePath();
                sqlDB = new SqliteDatabase(databasePath);
            }
            string updateStatusQuery = "UPDATE PlayerStatus SET CurrentHP = (SELECT CurrentHP FROM PlayerStatus WHERE PlayerID = 1) - " + damage + " WHERE PlayerID = 1;";
            sqlDB.ExecuteNonQuery(updateStatusQuery);

            string query = "SELECT CurrentHP FROM PlayerStatus WHERE PlayerID = 1;";
            DataTable Data = sqlDB.ExecuteQuery(query);
            int CurrentHP = Convert.ToInt32(Data[0]["CurrentHP"]);

            if (CurrentHP <= 0)
            {
                systemText.TextSet("Player Dead!");
                gameEnd.GameOverPerformance();
                Time.timeScale = 0;
            }
        }

        public bool HealHP(int Heal)
        {
            if (sqlDB == null)
            {
                string databasePath = SQLDBInitializationV2.GetDatabasePath();
                sqlDB = new SqliteDatabase(databasePath);
            }
            if (systemText == null) systemText = FindObjectOfType<SystemTextV2>();
            string query = "SELECT * FROM PlayerStatus WHERE PlayerID = 1;";
            DataTable Data = sqlDB.ExecuteQuery(query);
            int CurrentHP = Convert.ToInt32(Data[0]["CurrentHP"]);
            int MaxHP = Convert.ToInt32(Data[0]["MaxHP"]);

            int HealHP = CurrentHP + Heal;
            if (HealHP > MaxHP)
            {
                systemText.TextSet("MaxHP!");
                return false;
            }

            string updateStatusQuery = "UPDATE PlayerStatus SET CurrentHP = " + HealHP + " WHERE PlayerID = 1;";
            sqlDB.ExecuteNonQuery(updateStatusQuery);

            systemText.TextSet("Player" + Heal + "Heal! HP:" + HealHP);

            return true;
        }

        public void TurnRecoveryHp()
        {
            if (sqlDB == null)
            {
                string databasePath = SQLDBInitializationV2.GetDatabasePath();
                sqlDB = new SqliteDatabase(databasePath);
            }
            if (systemText == null) systemText = FindObjectOfType<SystemTextV2>();
            string query = "SELECT * FROM PlayerStatus WHERE PlayerID = 1;";
            DataTable Data = sqlDB.ExecuteQuery(query);

            if (Convert.ToInt32(Data[0]["CurrentHungry"]) == 0) return;

            int CurrentHP = Convert.ToInt32(Data[0]["CurrentHP"]);
            int MaxHP = Convert.ToInt32(Data[0]["MaxHP"]);

            if (CurrentHP < MaxHP)
            {
                Recovery++;
                if (Recovery >= 2)
                {
                    string updateStatusQuery = "UPDATE PlayerStatus SET CurrentHP = (SELECT CurrentHP FROM PlayerStatus WHERE PlayerID = 1) + " + 1 + " WHERE PlayerID = 1;";
                    sqlDB.ExecuteNonQuery(updateStatusQuery);
                    Recovery = 0;
                }

            }
        }
    }
}