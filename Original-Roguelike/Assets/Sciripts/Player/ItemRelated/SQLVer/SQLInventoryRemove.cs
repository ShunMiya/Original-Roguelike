using System;
using System.IO;
using UnityEngine;

namespace ItemSystemSQL.Inventory
{
    public class SQLInventoryRemove : MonoBehaviour
    {
        private SqliteDatabase sqlDB;
        private string copiedDatabasePath;

        public void Awake()
        {
            Debug.Log("RemoveAwake�Ăяo��");
            copiedDatabasePath = Path.Combine(Application.persistentDataPath, "InventoryDataBase.db");
            sqlDB = new SqliteDatabase(copiedDatabasePath);
        }

        public int RemoveItem(DataRow row,int ItemType)
        {
            if (sqlDB == null) Awake();

            Debug.Log("����");
            int remainingStock = 0;
            switch (ItemType)
            {
                case 0:
                    Debug.Log("Consumable���������J�n");
                    remainingStock = RemoveConsumable(row);
                    break;
                default:
                    Debug.Log("Equipment���������J�n");
                    remainingStock = RemoveEquipment(row);
                    break;
            }
            return remainingStock;
        }

        public int RemoveConsumable(DataRow row)
        {
            int itemStock = Convert.ToInt32(row["Num"]);
            
            int remainingStack = itemStock - 1;
            
            if (remainingStack > 0)
            {
                string updateQuery = "UPDATE Inventory SET Num = " + remainingStack + " WHERE IID = " + row["IID"];
                sqlDB.ExecuteNonQuery(updateQuery);

                Debug.Log(row["IID"] + "��Stock��(" + remainingStack + ")�Ɍ���");
                return remainingStack;
            }
            else if (remainingStack == 0)
            {
                string deleteQuery = "DELETE FROM Inventory WHERE IID = " + row["IID"];
                sqlDB.ExecuteNonQuery(deleteQuery);
                Debug.Log(row["IID"] + "�̃A�C�e�����g���؂���");
                return remainingStack;
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