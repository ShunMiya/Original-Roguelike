using TMPro;
using UnityEngine;
using System.IO;
using System;
using ItemSystemSQL;

namespace UISystem
{
    public class CreateEquipItemButtonSQL : MonoBehaviour
    {
        public ItemButtonSQL buttonPrefab;
        public Transform buttonContainer;
        [SerializeField] private TextMeshProUGUI informationText;

        private SqliteDatabase sqlDB;
        string query;
        private string copiedDatabasePath;

        [SerializeField] private int totalTextLength;

        public void Start()
        {
            copiedDatabasePath = Path.Combine(Application.persistentDataPath, "InventoryDataBase.db");
            sqlDB = new SqliteDatabase(copiedDatabasePath);
        }

        public void SetButton()
        {
            copiedDatabasePath = Path.Combine(Application.persistentDataPath, "InventoryDataBase.db");
            sqlDB = new SqliteDatabase(copiedDatabasePath);

            ClearButtons();

            query = "SELECT * FROM Inventory ORDER BY Id ASC";
            DataTable InventoryData = sqlDB.ExecuteQuery(query);

            foreach (DataRow row in InventoryData.Rows)
            {
                int itemId = Convert.ToInt32(row["Id"]);
                EquipmentData equipmentItem = ItemDataCache.GetEquipment(itemId);

                if (equipmentItem != null)
                {
                    ItemButtonSQL button = Instantiate(buttonPrefab, buttonContainer);
                    ItemButtonSQL itemButton = button.GetComponent<ItemButtonSQL>();
                    itemButton.row = row;
                    itemButton.informationText = informationText;
                    TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();

                    buttonText.text = equipmentItem.ItemName;

                }
            }
        }

        private void ClearButtons()
        {
            ItemButtonSQL[] existingButtons = buttonContainer.GetComponentsInChildren<ItemButtonSQL>();
            foreach (ItemButtonSQL button in existingButtons)
            {
                Destroy(button.gameObject);
            }
        }

    }
}