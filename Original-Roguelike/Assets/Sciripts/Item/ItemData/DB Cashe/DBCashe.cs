using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.Search;
using UnityEngine;

namespace ItemSystemSQL
{
    public class DBCashe : MonoBehaviour
    {
        SqliteDatabase sqlDB;

        private void Awake()
        {
            string originalDatabasePath = Path.Combine(Application.streamingAssetsPath, "ItemDataBase.db");
            sqlDB = new SqliteDatabase(originalDatabasePath);
            string query = "SELECT * FROM Equipment";
            DataTable equipmentData = sqlDB.ExecuteQuery(query);
            ItemDataCache.CacheEquipment(equipmentData);

            query = "SELECT * FROM Consumable";
            DataTable consumableData = sqlDB.ExecuteQuery(query);
            ItemDataCache.CacheConsumable(consumableData);
        }
    }
}