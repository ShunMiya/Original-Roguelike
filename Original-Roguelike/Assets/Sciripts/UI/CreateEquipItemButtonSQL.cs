using TMPro;
using UnityEngine;
using System;
using ItemSystemSQL;
using ItemSystemSQL.Inventory;

namespace UISystem
{
    public class CreateEquipItemButtonSQL : MonoBehaviour
    {
        public ItemButtonSQL buttonPrefab;
        public Transform buttonContainer;
        [SerializeField] private TextMeshProUGUI informationText;
        [SerializeField] private int totalTextLength;

        private SqliteDatabase sqlDB;

        public void Start()
        {
            string databasePath = SQLDBInitialization.GetDatabasePath();
            sqlDB = new SqliteDatabase(databasePath);
        }

        public void SetButton()
        {
            if(sqlDB == null)
            {
                string databasePath = SQLDBInitialization.GetDatabasePath();
                sqlDB = new SqliteDatabase(databasePath);
            }
            ClearButtons();

            string query = "SELECT * FROM Inventory ORDER BY Id ASC";
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