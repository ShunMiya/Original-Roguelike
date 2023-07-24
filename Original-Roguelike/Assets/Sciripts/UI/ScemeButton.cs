using ItemSystemSQL.Inventory;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UISystem
{
    public class ScemeButton : MonoBehaviour
    {
        private SqliteDatabase sqlDB;

        public void BackTitleButtonClick()
        {
            SceneManager.LoadScene("Title");
        }

        public void GameStartButtonClick()
        {
            SceneManager.LoadScene("Main");
        }

        public void GameEndButtonClick()
        {
            Application.Quit();
        }

        public void RetryButtonClick()
        {
            SQLDBInitialization.PlayerDataInitialization();

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void NextStageButtonClick()
        {
            string databasePath = SQLDBInitialization.GetDatabasePath();
            sqlDB = new SqliteDatabase(databasePath);

            string updateStatusQuery = "UPDATE PlayerStatus SET FloorLevel = (SELECT FloorLevel FROM PlayerStatus WHERE PlayerID = 1) + 1 WHERE PlayerID = 1;";
            sqlDB.ExecuteNonQuery(updateStatusQuery);

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}