using System.Collections;
using System.Collections.Generic;
using UISystem;
using UnityEngine;

namespace ItemSystem
{
    public class PlayerUseItem : MonoBehaviour
    {
        public PlayerInventoryDataBase playerInventory;
//        EquipmentItem equipmentItem = GameObject.Find("EquipArea").GetComponent<EquipmentItem>();
        private void Start()
        {
//            equipmentItem.GetComponent<EquipmentItem>();
        }

        public int UseItem(IItemData itemData)
        {
            int itemStock = 0;
            switch (itemData.ItemType)
            {
                case 0:
                    if (itemData is Consumable consumable)
                    {
                        int itemId = consumable.Id;
                        itemStock = consumable.ItemStock;

                        //アイテムの効果処理。RemoveItemも内部に記載。

                        itemStock =playerInventory.RemoveItem(itemId, itemStock);
                    }
                    break;
                default:
                    int ItemId = itemData.Id;
                    //装備解除周りの処理

                    //if (((EquipItemData)itemData).Equipped == true) equipmentItem.UnequipItem(itemData);// NonActive
                    playerInventory.RemoveItem(ItemId, 0);

                    break;
            }
            return itemStock;
        }

    }
}