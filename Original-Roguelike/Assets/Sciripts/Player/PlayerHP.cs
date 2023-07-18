using ItemSystemSQL.Inventory;
using System;
using TMPro;
using UISystem;
using UnityEngine;

namespace PlayerStatusList
{
    public class PlayerHP : MonoBehaviour
    {
        private SqliteDatabase sqlDB;
        private SystemText systemText;
        public TextMeshProUGUI informationText;

        private void Start()
        {
            systemText = FindObjectOfType<SystemText>();
        }
        public void TakeDamage(int damage)
        {
            if (sqlDB == null)
            {
                string databasePath = SQLDBInitialization.GetDatabasePath();
                sqlDB = new SqliteDatabase(databasePath);
            }
            string query = "SELECT * FROM PlayerStatus WHERE PlayerID = 1;";
            DataTable Data = sqlDB.ExecuteQuery(query);
            int CurrentHP = Convert.ToInt32(Data[0]["CurrentHP"]);
            int Defense = Convert.ToInt32(Data[0]["Defense"]);

            int reducedDamage = damage - Defense;
            if (reducedDamage <= 0)
            {
                systemText.TextSet("PlayerNoDamage");
                return;
            }
            int newHP = CurrentHP - reducedDamage;

            string updateStatusQuery = "UPDATE PlayerStatus SET CurrentHP = " + newHP + " WHERE PlayerID = 1;";
            sqlDB.ExecuteNonQuery(updateStatusQuery);

            if (newHP <= 0)
            {
                systemText.TextSet("Player Dead!");
            }
            else if (newHP > 0)
            {
                systemText.TextSet("Player"+damage+"Damage! HP:" + newHP);
            }
        }

        public bool HealHP(int Heal)
        {
            if (sqlDB == null)
            {
                string databasePath = SQLDBInitialization.GetDatabasePath();
                sqlDB = new SqliteDatabase(databasePath);
            }
            string query = "SELECT * FROM PlayerStatus WHERE PlayerID = 1;";
            DataTable Data = sqlDB.ExecuteQuery(query);
            int CurrentHP = Convert.ToInt32(Data[0]["CurrentHP"]);
            int MaxHP = Convert.ToInt32(Data[0]["MaxHP"]);

            int HealHP = CurrentHP + Heal;
            if (HealHP > MaxHP)
            {
                Debug.Log("MaxHP");
                return false;
            }

            string updateStatusQuery = "UPDATE PlayerStatus SET CurrentHP = " + HealHP + " WHERE PlayerID = 1;";
            sqlDB.ExecuteNonQuery(updateStatusQuery);

            Debug.Log("Player" + Heal + "Heal! HP:" + HealHP);

            return true;
        }
    }
}