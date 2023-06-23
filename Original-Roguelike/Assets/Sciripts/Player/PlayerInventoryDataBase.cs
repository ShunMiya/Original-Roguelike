using PlayerStatusList;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ItemSystem
{

    [CreateAssetMenu(menuName = "ScriptableObject/PlayerInventoryDB")]
    public class PlayerInventoryDataBase : ScriptableObject
    {
        public List<ItemData> inventory = new List<ItemData>();
        public int inventorySize;

        public void InitializeFromPlayerStatus(PlayerStatus playerStatus)
        {
            Debug.Log(inventorySize + "Ç"+playerStatus.inventorySize + "Ç…ïœçX");
            inventorySize = playerStatus.inventorySize;
        }

        public bool AddItem(string itemId , int itemStack)
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
                            Debug.Log(itemData.ItemName + "(" + itemStack + ")" + "ÇéÊìæ");
                            return true;
                        }
                    }
                    if (inventory.Count == inventorySize) return false;
                    UseItemData newItem = Instantiate(useItemData);
                    newItem.ItemStack = itemStack;
                    Debug.Log(itemData.ItemName + "(" + itemStack + ")" + "ÇéÊìæ");
                    inventory.Add(newItem);
                    break;
                default:
                    if (inventory.Count == inventorySize) return false;
                    ItemData newEquipItem = Instantiate(itemData);
                    inventory.Add(newEquipItem);
                    Debug.Log(newEquipItem.ItemName + "ÇéÊìæ");
                    break;
            }
            return true;
        }

        public int RemoveItem(string itemId , int itemStack)
        {
            ItemData itemData = ItemManager.Instance.GetItemDataById(itemId);

            switch (itemData.ItemType)
            {
                case 0:
                    UseItemData existingItem = inventory
    .Find(item => item.Id == itemId && (item as UseItemData).ItemStack == itemStack) as UseItemData;

                    if (existingItem != null)
                    {
                        int remainingStack = existingItem.ItemStack - 1;

                        if (remainingStack > 0)
                        {
                            existingItem.ItemStack = remainingStack;
                            Debug.Log(itemData.ItemName + "(" + itemStack + ")" + "Çè¡îÔ");
                            return remainingStack;
                        }
                        else if (remainingStack == 0)
                        {
                            existingItem.ItemStack = remainingStack;
                            inventory.Remove(existingItem);
                            Destroy(existingItem);
                            Debug.Log(itemData.ItemName + "(" + itemStack + ")" + "ÇégÇ¢êÿÇ¡ÇΩ");
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
                        Debug.Log(itemData.ItemName + "ÇéÃÇƒÇΩ");
                    }
                    break;
            }
            return 0;
        }
    }
}