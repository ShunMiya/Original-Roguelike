using UnityEngine;
using ItemSystemV2.Inventory;
using System.IO;
using UnityEngine.UI;
using System;
using UISystemV2;
using UnityEngine.EventSystems;

namespace SaveLoad
{
    public class LoadSystem : MonoBehaviour
    {
        private string databasePath;
        private string saveDatabasePath;
        [SerializeField] private GameObject StairLoadMessage;
        [SerializeField] private GameObject StairLoadButton;
        private Button loadButton;
        private SqliteDatabase sqlDB;
        string query;
        [SerializeField] private SceneButtonV2 sceneButtonV2;

        // Start is called before the first frame update
        void Start()
        {
            databasePath = SQLDBInitializationV2.GetDatabasePath();
            saveDatabasePath = Path.Combine(Application.persistentDataPath, "SaveDataBase01.db");
            loadButton = GetComponent<Button>();
            sqlDB = new SqliteDatabase(saveDatabasePath);

            if (File.Exists(saveDatabasePath))  loadButton.interactable = true;
        }

        public void Load()
        {
            try
            {
                File.Copy(saveDatabasePath, databasePath, true);
                Debug.Log("データベースが正常に複製されました。");
            }
            catch (IOException ex)
            {
                Debug.LogError("データベースの複製に失敗しました: " + ex.Message);
            }
        }

        public void LoadTypeCheck()
        {
            if (sqlDB == null) sqlDB = new SqliteDatabase(saveDatabasePath);
            query = "SELECT * FROM PlayerStatus WHERE PlayerID = 1";
            DataTable Data = sqlDB.ExecuteQuery(query);
            int CurrentDungeonId = Convert.ToInt32(Data[0]["DungeonId"]);
            int CurrentFloorLevel = Convert.ToInt32(Data[0]["FloorLevel"]);

            if (CurrentFloorLevel == 0)
            {
                illegalload();
                sceneButtonV2.ChangeSceneButtonSceneSelect("Base");
                return;
            }

            if (CurrentDungeonId == 0)
            {
                Load();
                sceneButtonV2.ChangeSceneButtonSceneSelect("Base");
                return;
            }

            StairLoadMessage.SetActive(true);
            EventSystemSet();
        }

        public void EventSystemSet()
        {
            gameObject.transform.parent.GetComponent<CanvasGroup>().interactable = false;
            EventSystem.current.SetSelectedGameObject(StairLoadButton);
        }

        public void BackTitleMenu()
        {
            gameObject.transform.parent.GetComponent<CanvasGroup>().interactable = true;
            StairLoadMessage.SetActive(false);
        }

        public void illegalload()
        {
            Load();
            SQLDBInitializationV2.PlayerStatusInitialization();
            SQLDBInitializationV2.PlayerInventoryInitialization();
        }
    }
}