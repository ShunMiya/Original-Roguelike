using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ItemSystem
{
    [CreateAssetMenu(menuName = "ScriptableObject/PlayerInventoryDB")]
    public class PlayerInventoryDataBase : ScriptableObject
    {
        public List<IItemData> inventory = new List<IItemData>();
        int inventorySize;

        public void InitializeFromPlayerStatus(int inventorysize)
        {
            Debug.Log("ListinventorySize"+inventorySize + "を"+inventorysize + "に変更");
            inventorySize = inventorysize;
        }

        public bool AddItem(int itemId , int num)
        {
            Debug.Log("Inventory到着");
            IItemData itemData = IItemDataBase.GetItemById(itemId);
            Debug.Log("itemData抽出");
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
                    Debug.Log(existingItem.ItemName + "(" + num + ")" + "に増加");
                    return true;
                }
            }
            if (inventory.Count == inventorySize) return false;
            Consumable newItem = Instantiate(consumable);
            newItem.ItemStock = num;
            Debug.Log(newItem.ItemName + "(" + num + ")" + "を取得");
            inventory.Add(newItem);
            return true;
        }

        public bool AddEquipment(IItemData itemData)
        {
            if (inventory.Count == inventorySize) return false;
            Equipment equipment = itemData as Equipment;
            Equipment newEquipItem = Instantiate(equipment);
            inventory.Add(newEquipItem);
            Debug.Log(newEquipItem.ItemName + "を取得");
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
                    Debug.Log(itemData.ItemName + "(" + num + ")" + "を消費");
                    return remainingStack;
                }
                else if (remainingStack == 0)
                {
                    existingItem.ItemStock = remainingStack;
                    inventory.Remove(existingItem);
                    Destroy(existingItem);
                    Debug.Log(itemData.ItemName + "(" + num + ")" + "を使い切った");
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
                Debug.Log(itemData.ItemName + "を捨てた");
            }
            return 0;
        }
    }
}