using System.IO;
using UnityEngine;

namespace Field
{
    public class GDBCache : MonoBehaviour
    {
        SqliteDatabase sqlDB;

        private void Awake()
        {
            string originalDatabasePath = Path.Combine(Application.streamingAssetsPath, "GimmickDataBase.db");
            sqlDB = new SqliteDatabase(originalDatabasePath);
            string query = "SELECT * FROM Gimmick";
            DataTable gimmickData = sqlDB.ExecuteQuery(query);
            GimmickDataCache.CacheGimmick(gimmickData);

        }
    }
}