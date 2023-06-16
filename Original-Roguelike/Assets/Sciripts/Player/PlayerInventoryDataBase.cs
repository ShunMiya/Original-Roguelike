using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

namespace ItemSystem
{

    [CreateAssetMenu(menuName = "ScriptableObject/PlayerInventoryDB")]
    public class PlayerInventoryDataBase : ScriptableObject
    {
        public List<ItemData> inventory = new List<ItemData>();

        public void AddItem(string itemId , int itemStack)
        {
            ItemData itemData = ItemManager.Instance.GetItemDataById(itemId);

            switch(itemData.ItemType)
            {
                case 0:
                    UseItemData useItemData = itemData as UseItemData;
                    UseItemData existingItem = inventory.Find(item => item.Id == itemId) as UseItemData;

                    if(existingItem != null)
                    {
                        int totalStack = existingItem.ItemStack + itemStack;
                        if (totalStack <= useItemData.MaxStack)
                        {
                            existingItem.ItemStack = totalStack;
                        }
                        else
                        {
                            UseItemData useitemData = Instantiate(useItemData);
                            useitemData.ItemStack = itemStack;
                            inventory.Add(useitemData);
                        }
                    }
                    else
                    {
                        useItemData.ItemStack = itemStack;
                        inventory.Add(useItemData);
                    }
                    break;
                default:
                    inventory.Add(itemData);
                    break;
            }

            Debug.Log(itemData.ItemName + "(" +itemStack + ")" +  "を取得");

            //デバッグシステム
 /*           Debug.Log("所持アイテム一覧");
            foreach (ItemData item in inventory)
            {
                Debug.Log(item.ItemName + "×" + item.ItemStack);
            }*/
        }
    }
}