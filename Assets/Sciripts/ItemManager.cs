using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItemSystem
{
    public class ItemManager : MonoBehaviour
    {
        [SerializeField] private ItemDataBase itemDataBase;

        private void Awake()
        {
            itemDataBase = Resources.Load<ItemDataBase>("ItemDataBase");
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

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
