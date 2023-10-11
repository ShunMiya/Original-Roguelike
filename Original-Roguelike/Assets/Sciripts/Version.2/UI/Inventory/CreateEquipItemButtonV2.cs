using TMPro;
using UnityEngine;
using System;
using ItemSystemV2;
using ItemSystemV2.Inventory;

namespace UISystemV2
{
    public class CreateEquipItemButtonV2 : MonoBehaviour
    {
        public ItemButtonV2 buttonPrefab;
        public Transform buttonContainer;
        [SerializeField] private TextMeshProUGUI informationText;
        [SerializeField] private GameObject returnButton;
        [SerializeField] private SubMenu subMenu;
        [SerializeField] private Transform EquipArea;
        [SerializeField] private int totalTextLength;

        private SqliteDatabase sqlDB;
        string query;

        public void Start()
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
            ClearButtons();

            query = "SELECT * FROM Inventory ORDER BY Id ASC";
            DataTable InventoryData = sqlDB.ExecuteQuery(query);

            foreach (DataRow row in InventoryData.Rows)
            {
                int itemId = Convert.ToInt32(row["Id"]);
                ConsumableDataV2 consumableItem = ItemDataCacheV2.GetConsumable(itemId);
                EquipmentDataV2 equipmentItem = ItemDataCacheV2.GetEquipment(itemId);

                if (equipmentItem != null)
                {
                    ItemButtonV2 button = Instantiate(buttonPrefab, buttonContainer);
                    ItemButtonV2 itemButton = button.GetComponent<ItemButtonV2>();
                    itemButton.row = row;
                    itemButton.informationText = informationText;
                    itemButton.returnButton = returnButton;
                    itemButton.subMenu = subMenu;
                    itemButton.EquipArea = EquipArea;
                    
                    TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
                    if (Convert.ToInt32(row["Equipped"]) != 0)
                    {
                        buttonText.text = FormatEquippedItemText(equipmentItem.ItemName);
                        continue;
                    }

                    buttonText.text = equipmentItem.ItemName;
                }
            }
            GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        }

        private void ClearButtons()
        {
            ItemButtonV2[] existingButtons = buttonContainer.GetComponentsInChildren<ItemButtonV2>();
            foreach (ItemButtonV2 button in existingButtons)
            {
                Destroy(button.gameObject);
            }
        }

        private string FormatEquippedItemText(string itemName)
        {
            string E = "E: ";
            string itemText = $"{E}{itemName}";

            return itemText;
        }
    }
}