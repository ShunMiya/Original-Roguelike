using PlayerStatusList;
using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;

namespace ItemSystem
{
    public class SQLDBPlayerInventory : MonoBehaviour
    {
        private SqliteDatabase sqlDB;
        string query;
        private string copiedDatabasePath;
        int inventorySize = 5;
        int itemCount;

        public void Awake()
        {
            copiedDatabasePath = Path.Combine(Application.persistentDataPath, "ItemDataBase.db");
        }

        public bool AddItem(int itemId, int num)
        {
            sqlDB = new SqliteDatabase(copiedDatabasePath);

            itemCount = InventoryCount();

            query = "SELECT * FROM Consumable WHERE Id = " + itemId;
            DataTable consumableData = sqlDB.ExecuteQuery(query);

            query = "SELECT * FROM Equipment WHERE Id = " + itemId;
            DataTable equipmentData = sqlDB.ExecuteQuery(query);

            bool GetItem = false;
            if (consumableData.Rows.Count > 0)
            {
                GetItem = AddConsumable(itemId, num);
                return GetItem;
            }
            else if (equipmentData.Rows.Count > 0)
            {
                GetItem = AddEquipment(equipmentData);
                return GetItem;
            }
            return GetItem;
        }


        public bool AddConsumable(int itemId,int num)
        {
            /*string query = "SELECT * FROM Inventory WHERE Id = " + consumableData.Rows[0]["Id"];
            DataTable itemsData = sqlDB.ExecuteQuery(query);
            int itemId = Convert.ToInt32(consumableData.Rows[0]["Id"]);*/

            string query = "SELECT * FROM Inventory WHERE Id = " + itemId;
            DataTable itemsData = sqlDB.ExecuteQuery(query);

            Debug.Log(itemId+"でキャッシュ捜索");
            ConsumableData consumableItem = ItemDataCache.GetConsumable(itemId);
            Debug.Log("Idが" + consumableItem.Id + " MaxStockが" + consumableItem.MaxStock + "　名前が" + consumableItem.ItemName+" 回復値が"+consumableItem.HealValue);

            foreach (DataRow row in itemsData.Rows)
            {
                int currentNum = Convert.ToInt32(row["Num"]);
                int totalStock = currentNum + num;
                Debug.Log("入手num:" + num + " 所持品num" + currentNum + " 加算後num:" + totalStock + " 最大num:" + consumableItem.MaxStock);
                if (totalStock <= consumableItem.MaxStock)
                {
                    string updateQuery = "UPDATE Inventory SET Num = " + totalStock + " WHERE IID = " + row["IID"];
                    sqlDB.ExecuteNonQuery(updateQuery);
                    Debug.Log("加算完了");
                    return true;
                }
            }
            if (itemCount == inventorySize) return false;
            string insertQuery = "INSERT INTO Inventory (Id, ItemName, Num) VALUES ('" + consumableItem.Id + "', '" + consumableItem.ItemName + "', " + num + ")";
            sqlDB.ExecuteNonQuery(insertQuery);
            return true;
        }

        public bool AddEquipment(DataTable equipmentData)
        {
            if (itemCount == inventorySize) return false;
            string insertQuery = "INSERT INTO Inventory (Id, ItemName, Num) VALUES ('" + equipmentData.Rows[0]["Id"] + "', '" + equipmentData.Rows[0]["ItemName"] + "', " + 1 + ")";
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