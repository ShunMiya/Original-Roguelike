using System;
using UISystem;
using UnityEngine;

namespace ItemSystemSQL.Inventory
{
    public class SQLInventoryAdd : MonoBehaviour
    {
        private SqliteDatabase sqlDB;
        private SystemText systemText;
        int inventorySize;
        int itemCount;

        public void Start()
        {
            string databasePath = SQLDBInitialization.GetDatabasePath();
            sqlDB = new SqliteDatabase(databasePath);
            systemText = FindObjectOfType<SystemText>();
        }

        public void inventorySizeSet(int inventorysize)
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
                    systemText.TextSet(consumableItem.ItemName + ":Stock" + num + " Get!");
                    Debug.Log(totalStock+"に加算完了");
                    return true;
                }
            }
            if (itemCount == inventorySize)
            {
                systemText.TextSet("Inventory Max!");
                return false;
            }
            string insertQuery = "INSERT INTO Inventory (Id, ItemName, Num) VALUES ('" + consumableItem.Id + "', '" + consumableItem.ItemName + "', " + num + ")";
            systemText.TextSet(consumableItem.ItemName + ":Stock" + num + " Get!");
            sqlDB.ExecuteNonQuery(insertQuery);
            return true;
        }

        public bool AddEquipment(int itemId, EquipmentData equipmentItem)
        {
            if (itemCount == inventorySize)
            {
                systemText.TextSet("Inventory Max!");
                return false;
            }
            string insertQuery = "INSERT INTO Inventory (Id, ItemName, Num) VALUES ('" + equipmentItem.Id + "', '"+ equipmentItem.ItemName + "', " + 1 + ")";
            systemText.TextSet(equipmentItem.ItemName + " Get!");
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