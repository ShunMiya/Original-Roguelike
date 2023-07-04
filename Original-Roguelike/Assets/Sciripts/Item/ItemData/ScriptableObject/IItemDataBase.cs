using System.Collections.Generic;
using UnityEngine;

namespace ItemSystem
{
    [CreateAssetMenu(fileName = "IItemDataBase", menuName = "ScriptableObject/IItemDataBase")]
    public class IItemDataBase : ScriptableObject
    {
        [SerializeField] private List<Consumable> consumables = new List<Consumable>();
        [SerializeField] private List<Equipment> equipments = new List<Equipment>();

        public static IReadOnlyList<Consumable> Consumables => instance.consumables;
        public static IReadOnlyList<Equipment> Equipments => instance.equipments;

        private static IItemDataBase instance;
        private void OnEnable()
        {
            instance = this;
        }

        public static IItemData GetItemById(int id)
        {
            foreach (IItemData item in Consumables)
            {
                if (item.Id == id)
                {
                    return item;
                }
            }

            foreach (IItemData item in Equipments)
            {
                if (item.Id == id)
                {
                    return item;
                }
            }

            return null;
        }

        public static string GetItemNameById(int id)
        {
            foreach (IItemData item in Consumables)
            {
                if (item.Id == id)
                {
                    return item.ItemName;
                }
            }

            foreach (IItemData item in Equipments)
            {
                if (item.Id == id)
                {
                    return item.ItemName;
                }
            }

            return null;
        }

        /*        public static IItemData GetItemByIndex(int index)
                {
                    if (index >= 0 && index < ItemLists.Count)
                    {
                        return ItemLists[index];
                    }

                    return null;
                }*/
    }
}