using System;
using UISystem;
using UnityEngine;
using PlayerStatusSystemV2;

namespace ItemSystemV2.Inventory
{
    public class SQLInventoryRemoveV2 : MonoBehaviour
    {
        private SqliteDatabase sqlDB;
        private SystemText systemText;
        private PlayerStatusV2 playerStatusV2;

        public void Start()
        {
            string databasePath = SQLDBInitializationV2.GetDatabasePath();
            sqlDB = new SqliteDatabase(databasePath);
            systemText = FindObjectOfType<SystemText>();
            playerStatusV2 = GetComponent<PlayerStatusV2>();
        }

        public int RemoveItem(DataRow row, int ItemType)
        {
            if (sqlDB == null)
            {
                string databasePath = SQLDBInitializationV2.GetDatabasePath();
                sqlDB = new SqliteDatabase(databasePath);
            }
            int remainingStock = 0;
            switch (ItemType)
            {
                case 0:
                    remainingStock = RemoveConsumable(row);
                    break;
                case 1:
                    remainingStock = RemoveEquipment(row);
                    break;
                case 2:
                    DiscardItem(row);
                    break;
            }
            return remainingStock;
        }

        public int RemoveConsumable(DataRow row)
        {
            int itemStock = Convert.ToInt32(row["Num"]);

            int remainingStock = itemStock - 1;

            if (remainingStock > 0)
            {
                string updateQuery = "UPDATE Inventory SET Num = " + remainingStock + " WHERE IID = " + row["IID"];
                sqlDB.ExecuteNonQuery(updateQuery);

                return remainingStock;
            }
            else if (remainingStock == 0)
            {
                string deleteQuery = "DELETE FROM Inventory WHERE IID = " + row["IID"];
                sqlDB.ExecuteNonQuery(deleteQuery);
                return remainingStock;
            }
            return 0;
        }

        public int RemoveEquipment(DataRow row)
        {
            string deleteQuery = "DELETE FROM Inventory WHERE IID = " + row["IID"];
            sqlDB.ExecuteNonQuery(deleteQuery);

            if (systemText == null) systemText = FindObjectOfType<SystemText>();
            EquipmentDataV2 equipmentItem = ItemDataCacheV2.GetEquipment(Convert.ToInt32(row["Id"]));
            systemText.TextSet(equipmentItem.ItemName + " Destruction");
            playerStatusV2.WeaponStatusPlus();

            return 0;
        }

        public void DiscardItem(DataRow row)
        {
            string deleteQuery = "DELETE FROM Inventory WHERE IID = " + row["IID"];
            sqlDB.ExecuteNonQuery(deleteQuery);
            playerStatusV2.WeaponStatusPlus();
        }
    }
}