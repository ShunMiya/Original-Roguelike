using TMPro;
using UnityEngine;
using System;
using ItemSystemV2;
using ItemSystemV2.Inventory;
using UnityEngine.EventSystems;
using System.Collections;

namespace UISystemV2
{
    public class CreateItemButtonV2 : MonoBehaviour
    {
        public ItemButtonV2 buttonPrefab;
        public Transform buttonContainer;
        [SerializeField] private TextMeshProUGUI informationText;
        [SerializeField] private TextMeshProUGUI itemCountText;
        [SerializeField] private GameObject returnButton;
        [SerializeField] private SubMenu subMenu;

        private SqliteDatabase sqlDB;
        string query;
        [SerializeField] private ButtonNavigation buttonNavi;
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
            query = "SELECT * FROM Inventory ORDER BY Id ASC";
            DataTable InventoryData = sqlDB.ExecuteQuery(query);

            foreach (DataRow row in InventoryData.Rows)
            {
                int itemId = Convert.ToInt32(row["Id"]);
                IItemDataV2 itemData = ItemDataCacheV2.GetIItemData(itemId);

                switch(itemData.ItemType)
                {
                    case 0:
                        ConsumableDataV2 consumableItem = ItemDataCacheV2.GetConsumable(itemId);

                        ItemButtonV2 button = Instantiate(buttonPrefab, buttonContainer);
                        ItemButtonV2 itemButton = button.GetComponent<ItemButtonV2>();
                        itemButton.row = row;
                        itemButton.informationText = informationText;
                        itemButton.returnButton = returnButton;
                        itemButton.subMenu = subMenu;
                        TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();

                        buttonText.text = consumableItem.ItemName;
                        break;
                    case 1:
                        EquipmentDataV2 equipmentItem = ItemDataCacheV2.GetEquipment(itemId);

                        button = Instantiate(buttonPrefab, buttonContainer);
                        itemButton = button.GetComponent<ItemButtonV2>();
                        itemButton.row = row;
                        itemButton.informationText = informationText;
                        itemButton.returnButton = returnButton;
                        itemButton.subMenu = subMenu;

                        buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
                        if (Convert.ToInt32(row["Equipped"]) != 0)
                        {
                            buttonText.text = FormatEquippedItemText(equipmentItem.ItemName);
                            continue;
                        }
                        buttonText.text = equipmentItem.ItemName;
                        break;
                    case 2:
                        OffensiveDataV2 offensiveItem = ItemDataCacheV2.GetOffensive(itemId);

                        button = Instantiate(buttonPrefab, buttonContainer);
                        itemButton = button.GetComponent<ItemButtonV2>();
                        itemButton.row = row;
                        itemButton.informationText = informationText;
                        itemButton.returnButton = returnButton;
                        itemButton.subMenu = subMenu;
                        buttonText = button.GetComponentInChildren<TextMeshProUGUI>();

                        buttonText.text = offensiveItem.ItemName + "(" + Convert.ToInt32(row["Num"]) + ")";
                        break;
                }
            }
            ItemCountText();

            GetComponent<RectTransform>().anchoredPosition = new Vector2 (0, 0);

            if (gameObject.transform.childCount > 0) buttonNavi.SetItemMenuButtonNavigation();
        }

        public void OnDisable()
        {
            buttonNavi.ResetNavigation();

            ClearButtons();
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

        private void ItemCountText()
        {
            string query = "SELECT COUNT(*) AS TotalCount FROM Inventory";
            DataTable result = sqlDB.ExecuteQuery(query);
            object value = result.Rows[0]["TotalCount"];
            int itemCount = Convert.ToInt32(value);

            query = "SELECT InventorySize FROM PlayerStatus WHERE PlayerID = 1;";
            DataTable Data = sqlDB.ExecuteQuery(query);
            int Size = Convert.ToInt32(Data[0]["InventorySize"]);

            itemCountText.text = "èäéùêî\t" + itemCount + " / " + Size;
        }
    }
}