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
                                .FindAll(item => item.Id == itemId && item)
                                .Cast<UseItemData>()
                                .ToList();

                    foreach (UseItemData existingItem in existingItems)
                    {
                        int totalStack = existingItem.ItemStack + itemStack;

                        if (totalStack <= useItemData.MaxStack)
                        {
                            existingItem.ItemStack = totalStack;
                            Debug.Log(itemData.ItemName + "(" + itemStack + ")" + "‚ðŽæ“¾");
                            return;
                        }
                    }
                    UseItemData newItem = Instantiate(useItemData);
                    newItem.ItemStack = itemStack;
                    Debug.Log(itemData.ItemName + "(" + itemStack + ")" + "‚ðŽæ“¾");
                    inventory.Add(newItem);
                    break;
                default:
                    inventory.Add(itemData);
                    Debug.Log(itemData.ItemName + "‚ðŽæ“¾");
                    break;
            }
        }

        public int RemoveItem(string itemId , int itemStack)
        {
            ItemData itemData = ItemManager.Instance.GetItemDataById(itemId);

            switch (itemData.ItemType)
            {
                case 0:
                    UseItemData useItemData = itemData as UseItemData;
                    UseItemData existingItem = inventory
    .Find(item => item.Id == itemId && (item as UseItemData).ItemStack == itemStack) as UseItemData;

                    if (existingItem != null)
                    {
                        int remainingStack = existingItem.ItemStack - 1;

                        if (remainingStack > 0)
                        {
                            existingItem.ItemStack = remainingStack;
                            Debug.Log(itemData.ItemName + "(" + itemStack + ")" + "‚ðÁ”ï");
                            return remainingStack;
                        }
                        else if (remainingStack == 0)
                        {
                            existingItem.ItemStack = remainingStack;
                            inventory.Remove(existingItem);
                            Destroy(existingItem);
                            Debug.Log(itemData.ItemName + "(" + itemStack + ")" + "‚ðŽg‚¢Ø‚Á‚½");
                            return remainingStack;
                        }
                        else
                        {
                            itemStack -= existingItem.ItemStack;
                            inventory.Remove(existingItem);
                            Destroy(existingItem);
                        }
                    }
                    break;
                default:
                    ItemData existingEquipItem = inventory.Find(item => item.Id == itemId);

                    if (existingEquipItem != null)
                    {
                        inventory.Remove(existingEquipItem);
                        Debug.Log(itemData.ItemName + "‚ðŽÌ‚Ä‚½");
                    }
                    break;
            }
            return 0;
        }
    }
}