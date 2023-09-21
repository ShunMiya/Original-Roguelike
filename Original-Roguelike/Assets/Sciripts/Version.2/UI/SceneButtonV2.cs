using ItemSystemV2.Inventory;
using System;
using UnityEngine;
using Fade;
using UnityEngine.EventSystems;
using GameEndSystemV2;

namespace UISystemV2
{
    public class SceneButtonV2 : MonoBehaviour
    {
        private SqliteDatabase sqlDB;
        private FadeSystem fadeSystem;
        private GameEndV2 gameEnd;
        [SerializeField] private string SceneName;

        private void Start()
        {
            fadeSystem = FindObjectOfType<FadeSystem>();
            gameEnd = FindObjectOfType<GameEndV2>();
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
            StatusReset();
            ChangeSceneButtonClick();

            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void NextStageButtonClick()
        {
            ButtonTargetReset();

            gameEnd.NextStagePerformance();
        }

        public void SelectDungeon(int Dungeon)
        {
            ButtonTargetReset();
            string databasePath = SQLDBInitializationV2.GetDatabasePath();
            sqlDB = new SqliteDatabase(databasePath);

            string updateStatusQuery = "UPDATE PlayerStatus SET DungeonId = '"+Dungeon+"' WHERE PlayerID = 1;";
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

        public void StatusReset()
        {
            SQLDBInitializationV2.PlayerStatusInitialization();
        }

        public void InventoryReset()
        {
            SQLDBInitializationV2.PlayerInventoryInitialization();
        }
    }
}