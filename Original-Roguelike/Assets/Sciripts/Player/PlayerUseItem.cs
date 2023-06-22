using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItemSystem
{
    public class PlayerUseItem : MonoBehaviour
    {
        public PlayerInventoryDataBase playerInventory;


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

                    playerInventory.RemoveItem(ItemId, 0);

                    //装備解除周りの処理
                    break;
            }
            return itemStack;
        }

    }
}