using TMPro;
using UnityEngine;
using System;
using ItemSystemV2.Inventory;
using UISystemV2;
using UnityEngine.EventSystems;

namespace HomeSystem
{
    public class CreateDungeonButton : MonoBehaviour
    {
        public DungeonSelectButton buttonPrefab;
        public Transform buttonContainer;
        [SerializeField] private TextMeshProUGUI informationText;
        [SerializeField] private GameObject returnButton;
        [SerializeField] private SubMenu subMenu;

        private SqliteDatabase sqlDB;
        string query;
        [SerializeField] private int totalTextLength;

        // Start is called before the first frame update
        void Start()
        {
            string databasePath = SQLDBInitializationV2.GetDatabasePath();
            sqlDB = new SqliteDatabase(databasePath);
        }

        public void SetButton()
        {
            if (sqlDB == null)
            {
                string databasePath = SQLDBInitializationV2.GetDatabasePath();
                sqlDB = new SqliteDatabase(databasePath);
            }
            query = "SELECT MAX(DungeonId) AS MaxDungeonId FROM DungeonChallengeStatus WHERE Cleared = 1";
            DataTable MaxClearDungeonIdTable = sqlDB.ExecuteQuery(query);
            
            int MaxClearDungeonId = Convert.ToInt32(MaxClearDungeonIdTable[0]["MaxDungeonId"]);
            Debug.Log("最新クリアダンジョンId"+MaxClearDungeonId);

            query = "SELECT MAX(DungeonId) AS MaxDungeonId FROM DungeonChallengeStatus";
            DataTable MaxDungeonIdTable = sqlDB.ExecuteQuery(query);
            int MaxDungeonId = Convert.ToInt32(MaxDungeonIdTable[0]["MaxDungeonId"]);
            Debug.Log(MaxClearDungeonId + " : " + MaxDungeonId);
            if(MaxClearDungeonId < MaxDungeonId)  MaxClearDungeonId++;

            for (int DungeonId = MaxClearDungeonId; DungeonId > 0; DungeonId--)
            {
                query = "SELECT DungeonNameJP FROM DungeonChallengeStatus WHERE DungeonId ="+DungeonId;
                DataTable DungeonName = sqlDB.ExecuteQuery(query);

                DungeonSelectButton button = Instantiate(buttonPrefab, buttonContainer);
                DungeonSelectButton DungeonSelectButton = button.GetComponent<DungeonSelectButton>();
                DungeonSelectButton.Dungeon = DungeonId;
                DungeonSelectButton.returnButton = returnButton;

                TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
                buttonText.text = DungeonName[0]["DungeonNameJP"].ToString();
            }
        }

        public void OnDisable()
        {
            ClearButtons();
        }

        private void ClearButtons()
        {
            SceneButtonV2[] existingButtons = buttonContainer.GetComponentsInChildren<SceneButtonV2>();
            foreach (SceneButtonV2 button in existingButtons)
            {
                Destroy(button.gameObject);
            }
        }
    }
}
