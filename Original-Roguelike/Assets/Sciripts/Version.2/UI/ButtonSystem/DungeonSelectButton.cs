using UnityEngine;
using ItemSystemV2.Inventory;
using UnityEngine.EventSystems;

namespace UISystemV2
{
    public class DungeonSelectButton : MonoBehaviour
    {
        private SqliteDatabase sqlDB;
        public int Dungeon = 0;
        public GameObject returnButton;
        private ExpandTopMenu expandTopMenu;
        private ButtonSESystem buttonSE;

        void Awake()
        {
            expandTopMenu = FindObjectOfType<ExpandTopMenu>();
            buttonSE = FindObjectOfType<ButtonSESystem>();
        }

        public void OnSelected()
        {
            buttonSE.OnSelected();
        }

        public void SelectDungeon()
        {
            buttonSE.YesSelected();

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
            buttonSE.NoSelected();
            expandTopMenu.CloseMenu();
        }
    }
}