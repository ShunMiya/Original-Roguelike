using ItemSystemSQL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Enemy
{
    public static class EnemyDataCashe
    {
        private static Dictionary<int, EnemyData> enemyCache;

        static EnemyDataCashe()
        {
            enemyCache = new Dictionary<int, EnemyData> ();
        }

        public static void CasheEnemy(DataTable enemyTable)
        {
            foreach (DataRow row in enemyTable.Rows)
            {
                EnemyData enemyData = new EnemyData();
                enemyData.EnemyID = Convert.ToInt32(row["EnemyID"]);
                enemyData.PrefabName = row["PrefabName"].ToString();
                enemyData.MaxHP = Convert.ToInt32(row["MaxHP"]);
                enemyData.Attack = Convert.ToInt32(row["Attack"]);
                enemyData.Defense = Convert.ToInt32(row["Defense"]);
                enemyData.Speed = Convert.ToInt32(row["Speed"]);

                enemyCache[enemyData.EnemyID] = enemyData;
            }
        }

        public static EnemyData GetEnemyData(int EnemyID)
        {
            return enemyCache.TryGetValue(EnemyID, out EnemyData enemyData) ? enemyData : null;
        }

        public static EnemyData GetRandomEnemy()
        {
            int randomIndex = UnityEngine.Random.Range(0, enemyCache.Count-1);
            EnemyData randomEnemy = enemyCache.ElementAt(randomIndex).Value;
            return randomEnemy;
        }
    }
}
