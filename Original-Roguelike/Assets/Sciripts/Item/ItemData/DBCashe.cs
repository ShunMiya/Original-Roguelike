using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

namespace ItemSystem
{
    public class DBCashe : MonoBehaviour
    {
        SqliteDatabase sqlDB;

        private void Awake()
        {
            sqlDB = new SqliteDatabase("ItemDataBase.db");
            string query = "SELECT * FROM Equipment";
            DataTable equipmentData = sqlDB.ExecuteQuery(query);
            ItemDataCache.CacheEquipment(equipmentData);

            query = "SELECT * FROM Consumable";
            DataTable consumableData = sqlDB.ExecuteQuery(query);
            ItemDataCache.CacheConsumable(consumableData);
        }
    }
}