using System;
using System.Collections.Generic;

namespace Field
{
    public static class DungeonDataCache
    {
        private static Dictionary<int, FloorInfomationData> FloorInformationCache;
        private static Dictionary<int, EnemyAppearData> EnemyAppearCache;
        private static Dictionary<int, ItemAppearData> ItemAppearCache;
        private static Dictionary<int, GimmickAppearData> GimmickAppearCache;


        static DungeonDataCache()
        {
            FloorInformationCache = new Dictionary<int, FloorInfomationData>();
            EnemyAppearCache = new Dictionary<int, EnemyAppearData>();
            ItemAppearCache = new Dictionary<int, ItemAppearData>();
            GimmickAppearCache = new Dictionary<int, GimmickAppearData>();
        }


        public static void CacheFloorInformation(DataTable FloorInformationTable)
        {
            foreach (DataRow row in FloorInformationTable.Rows)
            {
                FloorInfomationData FloorInfoData = new FloorInfomationData();
                FloorInfoData.FloorLevel = Convert.ToInt32(row["FloorLevel"]);
                FloorInfoData.MinEnemies = Convert.ToInt32(row["MinEnemies"]);
                FloorInfoData.MaxEnemies = Convert.ToInt32(row["MaxEnemies"]);
                FloorInfoData.MinItems = Convert.ToInt32(row["MinItems"]);
                FloorInfoData.MaxItems = Convert.ToInt32(row["MaxItems"]);
                FloorInfoData.TrapNum = Convert.ToInt32(row["TrapNum"]);

                FloorInformationCache[FloorInfoData.FloorLevel] = FloorInfoData;
            }
        }

        public static FloorInfomationData GetFloorInformation(int FloorLevel)
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

        public static List<EnemyAppearData> GetEnemyAppearInFloor(int floorLevel)
        {
            List<EnemyAppearData> enemiesAppearList = new List<EnemyAppearData>();

            foreach (EnemyAppearData enemyAppearData in EnemyAppearCache.Values)
            {
                if (floorLevel == enemyAppearData.FloorLevel)
                {
                    enemiesAppearList.Add(enemyAppearData);
                }
            }

            return enemiesAppearList;
        }


        public static void CacheItemAppear(DataTable ItemAppearTable)
        {
            foreach (DataRow row in ItemAppearTable.Rows)
            {
                ItemAppearData ItemAppearData = new ItemAppearData();
                ItemAppearData.Iid = Convert.ToInt32(row["Iid"]);
                ItemAppearData.ItemId = Convert.ToInt32(row["ItemId"]);
                ItemAppearData.MinFloor = Convert.ToInt32(row["MinFloor"]);
                ItemAppearData.MaxFloor = Convert.ToInt32(row["MaxFloor"]);
                ItemAppearData.GenerationRate = Convert.ToInt32(row["GenerationRate"]);
                ItemAppearData.ShopOnly = Convert.ToBoolean(row["ShopOnly"]);


                ItemAppearCache[ItemAppearData.Iid] = ItemAppearData;
            }
        }

        public static ItemAppearData GetItemAppear(int Iid)
        {
            return ItemAppearCache.TryGetValue(Iid, out ItemAppearData ItemAppearData) ? ItemAppearData as ItemAppearData : null;
        }

        public static List<ItemAppearData> GetItemsAppearInFloor(int floorLevel)
        {
            List<ItemAppearData> itemsAppearList = new List<ItemAppearData>();

            foreach (ItemAppearData itemAppearData in ItemAppearCache.Values)
            {
                if (floorLevel >= itemAppearData.MinFloor && floorLevel <= itemAppearData.MaxFloor)
                {
                    itemsAppearList.Add(itemAppearData);
                }
            }

            return itemsAppearList;
        }

        public static void CacheGimmickAppear(DataTable GimmickAppearTable)
        {
            foreach (DataRow row in GimmickAppearTable.Rows)
            {
                GimmickAppearData GimmickAppearData = new GimmickAppearData();
                GimmickAppearData.Iid = Convert.ToInt32(row["Iid"]);
                GimmickAppearData.GimmickId = Convert.ToInt32(row["GimmickId"]);
                GimmickAppearData.GimmickType = Convert.ToInt32(row["GimmickType"]);
                GimmickAppearData.GenerationRate = Convert.ToInt32(row["GenerationRate"]);


                GimmickAppearCache[GimmickAppearData.Iid] = GimmickAppearData;
            }
        }

        public static GimmickAppearData GetGimmickAppear(int Iid)
        {
            return GimmickAppearCache.TryGetValue(Iid, out GimmickAppearData GimmickAppearData) ? GimmickAppearData as GimmickAppearData : null;
        }

        public static List<GimmickAppearData> GetGimmickAppearInGimmickType(int GimmickType)
        {
            List<GimmickAppearData> gimmickAppearList = new List<GimmickAppearData>();

            foreach (GimmickAppearData gimmickAppearData in GimmickAppearCache.Values)
            {
                if (GimmickType == gimmickAppearData.GimmickType)
                {
                    gimmickAppearList.Add(gimmickAppearData);
                }
            }

            return gimmickAppearList;
        }
    }
}
