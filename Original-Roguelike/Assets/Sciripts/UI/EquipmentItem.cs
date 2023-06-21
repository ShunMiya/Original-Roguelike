using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItemSystem;

namespace UISystem
{
    public class EquipmentItem : MonoBehaviour
    {
        public PlayerInventoryDataBase playerInventory;

        public ItemData RightEquip;
        public ItemData LeftEquip;

        public void EquipItem(ItemData ITemData)
        {
            EquipItemData itemData = ITemData as EquipItemData;
            switch (itemData.EquipType)
            {
                case 0:
/*                    if (LeftEquip != null && ((EquipItemData)LeftEquip).EquipType != (EquipType)1)
                    {
                        RightEquip = itemData;
                        break;
                    }*/
                    LeftEquip = itemData;
                    break;
                case (EquipType)1:
                    LeftEquip = itemData;
                    RightEquip = null;
                    break;
                case (EquipType)2:
                    RightEquip = itemData;
                    if (((EquipItemData)LeftEquip).EquipType == (EquipType)1) LeftEquip = null;
                    break;
            }
            SetButtonData();
        }

        private void UnequipItemForRight(ItemData itemData)
        {
            if (RightEquip == itemData)
            {
                RightEquip = null; // ‘•”õ‚ğŠO‚·
                Debug.Log("‘•”õ‚ğ‰ğœ‚µ‚Ü‚µ‚½: " + itemData.ItemName);
            }
        }

        private void UnequipItemForLeft(ItemData itemData)
        {
            if (LeftEquip == itemData)
            {
                LeftEquip = null; // ‘•”õ‚ğŠO‚·
                Debug.Log("‘•”õ‚ğ‰ğœ‚µ‚Ü‚µ‚½: " + itemData.ItemName);
            }
        }

        public void SetButtonData()
        {
            ItemButton rightButton = transform.Find("RightHandButton").GetComponent<ItemButton>();
            rightButton.itemData = RightEquip;

            ItemButton leftButton = transform.Find("LeftHandButton").GetComponent<ItemButton>();
            leftButton.itemData = LeftEquip;
        }
    }
}