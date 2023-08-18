using System;
using UISystem;
using UnityEngine;

namespace ItemSystemSQL.Inventory
{
    public class SQLInventoryRemoveV2 : MonoBehaviour
    {
        private SqliteDatabase sqlDB;
        private SystemText systemText;

        public void Start()
        {
            string databasePath = SQLDBInitialization.GetDatabasePath();
            sqlDB = new SqliteDatabase(databasePath);
            systemText = FindObjectOfType<SystemText>();
        }

        public int RemoveItem(DataRow row, int ItemType)
        {
            if (sqlDB == null)
            {
                string databasePath = SQLDBInitialization.GetDatabasePath();
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

                Debug.Log("IID" + row["IID"] + "��Stock��(" + remainingStock + ")�Ɍ���");
                return remainingStock;
            }
            else if (remainingStock == 0)
            {
                string deleteQuery = "DELETE FROM Inventory WHERE IID = " + row["IID"];
                sqlDB.ExecuteNonQuery(deleteQuery);
                Debug.Log("IID" + row["IID"] + "�̃A�C�e�����g���؂���");
                return remainingStock;
            }
            return 0;
        }

        public int RemoveEquipment(DataRow row)
        {
            string deleteQuery = "DELETE FROM Inventory WHERE IID = " + row["IID"];
            sqlDB.ExecuteNonQuery(deleteQuery);

            if (systemText == null) systemText = FindObjectOfType<SystemText>();
            EquipmentData equipmentItem = ItemDataCache.GetEquipment(Convert.ToInt32(row["Id"]));
            systemText.TextSet(equipmentItem.ItemName + " Destruction");

            return 0;
        }
    }
}