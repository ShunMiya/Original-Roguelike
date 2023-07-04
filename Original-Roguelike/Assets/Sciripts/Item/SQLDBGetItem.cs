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

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                SQLDBPlayerInventory SQLplayerInventory = other.GetComponent<SQLDBPlayerInventory>();

                bool ItemGet = SQLplayerInventory.AddItem(itemId, num);

                if (ItemGet == false) Debug.Log("Ž‚¿•¨‚ª‚¢‚Á‚Ï‚¢‚¾‚æI");

                if (ItemGet == true) Destroy(gameObject);

                /*string query;
 
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

                    string insertQuery = "INSERT INTO Inventory (Id, ItemName, Num) VALUES ('" + equipmentData.Rows[0]["Id"] + "', '" + equipmentData.Rows[0]["ItemName"] + "', " + num + ")";
                    sqlDB.ExecuteNonQuery(insertQuery);
                }
                else
                {
                    Debug.Log("Null");
                }*/
            }
        }
    }
}