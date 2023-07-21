using System;
using System.Collections.Generic;
using System.Linq;

namespace ItemSystemSQL
{
    public static class ItemDataCache
    {
        private static Dictionary<int, IItemData> equipmentCache;
        private static Dictionary<int, IItemData> consumableCache;

        static ItemDataCache()
        {
            equipmentCache = new Dictionary<int, IItemData>();
            consumableCache = new Dictionary<int, IItemData>();
        }

        public static IItemData GetIItemData(int itemId)
        {
            IItemData consumableItem = ItemDataCache.GetConsumable(itemId);
            IItemData equipmentItem = ItemDataCache.GetEquipment(itemId);

            return consumableItem != null ? consumableItem : equipmentItem;
        }

        public static void CacheEquipment(DataTable equipmentTable)
        {
            foreach (DataRow row in equipmentTable.Rows)
            {
                EquipmentData equipmentData = new EquipmentData();
                equipmentData.Id = Convert.ToInt32(row["Id"]);
                equipmentData.PrefabName = row["PrefabName"].ToString();
                equipmentData.ItemName = row["ItemName"].ToString();
                equipmentData.ItemType = Convert.ToInt32(row["ItemType"]);
                equipmentData.Description = row["Description"].ToString();
                equipmentData.EquipType = Convert.ToInt32(row["EquipType"]);
                equipmentData.AttackBonus = Convert.ToInt32(row["AttackBonus"]);
                equipmentData.DefenseBonus = Convert.ToInt32(row["DefenseBonus"]);
                equipmentData.WeaponRange = Convert.ToInt32(row["WeaponRange"]);
                equipmentData.WeaponDistance = Convert.ToInt32(row["WeaponDistance"]);

                equipmentCache[equipmentData.Id] = equipmentData;
            }
        }

        public static EquipmentData GetEquipment(int itemId)
        {
            return equipmentCache.TryGetValue(itemId, out IItemData equipmentData) ? equipmentData as EquipmentData : null;
        }

        public static void CacheConsumable(DataTable consumableTable)
        {
            foreach (DataRow row in consumableTable.Rows)
            {
                ConsumableData consumableData = new ConsumableData();
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

        public static ConsumableData GetConsumable(int itemId)
        {
            return consumableCache.TryGetValue(itemId, out IItemData consumableData) ? consumableData as ConsumableData : null;
        }

        public static IItemData GetRandomItem(bool isEquipment)
        {
            Dictionary<int, IItemData> selectedCache;

            selectedCache = isEquipment ? equipmentCache : consumableCache;

            int randomIndex = UnityEngine.Random.Range(0, selectedCache.Count);
            IItemData randomItem = selectedCache.ElementAt(randomIndex).Value;
            return randomItem;
        }
    }
}