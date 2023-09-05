using ItemSystemV2.Inventory;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameEndSystemV2
{
    public class GameEndV2 : MonoBehaviour
    {
        [SerializeField] private GameObject GameOverUI;
        [SerializeField] private GameObject GameClearUI;
        private SqliteDatabase sqlDB;


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
    }
}