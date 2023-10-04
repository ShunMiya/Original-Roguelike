using System.IO;
using UnityEngine;

namespace PlayerStatusSystemV2
{
    public class PLDBCache : MonoBehaviour
    {
        SqliteDatabase sqlDB;

        private void Awake()
        {
            string originalDatabasePath = Path.Combine(Application.streamingAssetsPath, "PlayerLevelDataBase.db");
            sqlDB = new SqliteDatabase(originalDatabasePath);
            string query = "SELECT * FROM Level";
            DataTable playerlevelData = sqlDB.ExecuteQuery(query);
            PlayerLevelDataCache.CachePlayerLevel(playerlevelData);

        }
    }
}