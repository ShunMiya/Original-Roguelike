using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using UnityEditor.Search;
using UnityEngine;

namespace ItemSystem
{
    public class SQLDBGetItem : MonoBehaviour
    {
        [SerializeField] private int itemId;
        [SerializeField] private int num;
        private int iid;
        private SqliteDatabase sqlDB;

        private void Start()
        {
            string originalDatabasePath = Path.Combine(Application.streamingAssetsPath, "ItemDataBase.db");
            string copiedDatabasePath = Path.Combine(Application.persistentDataPath, "ItemDataBase.db");

            if (File.Exists(copiedDatabasePath))
            {
                File.Delete(copiedDatabasePath);
            }

            File.Copy(originalDatabasePath, copiedDatabasePath);
            iid = 0;

            string dataPath = Application.persistentDataPath;
            Debug.Log("Data Path: " + dataPath);

            sqlDB = new SqliteDatabase(copiedDatabasePath);
        }

        private void OnTriggerEnter(Collider other)
        {
            string query;
            if (other.CompareTag("Player"))
            {
                query = "SELECT * FROM Consumable WHERE Id = " + itemId;
                DataTable consumableData = sqlDB.ExecuteQuery(query);

                query = "SELECT * FROM Equipment WHERE Id = " + itemId;
                DataTable equipmentData = sqlDB.ExecuteQuery(query);

                if (consumableData.Rows.Count > 0)
                {
                    int id;
                    string name = "";
                    int itemStock;
                    foreach (DataRow row in consumableData.Rows)
                    {
                        id = (int)row["Id"];
                        name = (string)row["ItemName"];
                        itemStock = (int)row["ItemStock"];
                        Debug.Log("ID:" + id + " name:" + name + "‚ÌItemStock" + itemStock + "‚ð" + num + "‚É•ÏX");
                    }

                }
                else if (equipmentData.Rows.Count > 0)
                {
                    int id = 0;
                    string name = "";
                    int EquipType = 0;
                    string Desciption = "";
                    foreach (DataRow dr in equipmentData.Rows)
                    {
                        id = (int)dr["Id"];
                        name = (string)dr["ItemName"];
                        EquipType = (int)dr["EquipType"];
                        Desciption = (string)dr["Desciption"];
                        Debug.Log("Id" + id + " name:" + name + " EquipType:" + EquipType + " Desciption" + Desciption);
                    }

                    string insertQuery = "INSERT INTO Inventory (IID, Id, ItemName, Num) VALUES (" + iid + ", '" + equipmentData.Rows[0]["Id"] + "', '" + equipmentData.Rows[0]["ItemName"] + "', " + num + ")";
                    sqlDB.ExecuteNonQuery(insertQuery);
                    iid += 1;
                }
                else
                {
                    Debug.Log("Null");
                }
            }
        }
    }
}