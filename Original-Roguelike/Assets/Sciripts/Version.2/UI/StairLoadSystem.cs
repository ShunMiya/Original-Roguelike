using UnityEngine;
using ItemSystemV2.Inventory;
using System.IO;
using UnityEngine.UI;
using System;
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
        [SerializeField] private GameObject returnButton;
        private SqliteDatabase sqlDB;
        [SerializeField] private SceneButtonV2 sceneButtonV2;

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
                Load();
                sceneButtonV2.ChangeSceneButtonSceneSelect("Dungeon");
            }
            if(LoadType == 2)
            {
                Load();
                SQLDBInitializationV2.PlayerStatusInitialization();
                SQLDBInitializationV2.PlayerInventoryInitialization();
                sceneButtonV2.ChangeSceneButtonSceneSelect("Base");
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
                SelectReturnButton();
            }
        }

        public void Load()
        {
            try
            {
                File.Copy(saveDatabasePath, databasePath, true);
                Debug.Log("データベースが正常に複製されました。");

                if (sqlDB == null) sqlDB = new SqliteDatabase(saveDatabasePath);
                string updateStatusQuery = "UPDATE PlayerStatus SET FloorLevel = 0 WHERE PlayerID = 1;";
                sqlDB.ExecuteNonQuery(updateStatusQuery);

            }
            catch (IOException ex)
            {
                Debug.LogError("データベースの複製に失敗しました: " + ex.Message);
            }
        }

        public void SelectReturnButton()
        {
            //menuSE.MenuOperationSE(2);

            EventSystem.current.SetSelectedGameObject(returnButton);
            loadSystem.BackTitleMenu();
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