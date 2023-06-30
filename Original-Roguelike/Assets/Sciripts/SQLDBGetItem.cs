using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEditor.Search;
using UnityEngine;

namespace ItemSystem
{
    public class SQLDBGetItem : MonoBehaviour
    {
        [SerializeField] private int itemId;
        [SerializeField] private int num2;

        private void OnTriggerEnter(Collider other)
        {
            string query;
            if (other.CompareTag("Player"))
            {
                SqliteDatabase sqlDB = new SqliteDatabase("ItemDataBase.db");
                query = "SELECT * FROM Consumable WHERE Id = " + itemId;
                DataTable consumableData = sqlDB.ExecuteQuery(query);

                // Equipmentテーブルからアイテムを取得するクエリを実行
                query = "SELECT * FROM Equipment WHERE Id = " + itemId;
                DataTable equipmentData = sqlDB.ExecuteQuery(query);


                if (consumableData.Rows.Count > 0)
                {
                    int id;
                    string name = "";
                    int itemStock;
                    foreach(DataRow row in consumableData.Rows)
                    {
                        id = (int)row["Id"];
                        name = (string)row["ItemName"];
                        itemStock = (int)row["ItemStock"];
                        Debug.Log("ID:" + id + "name:" + name + "のItemStock" + itemStock + "を" + num2 + "に変更");
                    }
                    // Consumableテーブルからデータを取得した場合の処理
                    // consumableDataを利用して必要な処理を行う
                    // 例: consumableData.Rows[0]["ColumnName"] で列の値にアクセスするなど
                }
                else if (equipmentData.Rows.Count > 0)
                {
                    int id;
                    string name = "";
                    int EquipType;
                    string Desciption = "";
                    foreach (DataRow dr in equipmentData.Rows)
                    {
                        id = (int)dr["Id"];
                        name = (string)dr["ItemName"];
                        EquipType = (int)dr["EquipType"];
                        Desciption = (string)dr["Desciption"];
                        Debug.Log("Id" + id + "name:" + name + " EquipType:" + EquipType + "Desciption" + Desciption);
                    }


                    SqliteDatabase inventoryDataBase = new SqliteDatabase("InventoryDataBase.db");
                    string insertQuery = "INSERT INTO Inventory (Id, ItemName, Num) VALUES ('" + equipmentData.Rows[0]["Id"] + "', '" + equipmentData.Rows[0]["ItemName"] + "', " + num2 + ")";
                    inventoryDataBase.ExecuteNonQuery(insertQuery);
                }
                else
                {
                    Debug.Log("Null");
                    // アイテムが見つからなかった場合の処理
                    // エラーメッセージの表示など適切な処理を行う
                }
            }
        }
    }
}