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

        public void UnequipItem(ItemData itemData)
        {
            if (RightEquip == itemData)
            {
                RightEquip = null;
                return;
            }
            if (LeftEquip == itemData)
            {
                LeftEquip = null;
                return;
            }
        }

        public void SetButtonData()
        {
            EquipmentItemButton leftButton = transform.Find("LeftHandButton").GetComponent<EquipmentItemButton>();
            leftButton.SetEquip(LeftEquip);
            
            EquipmentItemButton rightButton = transform.Find("RightHandButton").GetComponent<EquipmentItemButton>();
            rightButton.SetEquip(RightEquip);
        }
    }
}