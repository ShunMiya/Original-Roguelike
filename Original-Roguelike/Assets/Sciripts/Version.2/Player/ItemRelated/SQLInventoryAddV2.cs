using System;
using UISystemV2;
using UnityEngine;

namespace ItemSystemV2.Inventory
{
    public class SQLInventoryAddV2 : MonoBehaviour
    {
        private SqliteDatabase sqlDB;
        private SystemTextV2 systemText;
        int inventorySize;
        int itemCount;

        public void Start()
        {
            string databasePath = SQLDBInitializationV2.GetDatabasePath();
            sqlDB = new SqliteDatabase(databasePath);
            systemText = FindObjectOfType<SystemTextV2>();
        }

        public void inventorySizeSet(int inventorysize)
        {
            Debug.Log("SQLinventorySize" + inventorySize + "を" + inventorysize + "に変更");
            inventorySize = inventorysize;
        }

        public bool AddItem(int itemId, int num)
        {
            itemCount = InventoryCount();

            IItemDataV2 ItemData = ItemDataCacheV2.GetIItemData(itemId);

            bool GetItem = false;

            switch (ItemData.ItemType)
            {
                case 0:
                    ConsumableDataV2 consumableItem = ItemDataCacheV2.GetConsumable(itemId);
                    GetItem = AddConsumable(itemId, consumableItem);
                    break;
                case 1:
                    EquipmentDataV2 equipmentItem = ItemDataCacheV2.GetEquipment(itemId);
                    GetItem = AddEquipment(itemId, num, equipmentItem);
                    break; 
                case 2:
                    OffensiveDataV2 offensiveItem = ItemDataCacheV2.GetOffensive(itemId);
                    GetItem = AddOffensive(itemId, num, offensiveItem);
                    break;
                default:
                    break;
            }
            return GetItem;
        }

        public bool AddConsumable(int itemId, ConsumableDataV2 consumableItem)
        {
            if (itemCount == inventorySize)
            {
                systemText.TextSet("バッグが一杯だ！");
                return false;
            }
            string insertQuery = "INSERT INTO Inventory (Id, Num) VALUES ('" + consumableItem.Id + "', 1)";
            systemText.TextSet(consumableItem.ItemName + "を手に入れた");            
            sqlDB.ExecuteNonQuery(insertQuery);
            return true;
        }

        public bool AddEquipment(int itemId, int num, EquipmentDataV2 equipmentItem)
        {
            if (itemCount == inventorySize)
            {
                systemText.TextSet("バッグが一杯だ！");
                return false;
            }
            string insertQuery = "INSERT INTO Inventory (Id, Num) VALUES ('" + equipmentItem.Id + "', " + num + ")";
            systemText.TextSet(equipmentItem.ItemName + " を手に入れた");
            sqlDB.ExecuteNonQuery(insertQuery);
            return true;
        }

        public bool AddOffensive(int itemId, int num, OffensiveDataV2 offensiveItem)
        {
            string query = "SELECT * FROM Inventory WHERE Id = " + itemId;
            DataTable itemsData = sqlDB.ExecuteQuery(query);

            foreach (DataRow row in itemsData.Rows)
            {
                int currentNum = Convert.ToInt32(row["Num"]);
                int totalStock = currentNum + num;
                if (totalStock <= offensiveItem.MaxStock)
                {
                    string updateQuery = "UPDATE Inventory SET Num = " + totalStock + " WHERE IID = " + row["IID"];
                    sqlDB.ExecuteNonQuery(updateQuery);

                    systemText.TextSet(offensiveItem.ItemName + " (" + num + ") を手に入れた");

                    return true;
                }
            }
            if (itemCount == inventorySize)
            {
                systemText.TextSet("バッグが一杯だ！");
                return false;
            }
            string insertQuery = "INSERT INTO Inventory (Id, Num) VALUES ('" + offensiveItem.Id + "', " + num + ")";

            systemText.TextSet(offensiveItem.ItemName + " (" + num + ") を手に入れた");

            sqlDB.ExecuteNonQuery(insertQuery);
            return true;
        }


        public int InventoryCount()
        {
            string query = "SELECT COUNT(*) AS TotalCount FROM Inventory";
            DataTable result = sqlDB.ExecuteQuery(query);
            object value = result.Rows[0]["TotalCount"];
            itemCount = Convert.ToInt32(value);
            return itemCount;
        }
    }
}