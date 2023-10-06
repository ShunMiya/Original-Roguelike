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
            Debug.Log("SQLinventorySize" + inventorySize + "��" + inventorysize + "�ɕύX");
            inventorySize = inventorysize;
        }

        public bool AddItem(int itemId, int num)
        {
            itemCount = InventoryCount();

            ConsumableDataV2 consumableItem = ItemDataCacheV2.GetConsumable(itemId);
            EquipmentDataV2 equipmentItem = ItemDataCacheV2.GetEquipment(itemId);

            bool GetItem = false;
            if (consumableItem != null)
            {
                GetItem = AddConsumable(itemId, num, consumableItem);
                return GetItem;
            }
            else if (equipmentItem != null)
            {
                GetItem = AddEquipment(itemId, num, equipmentItem);
                return GetItem;
            }
            return GetItem;
        }


        public bool AddConsumable(int itemId, int num, ConsumableDataV2 consumableItem)
        {
            string query = "SELECT * FROM Inventory WHERE Id = " + itemId;
            DataTable itemsData = sqlDB.ExecuteQuery(query);

            foreach (DataRow row in itemsData.Rows)
            {
                int currentNum = Convert.ToInt32(row["Num"]);
                int totalStock = currentNum + num;
                if (totalStock <= consumableItem.MaxStock)
                {
                    string updateQuery = "UPDATE Inventory SET Num = " + totalStock + " WHERE IID = " + row["IID"];
                    sqlDB.ExecuteNonQuery(updateQuery);
                    systemText.TextSet(consumableItem.ItemName + " (" + num + ") ����ɓ��ꂽ");
                    return true;
                }
            }
            if (itemCount == inventorySize)
            {
                systemText.TextSet("�o�b�O����t���I");
                return false;
            }
            string insertQuery = "INSERT INTO Inventory (Id, Num) VALUES ('" + consumableItem.Id + "', " + num + ")";
            systemText.TextSet(consumableItem.ItemName + " (" + num + ") ����ɓ��ꂽ");
            sqlDB.ExecuteNonQuery(insertQuery);
            return true;
        }

        public bool AddEquipment(int itemId, int num, EquipmentDataV2 equipmentItem)
        {
            if (itemCount == inventorySize)
            {
                systemText.TextSet("�o�b�O����t���I");
                return false;
            }
            string insertQuery = "INSERT INTO Inventory (Id, Num) VALUES ('" + equipmentItem.Id + "', " + num + ")";
            systemText.TextSet(equipmentItem.ItemName + " ����ɓ��ꂽ");
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