using System.IO;
using UnityEngine;

namespace Enemy
{
    public class EDBCashe : MonoBehaviour
    {
        SqliteDatabase sqlDB;

        private void Awake()
        {
            string originalDatabasePath = Path.Combine(Application.streamingAssetsPath, "EnemyDataBase.db");
            sqlDB = new SqliteDatabase(originalDatabasePath);
            string query = "SELECT * FROM Enemy";
            DataTable enemyData = sqlDB.ExecuteQuery(query);
            EnemyDataCashe.CasheEnemy(enemyData);
        }
    }
}