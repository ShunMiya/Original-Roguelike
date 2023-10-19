using System;
using UISystem;
using UnityEngine;
using PlayerStatusSystemV2;

namespace ItemSystemV2.Inventory
{
    public class SQLInventoryRemoveV2 : MonoBehaviour
    {
        private SqliteDatabase sqlDB;
        private PlayerStatusV2 playerStatusV2;

        public void Start()
        {
            string databasePath = SQLDBInitializationV2.GetDatabasePath();
            sqlDB = new SqliteDatabase(databasePath);
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
                    remainingStock = RemoveStackItem(row);
                    break;
                case 1:
                    DiscardItem(row);
                    break;
            }
            return remainingStock;
        }

        public int RemoveStackItem(DataRow row)
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

        public void DiscardItem(DataRow row)
        {
            string deleteQuery = "DELETE FROM Inventory WHERE IID = " + row["IID"];
            sqlDB.ExecuteNonQuery(deleteQuery);
            playerStatusV2.WeaponStatusPlus();
        }
    }
}