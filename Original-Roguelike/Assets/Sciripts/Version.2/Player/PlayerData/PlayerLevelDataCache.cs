using System;
using System.Collections.Generic;

namespace PlayerStatusSystemV2
{
    public class PlayerLevelDataCache
    {
        private static Dictionary<int, PlayerLevelData> playerLevelCache;

        static PlayerLevelDataCache()
        {
            playerLevelCache = new Dictionary<int, PlayerLevelData>();
        }

        public static void CachePlayerLevel(DataTable gimmickTable)
        {
            foreach (DataRow row in gimmickTable.Rows)
            {
                PlayerLevelData playerLevelData = new PlayerLevelData();
                playerLevelData.PlayerLevel = Convert.ToInt32(row["PlayerLevel"]);
                playerLevelData.NextLevelExp = Convert.ToInt32(row["NextLevelExp"]);
                playerLevelData.BasicHP = Convert.ToInt32(row["BasicHP"]);
                playerLevelData.HPVariance = Convert.ToInt32(row["HPVariance"]);
                playerLevelData.Strength = Convert.ToInt32(row["Strength"]);

                playerLevelCache[playerLevelData.PlayerLevel] = playerLevelData;
            }
        }

        public static PlayerLevelData GetPlayerLevelData(int PlayerLevel)
        {
            return playerLevelCache.TryGetValue(PlayerLevel, out PlayerLevelData playerLevelData) ? playerLevelData : null;
        }

    }
}