using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using System.IO;
using System;
using ItemSystemSQL;

namespace UISystem
{
    public class CreateItemButtonSQL : MonoBehaviour
    {
        public ItemButtonSQL buttonPrefab;
        public Transform buttonContainer;
        [SerializeField] private TextMeshProUGUI informationText;
        [SerializeField] private GameObject MenuButton;

        private SqliteDatabase sqlDB;
        string query;
        private string copiedDatabasePath;

        [SerializeField] private int totalTextLength;

        public void Awake()
        {
            copiedDatabasePath = Path.Combine(Application.persistentDataPath, "InventoryDataBase.db");
            sqlDB = new SqliteDatabase(copiedDatabasePath);
        }

        public void SetButton()
        {
            ClearButtons();

            query = "SELECT * FROM Inventory ORDER BY Id ASC";
            DataTable InventoryData = sqlDB.ExecuteQuery(query);

            foreach (DataRow row in InventoryData.Rows)
            {
                int itemId = Convert.ToInt32(row["Id"]);
                ConsumableData consumableItem = ItemDataCache.GetConsumable(itemId);
                EquipmentData equipmentItem = ItemDataCache.GetEquipment(itemId);

                if (consumableItem != null)
                {
                    int itemStock = Convert.ToInt32(row["Num"]);
                    ItemButtonSQL button = Instantiate(buttonPrefab, buttonContainer);
                    ItemButtonSQL itemButton = button.GetComponent<ItemButtonSQL>();
                    itemButton.row = row;
                    itemButton.informationText = informationText;
                    TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();

                    buttonText.text = FormatItemText(consumableItem.ItemName, itemStock);

                }
                if(equipmentItem != null)
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

        public void SetButtonTextAfterUse(ItemButtonSQL button)
        {
            string itemName = button.row["ItemName"].ToString();
            int ItemStock = Convert.ToInt32(button.row["Num"]);
            TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = FormatItemText(itemName, ItemStock);
            Debug.Log("èëÇ´ä∑Ç¶äÆóπ");
//ï€åØ            if (ItemStock == 0) SelectButtonChangeForDestruction(button);
        }

        public void SelectButtonChangeForDestruction(ItemButtonSQL button)
        {
            ItemButtonSQL[] existingButtons = buttonContainer.GetComponentsInChildren<ItemButtonSQL>();
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