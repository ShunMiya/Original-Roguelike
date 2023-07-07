using System;
using UnityEngine;

namespace ItemSystemSQL.Inventory
{
    public class SQLInventoryAdd : MonoBehaviour
    {
        private SqliteDatabase sqlDB;
        string query;
        int inventorySize;
        int itemCount;

        public void Start()
        {
            string databasePath = SQLDBInitialization.GetDatabasePath();
            sqlDB = new SqliteDatabase(databasePath);
        }

        public void InitializeFromPlayerStatus(int inventorysize)
        {
            Debug.Log("SQLinventorySize" + inventorySize + "を" + inventorysize + "に変更");
            inventorySize = inventorysize;
        }

        public bool AddItem(int itemId, int num)
        {
            itemCount = InventoryCount();

            ConsumableData consumableItem = ItemDataCache.GetConsumable(itemId);
            EquipmentData equipmentItem = ItemDataCache.GetEquipment(itemId);

            bool GetItem = false;
            if (consumableItem != null)
            {
                GetItem = AddConsumable(itemId, num, consumableItem);
                return GetItem;
            }
            else if (equipmentItem != null)
            {
                GetItem = AddEquipment(itemId, equipmentItem);
                return GetItem;
            }
            return GetItem;
        }


        public bool AddConsumable(int itemId,int num, ConsumableData consumableItem)
        {
            string query = "SELECT * FROM Inventory WHERE Id = " + itemId;
            DataTable itemsData = sqlDB.ExecuteQuery(query);

            foreach (DataRow row in itemsData.Rows)
            {
                int currentNum = Convert.ToInt32(row["Num"]);
                int totalStock = currentNum + num;
                Debug.Log("入手num:" + num + " 所持品num" + currentNum + " 加算後num:" + totalStock + " 最大num:" + consumableItem.MaxStock);
                if (totalStock <= consumableItem.MaxStock)
                {
                    string updateQuery = "UPDATE Inventory SET Num = " + totalStock + " WHERE IID = " + row["IID"];
                    sqlDB.ExecuteNonQuery(updateQuery);
                    Debug.Log(totalStock+"に加算完了");
                    return true;
                }
            }
            if (itemCount == inventorySize) return false;
            string insertQuery = "INSERT INTO Inventory (Id, ItemName, Num) VALUES ('" + consumableItem.Id + "', '" + consumableItem.ItemName + "', " + num + ")";
            Debug.Log(consumableItem.ItemName + "をStock" + num + "で入手");
            sqlDB.ExecuteNonQuery(insertQuery);
            return true;
        }

        public bool AddEquipment(int itemId, EquipmentData equipmentItem)
        {
            if (itemCount == inventorySize) return false;

            string insertQuery = "INSERT INTO Inventory (Id, ItemName, Num) VALUES ('" + equipmentItem.Id + "', '"+ equipmentItem.ItemName + "', " + 1 + ")";
            Debug.Log(equipmentItem.ItemName+"を入手");
            sqlDB.ExecuteNonQuery(insertQuery);
            return true;
        }

        public int InventoryCount()
        {
            query = "SELECT COUNT(*) AS TotalCount FROM Inventory";
            DataTable result = sqlDB.ExecuteQuery(query);
            object value = result.Rows[0]["TotalCount"];
            itemCount = Convert.ToInt32(value);
            Debug.Log(itemCount);
            return itemCount;
        }
    }
}