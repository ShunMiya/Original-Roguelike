using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using ItemSystemV2;
using ItemSystemV2.Inventory;

namespace UISystemV2
{
    public class CreateItemButtonV2 : MonoBehaviour
    {
        public ItemButtonV2 buttonPrefab;
        public Transform buttonContainer;
        [SerializeField] private TextMeshProUGUI informationText;
        [SerializeField] private GameObject MenuButton;

        private SqliteDatabase sqlDB;
        string query;

        [SerializeField] private int totalTextLength;

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

                if (consumableItem != null)
                {
                    int itemStock = Convert.ToInt32(row["Num"]);
                    ItemButtonV2 button = Instantiate(buttonPrefab, buttonContainer);
                    ItemButtonV2 itemButton = button.GetComponent<ItemButtonV2>();
                    itemButton.row = row;
                    itemButton.informationText = informationText;
                    TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();

                    buttonText.text = FormatItemText(consumableItem.ItemName, itemStock);

                }
                if (equipmentItem != null)
                {
                    ItemButtonV2 button = Instantiate(buttonPrefab, buttonContainer);
                    ItemButtonV2 itemButton = button.GetComponent<ItemButtonV2>();
                    itemButton.row = row;
                    itemButton.informationText = informationText;
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

        private string FormatItemText(string itemName, int itemStack)
        {
            string itemText = $"{itemName}Å~{itemStack}";
            int spacesToAdd = totalTextLength - itemText.Length;

            if (spacesToAdd > 0)
            {
                string spaceText = new string(' ', spacesToAdd);
                itemText = $"{itemName}{spaceText}Å~{itemStack}";
            }

            return itemText;
        }

        public void SetButtonTextAfterUse(ItemButtonV2 button)
        {
            string itemName = button.row["ItemName"].ToString();
            int ItemStock = Convert.ToInt32(button.row["Num"]);
            TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = FormatItemText(itemName, ItemStock);
        }

        public void SelectButtonChangeForDestruction(ItemButtonV2 button)
        {
            ItemButtonV2[] existingButtons = buttonContainer.GetComponentsInChildren<ItemButtonV2>();
            EventSystem eventSystem = EventSystem.current;

            if (existingButtons.Length > 1)
            {
                GameObject currentSelectedObject = eventSystem.currentSelectedGameObject;

                GameObject nextSelectedObject = null;
                for (int i = 0; i < existingButtons.Length; i++)
                {
                    if (existingButtons[i].gameObject == currentSelectedObject)
                    {
                        if (i < existingButtons.Length - 1)
                        {
                            nextSelectedObject = existingButtons[i + 1].gameObject;
                        }
                        else
                        {
                            nextSelectedObject = existingButtons[i - 1].gameObject;
                        }
                        break;
                    }
                }

                if (nextSelectedObject == null)
                {
                    nextSelectedObject = existingButtons[existingButtons.Length - 1].gameObject;
                }
                eventSystem.SetSelectedGameObject(nextSelectedObject);
            }
            else
            {
                EventSystem.current.SetSelectedGameObject(MenuButton);
            }
            Destroy(button.gameObject);
            return;
        }
    }
}