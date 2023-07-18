using System;
using System.Collections.Generic;

namespace Enemy
{
    public static class EnemyDataCashe
    {
        private static Dictionary<int, EnemyData> enemyCashe;

        static EnemyDataCashe()
        {
            enemyCashe = new Dictionary<int, EnemyData> ();
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

                enemyCashe[enemyData.EnemyID] = enemyData;
            }
        }

        public static EnemyData GetEnemyData(int EnemyID)
        {
            return enemyCashe.TryGetValue(EnemyID, out EnemyData enemyData) ? enemyData : null;
        }
    }

}
