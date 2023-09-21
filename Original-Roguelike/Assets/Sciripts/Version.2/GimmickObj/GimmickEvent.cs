using ItemSystemV2.Inventory;
using PlayerStatusSystemV2;
using System;
using System.Collections;
using UISystemV2;
using UnityEngine;

namespace Field
{
    public class GimmickEvent : MonoBehaviour
    {
        private SystemTextV2 systemText;
        private SqliteDatabase sqlDB;
        private PlayerCondition playerCondition;

        private void Start()
        {
            systemText = FindObjectOfType<SystemTextV2>();
            playerCondition = FindObjectOfType<PlayerCondition>();
        }
        
        public IEnumerator Event(int num)
        {
            if(sqlDB == null)
            {
                string databasePath = SQLDBInitializationV2.GetDatabasePath();
                sqlDB = new SqliteDatabase(databasePath);
            }
            switch (num)
            {
                case 1:
                    systemText.TextSet("FallingRockTrap!");
                    string query = "SELECT CurrentHP FROM PlayerStatus WHERE PlayerID = 1;";
                    DataTable Data = sqlDB.ExecuteQuery(query);
                    int CurrentHP = Convert.ToInt32(Data[0]["CurrentHP"]);
                    int damage = (int)(CurrentHP * 0.7f);
                    PlayerHPV2 playerHPV2 = FindAnyObjectByType<PlayerHPV2>();
                    playerHPV2.DirectDamage(damage);
                    break;
                case 2:
                    systemText.TextSet("HungryTrap!");
                    query = "SELECT CurrentHungry FROM PlayerStatus WHERE PlayerID = 1;";
                    Data = sqlDB.ExecuteQuery(query);
                    int CurrentHungry = Convert.ToInt32(Data[0]["CurrentHungry"]);
                    damage = UnityEngine.Random.Range(10, 20);
                    int newHungry = CurrentHungry - damage;
                    if (newHungry < 0) newHungry = 0;
                    string updateStatusQuery = "UPDATE PlayerStatus SET CurrentHungry = " + newHungry + " WHERE PlayerID = 1;";
                    sqlDB.ExecuteNonQuery(updateStatusQuery);
                    break;
                case 3:
                    systemText.TextSet("PoisonTrap!");
                    playerCondition.SetCondition(1, 10);
                    break;
                case 4:
                    systemText.TextSet("ConfusionTrap!");
                    playerCondition.SetCondition(2, 5);
                    break;
                case 5:
                    systemText.TextSet("StunTrap!");
                    playerCondition.SetCondition(3, 3);
                    break;
                default:
                    break;
            }

            yield return null;
        }

    }
}