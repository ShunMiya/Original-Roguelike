using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItemSystem
{
    public class PlayerUseItem : MonoBehaviour
    {
        public PlayerInventoryDataBase playerInventory;


        public ItemData UseItem(ItemData itemData)
        {
            switch (itemData.ItemType)
            {
                case 0:
                    if (itemData is UseItemData useItemData)
                    {
                        string itemId = useItemData.Id;
                        int itemStack = useItemData.ItemStack;

                        playerInventory.RemoveItem(itemId, itemStack);
                        itemData = useItemData as ItemData;
                    }
                    break;
                default:
                    string ItemId = itemData.Id;

                    playerInventory.RemoveItem(ItemId, 0);
                    break;
            }
            return itemData;
        }

    }
}