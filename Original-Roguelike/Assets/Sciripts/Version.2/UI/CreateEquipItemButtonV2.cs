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

            string query = "SELECT * FROM Inventory ORDER BY Id ASC";
            DataTable InventoryData = sqlDB.ExecuteQuery(query);

            foreach (DataRow row in InventoryData.Rows)
            {
                int itemId = Convert.ToInt32(row["Id"]);
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

                    buttonText.text = equipmentItem.ItemName;

                }
            }
        }

        private void ClearButtons()
        {
            ItemButtonV2[] existingButtons = buttonContainer.GetComponentsInChildren<ItemButtonV2>();
            foreach (ItemButtonV2 button in existingButtons)
            {
                Destroy(button.gameObject);
            }
        }

    }
}