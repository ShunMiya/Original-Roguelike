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

        public int UseItem(ItemData itemData)
        {
            int itemStack = 0;
            switch (itemData.ItemType)
            {
                case 0:
                    if (itemData is UseItemData useItemData)
                    {
                        string itemId = useItemData.Id;
                        itemStack = useItemData.ItemStack;

                        //アイテムの効果処理。RemoveItemも内部に記載。

                        itemStack =playerInventory.RemoveItem(itemId, itemStack);
                    }
                    break;
                default:
                    string ItemId = itemData.Id;
                    //装備解除周りの処理

                    //if (((EquipItemData)itemData).Equipped == true) equipmentItem.UnequipItem(itemData);// NonActive
                    playerInventory.RemoveItem(ItemId, 0);

                    break;
            }
            return itemStack;
        }

    }
}