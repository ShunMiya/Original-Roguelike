using ItemSystemV2.Inventory;
using System.Collections;
using System.Collections.Generic;
using UISystemV2;
using UnityEngine;

namespace PlayerStatusSystemV2
{
    public class PlayerLevel : MonoBehaviour
    {
        private PlayerStatusV2 playerStatus;
        private SystemTextV2 systemText;
        private SqliteDatabase sqlDB;

        public void Start ()
        {
            playerStatus = GetComponent<PlayerStatusV2>();
            systemText = FindObjectOfType<SystemTextV2>();

            string databasePath = SQLDBInitializationV2.GetDatabasePath();
            sqlDB = new SqliteDatabase(databasePath);
        }

        public void PlayerGetExp(int Exp)
        {
            systemText.TextSet("Get"+Exp+"Exp!");

            string updateStatusQuery = "UPDATE PlayerStatus SET PlayerExp = (SELECT PlayerExp FROM PlayerStatus WHERE PlayerID = 1) + " + Exp + " WHERE PlayerID = 1;";
            sqlDB.ExecuteNonQuery(updateStatusQuery);
        }
    }
}