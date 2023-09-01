using System.IO;
using UnityEngine;

namespace ItemSystemV2
{
    public class DBCacheV2 : MonoBehaviour
    {
        SqliteDatabase sqlDB;

        private void Awake()
        {
            string originalDatabasePath = Path.Combine(Application.streamingAssetsPath, "ItemDataBase.db");
            sqlDB = new SqliteDatabase(originalDatabasePath);
            string query = "SELECT * FROM Equipment";
            DataTable equipmentData = sqlDB.ExecuteQuery(query);
            ItemDataCacheV2.CacheEquipment(equipmentData);

            query = "SELECT * FROM Consumable";
            DataTable consumableData = sqlDB.ExecuteQuery(query);
            ItemDataCacheV2.CacheConsumable(consumableData);
        }
    }
}