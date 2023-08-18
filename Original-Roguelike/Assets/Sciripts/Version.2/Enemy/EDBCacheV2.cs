using System.IO;
using UnityEngine;

namespace EnemySystem
{
    public class EDBCacheV2 : MonoBehaviour
    {
        SqliteDatabase sqlDB;

        private void Awake()
        {
            string originalDatabasePath = Path.Combine(Application.streamingAssetsPath, "EnemyDataBase.db");
            sqlDB = new SqliteDatabase(originalDatabasePath);
            string query = "SELECT * FROM Enemy";
            DataTable enemyData = sqlDB.ExecuteQuery(query);
            EnemyDataCacheV2.CacheEnemy(enemyData);
        }
    }
}