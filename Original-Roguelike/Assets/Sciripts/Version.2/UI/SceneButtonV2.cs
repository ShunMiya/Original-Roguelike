using ItemSystemV2.Inventory;
using System;
using System.IO;
using UnityEngine;
using Fade;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace UISystemV2
{
    public class SceneButtonV2 : MonoBehaviour
    {
        private SqliteDatabase sqlDB;
        private FadeSystem fadeSystem;
        [SerializeField] private string SceneName;

        private void Start()
        {
            fadeSystem = FindObjectOfType<FadeSystem>();
        }

        public void OnSelected()
        {
            if (EventSystem.current.currentSelectedGameObject != gameObject)
            {
                EventSystem.current.SetSelectedGameObject(gameObject);
            }
        }

        public void ChangeSceneButtonClick()
        {
            ButtonTargetReset();
            fadeSystem.SceneJump(SceneName);
        }

        public void GameEndButtonClick()
        {
            ButtonTargetReset();
            fadeSystem.GameCloseButtonClick();
        }

        public void RetryButtonClick()
        {
            ButtonTargetReset();
            SQLDBInitializationV2.PlayerDataInitialization();
            ChangeSceneButtonClick();

            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void NextStageButtonClick()
        {
            ButtonTargetReset();
            string databasePath = SQLDBInitializationV2.GetDatabasePath();
            sqlDB = new SqliteDatabase(databasePath);

            string updateStatusQuery = "UPDATE PlayerStatus SET FloorLevel = (SELECT FloorLevel FROM PlayerStatus WHERE PlayerID = 1) + 1 WHERE PlayerID = 1;";
            sqlDB.ExecuteNonQuery(updateStatusQuery);

            string query = "SELECT * FROM PlayerStatus WHERE PlayerID = 1;";
            DataTable PlayerDB = sqlDB.ExecuteQuery(query);
            string dungeonName = (string)PlayerDB[0]["DungeonName"];
            int floorLevel = Convert.ToInt32(PlayerDB[0]["FloorLevel"]);

            /*query = "SELECT TopFloor FROM " + dungeonName + " WHERE DungeonID = 1;";
            DataTable DungeonDB = sqlDB.ExecuteQuery(query);
            int topFloor = Convert.ToInt32(DungeonDB[0]["TopFloor"]);*/
            int topFloor = 2;
            if (floorLevel > topFloor)
            {
                StartCoroutine(fadeSystem.GameClearFade());
                return;
            }


            StartCoroutine(fadeSystem.NextStageFade());
        }

        public void SelectDungeon(string Dungeon)
        {
            ButtonTargetReset();
            string databasePath = SQLDBInitializationV2.GetDatabasePath();
            sqlDB = new SqliteDatabase(databasePath);

            string updateStatusQuery = "UPDATE PlayerStatus SET DungeonName = '"+Dungeon+"' WHERE PlayerID = 1;";
            sqlDB.ExecuteNonQuery(updateStatusQuery);

            updateStatusQuery = "UPDATE PlayerStatus SET FloorLevel = 1 WHERE PlayerID = 1;";
            sqlDB.ExecuteNonQuery(updateStatusQuery);

        }

        public void DisableWindow()
        {
            ButtonTargetReset();
            gameObject.SetActive(false);
            Input.ResetInputAxes();
        }

        public void ButtonTargetReset()
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
    }
}