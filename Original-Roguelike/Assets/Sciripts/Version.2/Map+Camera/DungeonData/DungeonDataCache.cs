using ItemSystemV2;
using System;
using System.Collections.Generic;

namespace Field
{
    public static class DungeonDataCache
    {
        private static Dictionary<int, FloorInfomationData> FloorInformationCache;
        private static Dictionary<int, EnemyAppearData> EnemyAppearCache;
        private static Dictionary<int, ItemAppearData> ItemAppearCache;

        static DungeonDataCache()
        {
            FloorInformationCache = new Dictionary<int, FloorInfomationData>();
            EnemyAppearCache = new Dictionary<int, EnemyAppearData>();
            ItemAppearCache = new Dictionary<int, ItemAppearData>();
        }


        public static void CacheFloorInformation(DataTable FloorInformationTable)
        {
            foreach (DataRow row in FloorInformationTable.Rows)
            {
                FloorInfomationData FloorInfoData = new FloorInfomationData();
                FloorInfoData.FloorLevel = Convert.ToInt32(row["FloorLevel"]);
                FloorInfoData.MinEnemys = Convert.ToInt32(row["MinEnemys"]);
                FloorInfoData.MaxEnemys = Convert.ToInt32(row["MaxEnemys"]);
                FloorInfoData.MinItems = Convert.ToInt32(row["MinItems"]);
                FloorInfoData.MaxItems = Convert.ToInt32(row["MaxItems"]);

                FloorInformationCache[FloorInfoData.FloorLevel] = FloorInfoData;
            }
        }

        public static FloorInfomationData GetEquipmentFloorInformation(int FloorLevel)
        {
            return FloorInformationCache.TryGetValue(FloorLevel, out FloorInfomationData FloorInfoData) ? FloorInfoData as FloorInfomationData : null;
        }

        public static void CacheEnemyAppear(DataTable EnemyAppearTable)
        {
            foreach (DataRow row in EnemyAppearTable.Rows)
            {
                EnemyAppearData EnemyAppearData = new EnemyAppearData();
                EnemyAppearData.Iid = Convert.ToInt32(row["Iid"]);
                EnemyAppearData.FloorLevel = Convert.ToInt32(row["FloorLevel"]);
                EnemyAppearData.EnemyId = Convert.ToInt32(row["EnemyId"]);
                EnemyAppearData.MinLevel = Convert.ToInt32(row["MinLevel"]);
                EnemyAppearData.MaxLevel = Convert.ToInt32(row["MaxLevel"]);
                EnemyAppearData.GenerationRate = Convert.ToInt32(row["GenerationRate"]);

                EnemyAppearCache[EnemyAppearData.Iid] = EnemyAppearData;
            }
        }

        public static EnemyAppearData GetEnemyAppear(int Iid)
        {
            return EnemyAppearCache.TryGetValue(Iid, out EnemyAppearData EnemyAppearData) ? EnemyAppearData as EnemyAppearData : null;
        }


        public static void CacheItemAppear(DataTable ItemAppearTable)
        {
            foreach (DataRow row in ItemAppearTable.Rows)
            {
                ItemAppearData ItemAppearData = new ItemAppearData();
                ItemAppearData.Iid = Convert.ToInt32(row["Iid"]);
                ItemAppearData.FloorLevel = Convert.ToInt32(row["FloorLevel"]);
                ItemAppearData.ItemId = Convert.ToInt32(row["ItemId"]);
                ItemAppearData.MinFloor = Convert.ToInt32(row["MinFloor"]);
                ItemAppearData.MaxFloor = Convert.ToInt32(row["MaxFloor"]);
                ItemAppearData.ShopOnly = Convert.ToBoolean(row["ShopOnly"]);

                ItemAppearCache[ItemAppearData.FloorLevel] = ItemAppearData;
            }
        }

        public static ItemAppearData GetItemAppear(int Iid)
        {
            return ItemAppearCache.TryGetValue(Iid, out ItemAppearData ItemAppearData) ? ItemAppearData as ItemAppearData : null;
        }

    }
}
