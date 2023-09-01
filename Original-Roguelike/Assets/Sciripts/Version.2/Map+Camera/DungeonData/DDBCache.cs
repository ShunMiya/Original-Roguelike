using ItemSystemV2.Inventory;
using System.IO;
using UnityEngine;

namespace Field
{
    public class DDBCache : MonoBehaviour
    {
        SqliteDatabase sqlDB;

        private void Awake()
        {
            string databasePath = SQLDBInitializationV2.GetDatabasePath();
            sqlDB = new SqliteDatabase(databasePath);
            string query = "SELECT DungeonName FROM PlayerStatus WHERE PlayerID = 1;";
            DataTable Data = sqlDB.ExecuteQuery(query);
            string DungeonName = (string)Data[0]["DungeonName"];

            string originalDatabasePath = Path.Combine(Application.streamingAssetsPath, DungeonName + ".db");
            sqlDB = new SqliteDatabase(originalDatabasePath);
            query = "SELECT * FROM FloorInformation";
            DataTable FloorInformationData = sqlDB.ExecuteQuery(query);
            DungeonDataCache.CacheFloorInformation(FloorInformationData);

            query = "SELECT * FROM EnemyAppear";
            DataTable EnemyAppearData = sqlDB.ExecuteQuery(query);
            DungeonDataCache.CacheEnemyAppear(EnemyAppearData);

            query = "SELECT * FROM ItemAppear";
            DataTable ItemAppearData = sqlDB.ExecuteQuery(query);
            DungeonDataCache.CacheItemAppear(ItemAppearData);

        }
    }
}