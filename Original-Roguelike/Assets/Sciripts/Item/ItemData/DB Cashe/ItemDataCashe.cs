using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using static UnityEditor.Progress;

namespace ItemSystem
{
    public static class ItemDataCache
    {
        private static Dictionary<int, EquipmentData> equipmentCache;
        private static Dictionary<int, ConsumableData> consumableCache;

        static ItemDataCache()
        {
            equipmentCache = new Dictionary<int, EquipmentData>();
            consumableCache = new Dictionary<int, ConsumableData>();
        }

        public static void CacheEquipment(DataTable equipmentTable)
        {
            foreach (DataRow row in equipmentTable.Rows)
            {
                EquipmentData equipmentData = new EquipmentData();
                equipmentData.Id = Convert.ToInt32(row["Id"]);
                equipmentData.PrefabName = row["PrefabName"].ToString();
                equipmentData.ItemName = row["ItemName"].ToString();
                equipmentData.ItemType = row["ItemType"].ToString();
                equipmentData.Description = row["Description"].ToString();
                equipmentData.EquipType = Convert.ToInt32(row["EquipType"]);
                equipmentData.Equipped = Convert.ToInt32(row["Equipped"]);
                equipmentData.AttackBonus = Convert.ToSingle(row["AttackBonus"]);
                equipmentData.DefenseBonus = Convert.ToSingle(row["DefenseBonus"]);
                equipmentData.WeaponRange = Convert.ToSingle(row["WeaponRange"]);
                equipmentData.WeaponDistance = Convert.ToSingle(row["WeaponDistance"]);

                equipmentCache[equipmentData.Id] = equipmentData;
            }
        }

        public static EquipmentData GetEquipment(int itemId)
        {
            if (equipmentCache.TryGetValue(itemId, out EquipmentData equipmentData))
            {
                return equipmentData;
            }
            else
            {
                return null;
            }
        }

        public static void CacheConsumable(DataTable consumableTable)
        {
            foreach (DataRow row in consumableTable.Rows)
            {
                ConsumableData consumableData = new ConsumableData();
                consumableData.Id = Convert.ToInt32(row["Id"]);
                consumableData.PrefabName = row["PrefabName"].ToString();
                consumableData.ItemName = row["ItemName"].ToString();
                consumableData.ItemType = row["ItemType"].ToString();
                consumableData.Description = row["Description"].ToString();
                consumableData.ItemStock = Convert.ToInt32(row["ItemStock"]);
                consumableData.MaxStock = Convert.ToInt32(row["MaxStock"]);
                consumableData.HealValue = Convert.ToSingle(row["HealValue"]);

                consumableCache[consumableData.Id] = consumableData;
            }
        }

        public static ConsumableData GetConsumable(int itemId)
        {
            if (consumableCache.TryGetValue(itemId, out ConsumableData consumableData))
            {
                return consumableData;
            }
            else
            {
                return null;
            }
        }
    }
}
