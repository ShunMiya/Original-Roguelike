using ItemSystemV2;
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
                enemyData.EnemyName = row["EnemyName"].ToString();
                enemyData.PrefabName = row["PrefabName"].ToString();
                enemyData.MaterialName = row["MaterialName"].ToString();
                enemyData.AIType = Convert.ToInt32(row["AIType"]);
                enemyData.ThrowAttack = Convert.ToInt32(row["ThrowAttack"]);
                enemyData.MaxHP = Convert.ToInt32(row["MaxHP"]);
                enemyData.Attack = Convert.ToInt32(row["Attack"]);
                enemyData.Defense = Convert.ToInt32(row["Defense"]);
                enemyData.Speed = Convert.ToInt32(row["Speed"]);
                enemyData.Range = Convert.ToInt32(row["Range"]);
                enemyData.EnemyExp = Convert.ToInt32(row["EnemyExp"]);
                enemyData.DropProbability = Convert.ToInt32(row["DropProbability"]);

                enemyCache[enemyData.EnemyID] = enemyData;
            }
        }

        public static EnemyDataV2 GetEnemyData(int EnemyID)
        {
            return enemyCache.TryGetValue(EnemyID, out EnemyDataV2 enemyData) ? enemyData : null;
        }

        public static EnemyDataV2 GetEnemyDataByName(string name)
        {
            foreach (EnemyDataV2 enemyData in enemyCache.Values)
            {
                if (enemyData.PrefabName == name)
                {
                    return enemyData;
                }
            }
            return null;
        }
    }
}