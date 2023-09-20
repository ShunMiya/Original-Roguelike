using System;
using System.Collections.Generic;

namespace Field
{
    public static class GimmickDataCache
    {
        private static Dictionary<int, GimmickData> gimmickCache;

        static GimmickDataCache()
        {
            gimmickCache = new Dictionary<int, GimmickData>();
        }

        public static void CacheGimmick(DataTable gimmickTable)
        {
            foreach (DataRow row in gimmickTable.Rows)
            {
                GimmickData gimmickData = new GimmickData();
                gimmickData.GimmickId = Convert.ToInt32(row["GimmickId"]);
                gimmickData.GimmickType = Convert.ToInt32(row["GimmickType"]);
                gimmickData.GimmickName = row["GimmickName"].ToString();
                gimmickData.PrefabName = row["PrefabName"].ToString();
                gimmickData.Description = row["Description"].ToString();
                gimmickData.BrakeRate = Convert.ToInt32(row["BrakeRate"]);

                gimmickCache[gimmickData.GimmickId] = gimmickData;
            }
        }

        public static GimmickData GetGimmickData(int GimmickId)
        {
            return gimmickCache.TryGetValue(GimmickId, out GimmickData gimmickData) ? gimmickData : null;
        }

        public static GimmickData GetGimmickDataInGimmickName(string GimmickName)
        {
            foreach (var gimmickData in gimmickCache)
            {
                if (gimmickData.Value.GimmickName == GimmickName)
                {
                    return gimmickData.Value;
                }
            }
            return null;
        }
    }
}