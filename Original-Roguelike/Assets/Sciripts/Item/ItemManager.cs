using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItemSystem
{
    public class ItemManager : MonoBehaviour
    {
        [SerializeField] private ItemDataBase itemDataBase;

        private static ItemManager _instance;
        public static ItemManager Instance { get { return _instance; } }

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }
        }

        public ItemData GetItemDataById(string itemId)
        {
            foreach(ItemData itemData in itemDataBase.GetItemLists())
            {
                if(itemData.Id == itemId)
                {
                    switch(itemData.ItemType)
                    {
                        case ItemType.UseItem:
                            UseItemData useItemData = itemData as UseItemData;
                            return useItemData;
                        case ItemType.EquipItem:
                            EquipItemData equipItemData = itemData as EquipItemData;
                            return equipItemData;
                    }
                }
            }
            return null;
        }

        public string GetItemNameById(string itemId)
        {
            foreach(ItemData itemData in itemDataBase.GetItemLists())
            {
                if(itemData.Id == itemId)
                {
                    return itemData.ItemName;
                }
            }
            return "Unknown";
        }
    }
}