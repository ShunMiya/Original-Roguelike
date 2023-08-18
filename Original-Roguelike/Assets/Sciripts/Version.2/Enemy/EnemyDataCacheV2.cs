using System;
using System.Collections.Generic;
using System.Linq;

namespace EnemySystem
{
    public static class EnemyDataCacheV2
    {
        private static Dictionary<int, EnemyDataV2> enemyCache;

        static EnemyDataCacheV2()
        {
            enemyCache = new Dictionary<int, EnemyDataV2>();
        }

        public static void CacheEnemy(DataTable enemyTable)
        {
            foreach (DataRow row in enemyTable.Rows)
            {
                EnemyDataV2 enemyData = new EnemyDataV2();
                enemyData.EnemyID = Convert.ToInt32(row["EnemyID"]);
                enemyData.PrefabName = row["PrefabName"].ToString();
                enemyData.MaxHP = Convert.ToInt32(row["MaxHP"]);
                enemyData.Attack = Convert.ToInt32(row["Attack"]);
                enemyData.Defense = Convert.ToInt32(row["Defense"]);
                enemyData.Speed = Convert.ToInt32(row["Speed"]);

                enemyCache[enemyData.EnemyID] = enemyData;
            }
        }

        public static EnemyDataV2 GetEnemyData(int EnemyID)
        {
            return enemyCache.TryGetValue(EnemyID, out EnemyDataV2 enemyData) ? enemyData : null;
        }

        public static EnemyDataV2 GetRandomEnemy()
        {
            int randomIndex = UnityEngine.Random.Range(0, enemyCache.Count - 1);
            EnemyDataV2 randomEnemy = enemyCache.ElementAt(randomIndex).Value;
            return randomEnemy;
        }
    }
}