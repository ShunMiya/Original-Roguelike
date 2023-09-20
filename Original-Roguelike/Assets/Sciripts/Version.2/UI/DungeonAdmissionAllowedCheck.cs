using ItemSystemV2.Inventory;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Base
{
    public class DungeonAdmissionAllowedCheck : MonoBehaviour
    {
        [SerializeField] private Button ForestButton;
        [SerializeField] private Button MountainButton;
        private SqliteDatabase sqlDB;

        public void Start()
        {
            string databasePath = SQLDBInitializationV2.GetDatabasePath();
            sqlDB = new SqliteDatabase(databasePath);

            string query = "SELECT Cleared FROM DungeonChallengeStatus WHERE DungeonId = 1;)";
            DataTable Data = sqlDB.ExecuteQuery(query);
            int Cleared = Convert.ToInt32(Data[0]["Cleared"]);
            if (Cleared == 1)
            {
                ForestButton.gameObject.SetActive(true);
            }

             query = "SELECT Cleared FROM DungeonChallengeStatus WHERE DungeonId = 2;)";
             Data = sqlDB.ExecuteQuery(query);
             Cleared = Convert.ToInt32(Data[0]["Cleared"]);
            if (Cleared == 1)
            {
                MountainButton.gameObject.SetActive(true);
            }

        }

    }
}