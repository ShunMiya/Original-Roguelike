using PlayerStatusList;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;

namespace ItemSystem
{
    [CreateAssetMenu(menuName = "ScriptableObject/PlayerInventoryDB")]
    public class PlayerInventoryDataBase : ScriptableObject
    {
        public List<IItemData> inventory = new List<IItemData>();
        int inventorySize;

        public void InitializeFromPlayerStatus(PlayerStatus playerStatus)
        {
            Debug.Log("inventorySize"+inventorySize + "Ç"+playerStatus.inventorySize + "Ç…ïœçX");
            inventorySize = playerStatus.inventorySize;
        }

        public bool AddItem(int itemId , int num)
        {
            IItemData itemData = IItemDataBase.GetItemById(itemId);
            bool GetItem = false;
            switch(itemData.ItemType)
            {
                case 0:
                    GetItem = AddConsumableItem(itemData,num);
                    break;
                default:
                    GetItem = AddEquipment(itemData);
                    break;
            }
            return GetItem;
        }

        public bool AddConsumableItem(IItemData itemData,int num)
        {
            Consumable consumable = itemData as Consumable;
            List<Consumable> existingItems = inventory
                        .FindAll(item => item.Id == consumable.Id && item is Consumable)
                        .Cast<Consumable>()
                        .ToList();

            foreach (Consumable existingItem in existingItems)
            {
                int totalStack = existingItem.ItemStock + num;

                if (totalStack <= existingItem.MaxStock)
                {
                    existingItem.ItemStock = totalStack;
                    Debug.Log(existingItem.ItemName + "(" + num + ")" + "Ç…ëùâ¡");
                    return true;
                }
            }
            if (inventory.Count == inventorySize) return false;
            Consumable newItem = Instantiate(consumable);
            newItem.ItemStock = num;
            Debug.Log(newItem.ItemName + "(" + num + ")" + "ÇéÊìæ");
            inventory.Add(newItem);
            return true;
        }

        public bool AddEquipment(IItemData itemData)
        {
            if (inventory.Count == inventorySize) return false;
            Equipment equipment = itemData as Equipment;
            Equipment newEquipItem = Instantiate(equipment);
            inventory.Add(newEquipItem);
            Debug.Log(newEquipItem.ItemName + "ÇéÊìæ");
            return true;
        }

        public int RemoveItem(int itemId , int itemStock)
        {
            IItemData itemData = IItemDataBase.GetItemById(itemId);
            int Stock = 0;

            switch (itemData.ItemType)
            {
                case 0:
                    Stock = RemoveConsumable(itemData, itemStock);
                    break;
                default:
                    Stock = RemoveEquipment(itemData);
                    break;
            }
            return Stock;
        }

        public int RemoveConsumable(IItemData itemData,int num)
        {
            Consumable existingItem = inventory
                    .Find(item => item.Id == itemData.Id && (item as Consumable).ItemStock == num) as Consumable;

            if (existingItem != null)
            {
                int remainingStack = existingItem.ItemStock - 1;

                if (remainingStack > 0)
                {
                    existingItem.ItemStock = remainingStack;
                    Debug.Log(itemData.ItemName + "(" + num + ")" + "Çè¡îÔ");
                    return remainingStack;
                }
                else if (remainingStack == 0)
                {
                    existingItem.ItemStock = remainingStack;
                    inventory.Remove(existingItem);
                    Destroy(existingItem);
                    Debug.Log(itemData.ItemName + "(" + num + ")" + "ÇégÇ¢êÿÇ¡ÇΩ");
                    return remainingStack;
                }
            }
            return 0;
        }

        public int RemoveEquipment(IItemData itemData)
        {
            Equipment existingEquipItem = inventory
                    .Find(item => item.Id == itemData.Id)as Equipment;

            if (existingEquipItem != null)
            {
                inventory.Remove(existingEquipItem);
                Debug.Log(itemData.ItemName + "ÇéÃÇƒÇΩ");
            }
            return 0;
        }
    }
}