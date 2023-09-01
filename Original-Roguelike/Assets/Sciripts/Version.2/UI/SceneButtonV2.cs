using ItemSystemV2.Inventory;
using System;
using System.IO;
using UnityEngine;
using Fade;
using UnityEngine.EventSystems;

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
            fadeSystem.SceneJump(SceneName);
        }

        public void GameEndButtonClick()
        {
            fadeSystem.GameCloseButtonClick();
        }

        public void RetryButtonClick()
        {
            SQLDBInitializationV2.PlayerDataInitialization();

            ChangeSceneButtonClick();

            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void NextStageButtonClick()
        {
            string databasePath = SQLDBInitializationV2.GetDatabasePath();
            sqlDB = new SqliteDatabase(databasePath);

            string updateStatusQuery = "UPDATE PlayerStatus SET FloorLevel = (SELECT FloorLevel FROM PlayerStatus WHERE PlayerID = 1) + 1 WHERE PlayerID = 1;";
            sqlDB.ExecuteNonQuery(updateStatusQuery);

            StartCoroutine(fadeSystem.NextStageFade());

            //ChangeSceneButtonClick();
        }

        public void DisableWindow()
        {
            gameObject.SetActive(false);
            Input.ResetInputAxes();
        }
    }
}