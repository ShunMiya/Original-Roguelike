using UnityEngine;
using ItemSystemV2.Inventory;
using UnityEngine.EventSystems;
using Performances;

namespace UISystemV2
{
    public class DungeonSelectButton : MonoBehaviour
    {
        private SqliteDatabase sqlDB;
        public int Dungeon = 0;
        public GameObject returnButton;
        private HomeMenuButton homeMenuButton;
        private MenuSoundEffect menuSE;

        void Start()
        {
            homeMenuButton = FindObjectOfType<HomeMenuButton>();
            menuSE = FindObjectOfType<MenuSoundEffect>();
        }

        public void OnSelected()
        {
            if (EventSystem.current.currentSelectedGameObject != gameObject)
            {
                EventSystem.current.SetSelectedGameObject(gameObject);
            }
            menuSE.MenuOperationSE(0);
        }

        public void SelectDungeon()
        {
            menuSE.MenuOperationSE(1);

            EventSystem.current.SetSelectedGameObject(null);
            string databasePath = SQLDBInitializationV2.GetDatabasePath();
            sqlDB = new SqliteDatabase(databasePath);

            string updateStatusQuery = "UPDATE PlayerStatus SET DungeonId = '" + Dungeon + "' WHERE PlayerID = 1;";
            sqlDB.ExecuteNonQuery(updateStatusQuery);

            updateStatusQuery = "UPDATE PlayerStatus SET FloorLevel = 1 WHERE PlayerID = 1;";
            sqlDB.ExecuteNonQuery(updateStatusQuery);

        }

        public void SelectReturnButton()
        {
            menuSE.MenuOperationSE(2);

            EventSystem.current.SetSelectedGameObject(returnButton);
            homeMenuButton.BackHomeMenu();
        }
    }
}