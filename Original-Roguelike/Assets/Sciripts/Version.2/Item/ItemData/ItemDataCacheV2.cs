using System;
using System.Collections.Generic;
using System.Linq;

namespace ItemSystemV2
{
    public static class ItemDataCacheV2
    {
        private static Dictionary<int, IItemDataV2> equipmentCache;
        private static Dictionary<int, IItemDataV2> consumableCache;
        private static Dictionary<int, IItemDataV2> offensiveCache;

        static ItemDataCacheV2()
        {
            equipmentCache = new Dictionary<int, IItemDataV2>();
            consumableCache = new Dictionary<int, IItemDataV2>();
            offensiveCache = new Dictionary<int, IItemDataV2>();
        }

        public static IItemDataV2 GetIItemData(int itemId)
        {
            IItemDataV2 consumableItem = ItemDataCacheV2.GetConsumable(itemId);
            IItemDataV2 equipmentItem = ItemDataCacheV2.GetEquipment(itemId);
            IItemDataV2 offensiveItem = ItemDataCacheV2.GetOffensive(itemId);

            if (consumableItem != null) return consumableItem;
            else if (equipmentItem != null) return equipmentItem;
            else if (offensiveItem != null) return offensiveItem;
            else return null;
        }

        public static void CacheEquipment(DataTable equipmentTable)
        {
            foreach (DataRow row in equipmentTable.Rows)
            {
                EquipmentDataV2 equipmentData = new EquipmentDataV2();
                equipmentData.Id = Convert.ToInt32(row["Id"]);
                equipmentData.PrefabName = row["PrefabName"].ToString();
                equipmentData.ItemName = row["ItemName"].ToString();
                equipmentData.ItemType = Convert.ToInt32(row["ItemType"]);
                equipmentData.Description = row["Description"].ToString();
                equipmentData.EquipType = Convert.ToInt32(row["EquipType"]);
                equipmentData.AttackBonus = Convert.ToInt32(row["AttackBonus"]);
                equipmentData.DefenseBonus = Convert.ToInt32(row["DefenseBonus"]);
                equipmentData.WeaponRange = Convert.ToInt32(row["WeaponRange"]);

                equipmentCache[equipmentData.Id] = equipmentData;
            }
        }

        public static EquipmentDataV2 GetEquipment(int itemId)
        {
            return equipmentCache.TryGetValue(itemId, out IItemDataV2 equipmentData) ? equipmentData as EquipmentDataV2 : null;
        }

        public static void CacheConsumable(DataTable consumableTable)
        {
            foreach (DataRow row in consumableTable.Rows)
            {
                ConsumableDataV2 consumableData = new ConsumableDataV2();
                consumableData.Id = Convert.ToInt32(row["Id"]);
                consumableData.PrefabName = row["PrefabName"].ToString();
                consumableData.ItemName = row["ItemName"].ToString();
                consumableData.ItemType = Convert.ToInt32(row["ItemType"]);
                consumableData.Description = row["Description"].ToString();
                consumableData.ConsumableType = Convert.ToInt32(row["ConsumableType"]);
                consumableData.MaxStock = Convert.ToInt32(row["MaxStock"]);
                consumableData.HealValue = Convert.ToInt32(row["HealValue"]);

                consumableCache[consumableData.Id] = consumableData;
            }
        }

        public static ConsumableDataV2 GetConsumable(int itemId)
        {
            return consumableCache.TryGetValue(itemId, out IItemDataV2 consumableData) ? consumableData as ConsumableDataV2 : null;
        }

        public static IItemDataV2 GetRandomItem(bool isEquipment)
        {
            Dictionary<int, IItemDataV2> selectedCache;

            selectedCache = isEquipment ? equipmentCache : consumableCache;

            int randomIndex = UnityEngine.Random.Range(0, selectedCache.Count);
            IItemDataV2 randomItem = selectedCache.ElementAt(randomIndex).Value;
            return randomItem;
        }
    }
}