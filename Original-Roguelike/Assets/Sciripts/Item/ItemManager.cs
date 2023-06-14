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
                    return itemData;
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
