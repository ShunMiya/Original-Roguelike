using Fade;
using ItemSystemV2.Inventory;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameEndSystemV2
{
    public class GameEndV2 : MonoBehaviour
    {
        [SerializeField] private GameObject GameOverUI;
        [SerializeField] private GameObject GameClearUI;
        private SqliteDatabase sqlDB;
        [SerializeField]private FadeSystem fadeSystem;

        public void GameOverPerformance()
        {
            GameOverUI.SetActive(true);
            SQLDBInitializationV2.PlayerInventoryInitialization();
            EventSystem.current.SetSelectedGameObject(GameOverUI.transform.GetChild(1).gameObject);

        }

        public void GameClearPerformance()
        {
            GameClearUI.SetActive(true);

            string databasePath = SQLDBInitializationV2.GetDatabasePath();
            sqlDB = new SqliteDatabase(databasePath);

            string query = "UPDATE DungeonChallengeStatus SET Cleared = 1 WHERE DungeonId = (SELECT DungeonId FROM PlayerStatus WHERE PlayerID = 1);";
            sqlDB.ExecuteNonQuery(query);

            EventSystem.current.SetSelectedGameObject(GameClearUI.transform.GetChild(1).gameObject);

        }

        public void NextStagePerformance()
        {
            string databasePath = SQLDBInitializationV2.GetDatabasePath();
            sqlDB = new SqliteDatabase(databasePath);

            string updateStatusQuery = "UPDATE PlayerStatus SET FloorLevel = (SELECT FloorLevel FROM PlayerStatus WHERE PlayerID = 1) + 1 WHERE PlayerID = 1;";
            sqlDB.ExecuteNonQuery(updateStatusQuery);

            string query = "SELECT * FROM PlayerStatus WHERE PlayerID = 1;";
            DataTable PlayerDB = sqlDB.ExecuteQuery(query);
            int dungeonId = Convert.ToInt32(PlayerDB[0]["DungeonId"]);
            int floorLevel = Convert.ToInt32(PlayerDB[0]["FloorLevel"]);

            query = "SELECT TopFloor FROM DungeonChallengeStatus WHERE DungeonId = '" + dungeonId + "';";
            DataTable DungeonDB = sqlDB.ExecuteQuery(query);
            int topFloor = Convert.ToInt32(DungeonDB[0]["TopFloor"]);
            if (floorLevel > topFloor)
            {
                StartCoroutine(fadeSystem.GameClearFade());
                return;
            }


            StartCoroutine(fadeSystem.NextStageFade());

        }
    }
}