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

                        itemStack =playerInventory.RemoveItem(itemId, itemStack);
                    }
                    break;
                default:
                    string ItemId = itemData.Id;

                    playerInventory.RemoveItem(ItemId, 0);
                    break;
            }
            return itemStack;
        }

    }
}