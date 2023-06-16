using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
                    List<UseItemData> existingItems = inventory
                                .FindAll(item => item.Id == itemId && item is UseItemData)
                                .Cast<UseItemData>()
                                .ToList();

                    foreach (UseItemData existingItem in existingItems)
                    {
                        int totalStack = existingItem.ItemStack + itemStack;

                        if (totalStack <= useItemData.MaxStack)
                        {
                            existingItem.ItemStack = totalStack;
                            Debug.Log(itemData.ItemName + "(" + itemStack + ")" + "を取得");
                            return;
                        }
                    }
                    UseItemData newItem = Instantiate(useItemData);
                    newItem.ItemStack = itemStack;
                    inventory.Add(newItem);
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