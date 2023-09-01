using ItemSystemV2.Inventory;
using System;
using UISystemV2;
using UnityEngine;

namespace PlayerStatusSystemV2
{
    public class PlayerHungryV2 : MonoBehaviour
    {
        private SqliteDatabase sqlDB;
        private PlayerHPV2 playerHP;
        private SystemTextV2 systemText;
        float hung = 0;

        private void Start()
        {
            playerHP = GetComponent<PlayerHPV2>();
            string databasePath = SQLDBInitializationV2.GetDatabasePath();
            sqlDB = new SqliteDatabase(databasePath);
            systemText = FindObjectOfType<SystemTextV2>();
        }

        public void HungryDecrease()
        {
            if (sqlDB == null)
            {
                string databasePath = SQLDBInitializationV2.GetDatabasePath();
                sqlDB = new SqliteDatabase(databasePath);
            }
            string query = "SELECT CurrentHungry FROM PlayerStatus WHERE PlayerID = 1;";
            DataTable Data = sqlDB.ExecuteQuery(query);
            int CurrentHungy = Convert.ToInt32(Data[0]["CurrentHungry"]);

            if (CurrentHungy > 0)
            {
                hung++;
                if (hung >= 4)
                {
                    string updateStatusQuery = "UPDATE PlayerStatus SET CurrentHungry = (SELECT CurrentHungry FROM PlayerStatus WHERE PlayerID = 1) - " + 1 + " WHERE PlayerID = 1;";
                    sqlDB.ExecuteNonQuery(updateStatusQuery);
                    hung = 0;
                }
            }
            else
            {
                hung++;
                if (hung >= 2)
                {
                    playerHP.DirectDamage(1,(int)transform.rotation.y);

                    hung = 0;
                }
            }
        }

        public bool HealHungry(int Heal)
        {
            if (sqlDB == null)
            {
                string databasePath = SQLDBInitializationV2.GetDatabasePath();
                sqlDB = new SqliteDatabase(databasePath);
            }
            if (systemText == null) systemText = FindObjectOfType<SystemTextV2>();
            string query = "SELECT * FROM PlayerStatus WHERE PlayerID = 1;";
            DataTable Data = sqlDB.ExecuteQuery(query);
            int CurrentHungry = Convert.ToInt32(Data[0]["CurrentHungry"]);
            int MaxHungry = Convert.ToInt32(Data[0]["MaxHungry"]);

            Debug.Log(CurrentHungry + ":" + MaxHungry);
            if (CurrentHungry >= MaxHungry)
            {
                systemText.TextSet("Satiety!");
                return false;
            }
            int HealHungry = CurrentHungry + Heal;
            if (HealHungry > MaxHungry) HealHungry = MaxHungry;
            string updateStatusQuery = "UPDATE PlayerStatus SET CurrentHungry = " + HealHungry + " WHERE PlayerID = 1;";
            sqlDB.ExecuteNonQuery(updateStatusQuery);

            systemText.TextSet("Player" + Heal + "HungryHeal!");

            return true;
        }
    }
}