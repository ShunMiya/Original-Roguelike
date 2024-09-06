using ItemSystemV2.Inventory;
using System;
using TMPro;
using UnityEngine;

namespace UISystemV2
{
    public class DungeonFloorBar : MonoBehaviour
    {
        private SqliteDatabase sqlDB;
        private string DungeonNameJP;
        private int TopFloor;

        [SerializeField] private TextMeshProUGUI DungeonFloorLevelText;

        void Start()
        {
            string databasePath = SQLDBInitializationV2.GetDatabasePath();
            sqlDB = new SqliteDatabase(databasePath);
            string query = "SELECT * FROM PlayerStatus WHERE PlayerID = 1;";
            DataTable Data = sqlDB.ExecuteQuery(query);
            int DungeonId = Convert.ToInt32(Data[0]["DungeonId"]);
            query = "SELECT * FROM DungeonChallengeStatus WHERE DungeonId = " + DungeonId + " ;";
            DataTable DungeonData = sqlDB.ExecuteQuery(query);
            DungeonNameJP = DungeonData[0]["DungeonNameJP"].ToString();
            TopFloor = Convert.ToInt32(DungeonData[0]["TopFloor"]);

            UpdateFloorBar();
        }

        // Update is called once per frame
        public void UpdateFloorBar()
        {
            if (sqlDB == null)
            {
                string databasePath = SQLDBInitializationV2.GetDatabasePath();
                sqlDB = new SqliteDatabase(databasePath);
            }

            string query = "SELECT * FROM PlayerStatus WHERE PlayerID = 1;";
            DataTable Data = sqlDB.ExecuteQuery(query);
            int FloorLevel = Convert.ToInt32(Data[0]["FloorLevel"]);
            DungeonFloorLevelText.text = DungeonNameJP + " " + FloorLevel + "ŠK / " + TopFloor + "ŠK";
        }
    }
}