using ItemSystemSQL.Inventory;
using System;
using UISystem;
using UnityEngine;

namespace PlayerStatusList
{
    public class PlayerHungry : MonoBehaviour
    {
        private SqliteDatabase sqlDB;
        private PlayerHP playerHP;
        private SystemText systemText;
        float hung = 0;

        private void Start()
        {
            playerHP = GetComponent<PlayerHP>();
            string databasePath = SQLDBInitialization.GetDatabasePath();
            sqlDB = new SqliteDatabase(databasePath);
            systemText = FindObjectOfType<SystemText>();
        }

        public void HungryDecrease()
        {
            if (sqlDB == null)
            {
                string databasePath = SQLDBInitialization.GetDatabasePath();
                sqlDB = new SqliteDatabase(databasePath);
            }
            string query = "SELECT CurrentHungry FROM PlayerStatus WHERE PlayerID = 1;";
            DataTable Data = sqlDB.ExecuteQuery(query);
            int CurrentHungy = Convert.ToInt32(Data[0]["CurrentHungry"]);

            if (CurrentHungy > 0)
            {
                string updateStatusQuery = "UPDATE PlayerStatus SET CurrentHungry = (SELECT CurrentHungry FROM PlayerStatus WHERE PlayerID = 1) - " + 1 + " WHERE PlayerID = 1;";
                sqlDB.ExecuteNonQuery(updateStatusQuery);
            }
            else
            {
                hung += 0.5f;
                if (hung >= 1)
                {
                    playerHP.TakeDamage(1);
                    hung = 0;
                }
            }
        }

        public bool HealHungry(int Heal)
        {
            if (sqlDB == null)
            {
                string databasePath = SQLDBInitialization.GetDatabasePath();
                sqlDB = new SqliteDatabase(databasePath);
            }
            if (systemText == null) systemText = FindObjectOfType<SystemText>();
            string query = "SELECT * FROM PlayerStatus WHERE PlayerID = 1;";
            DataTable Data = sqlDB.ExecuteQuery(query);
            int CurrentHungry = Convert.ToInt32(Data[0]["CurrentHungry"]);
            int MaxHungry = Convert.ToInt32(Data[0]["MaxHungry"]);

            Debug.Log(CurrentHungry+":"+ MaxHungry);
            if (CurrentHungry >= MaxHungry)
            {
                systemText.TextSet("Satiety!");
                return false;
            }
            int HealHungry = CurrentHungry + Heal;
            if(HealHungry > MaxHungry) HealHungry = MaxHungry;
            string updateStatusQuery = "UPDATE PlayerStatus SET CurrentHungry = " + HealHungry + " WHERE PlayerID = 1;";
            sqlDB.ExecuteNonQuery(updateStatusQuery);

            systemText.TextSet("Player" + Heal + "HungryHeal!");

            return true;
        }
    }
}