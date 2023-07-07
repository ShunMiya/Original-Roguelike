using System;
using UnityEngine;

namespace ItemSystemSQL.Inventory
{
    public class SQLInventoryRemove : MonoBehaviour
    {
        private SqliteDatabase sqlDB;

        public void Start()
        {
            string databasePath = SQLDBInitialization.GetDatabasePath();
            sqlDB = new SqliteDatabase(databasePath);
        }

        public int RemoveItem(DataRow row,int ItemType)
        {
            if(sqlDB == null)
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

                Debug.Log("IID"+row["IID"] + "��Stock��(" + remainingStock + ")�Ɍ���");
                return remainingStock;
            }
            else if (remainingStock == 0)
            {
                string deleteQuery = "DELETE FROM Inventory WHERE IID = " + row["IID"];
                sqlDB.ExecuteNonQuery(deleteQuery);
                Debug.Log(row["IID"] + "�̃A�C�e�����g���؂���");
                return remainingStock;
            }
            return 0;
        }

        public int RemoveEquipment(DataRow row)
        {
            string deleteQuery = "DELETE FROM Inventory WHERE IID = " + row["IID"];
            sqlDB.ExecuteNonQuery(deleteQuery);
            Debug.Log(row["IID"] + "�̃A�C�e�����̂Ă�");

            return 0;
        }
    }
}