using UnityEngine;
using ItemSystemV2.Inventory;
using System.IO;
using UISystemV2;
using UnityEngine.EventSystems;
using TMPro;

namespace SaveLoad
{
    public class StairLoadSystem : MonoBehaviour
    {
        private string databasePath;
        private string saveDatabasePath;
        [SerializeField] private TextMeshProUGUI StairLoadText;
        [SerializeField] private GameObject StairLoadButton;
        private SqliteDatabase sqlDB;
        [SerializeField] private SceneButtonV2 sceneButtonV2;
        [SerializeField] private ExpandTopMenu expandTopMenu;

        [SerializeField] private LoadSystem loadSystem;
        private int LoadType;

        // Start is called before the first frame update
        void Start()
        {
            databasePath = SQLDBInitializationV2.GetDatabasePath();
            saveDatabasePath = Path.Combine(Application.persistentDataPath, "SaveDataBase01.db");
            sqlDB = new SqliteDatabase(saveDatabasePath);
            LoadType = 1;
        }
        public void YesButtonClick()
        {
            if(LoadType == 1)
            {
                loadSystem.Load();
                DataAdjustment();
                sceneButtonV2.ChangeSceneButtonClick("Dungeon");
            }
            if(LoadType == 2)
            {
                loadSystem.Load();
                DataAdjustment();
                SQLDBInitializationV2.PlayerStatusInitialization();
                SQLDBInitializationV2.PlayerInventoryInitialization();
                sceneButtonV2.ChangeSceneButtonClick("Base");
            }
        }

        public void NoButtonClick()
        {
            if(LoadType == 1)
            {
                LoadType = 2;
                SetText();
                EventSystem.current.SetSelectedGameObject(StairLoadButton);
                return;
            }
            if (LoadType == 2)
            {
                LoadType = 1;
                SetText();
                expandTopMenu.CloseMenu();
            }
        }

        public void DataAdjustment()
        {
            if (sqlDB == null) sqlDB = new SqliteDatabase(saveDatabasePath);
            string updateStatusQuery = "UPDATE PlayerStatus SET FloorLevel = 0 WHERE PlayerID = 1;";
            sqlDB.ExecuteNonQuery(updateStatusQuery);
        }

        public void SetText()
        {
            switch(LoadType)
            {
                case 1:
                    StairLoadText.text =
                        "ダンジョン探索途中の\nデータがあります。\n続きから再開しますか。";
                    break;
                case 2:
                    StairLoadText.text =
                        "探索を諦め\n拠点から再開しますか。\nアイテムは装備中アイテム\n以外消滅します。";
                        break;
            }
        }
    }
}