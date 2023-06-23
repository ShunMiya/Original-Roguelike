using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItemSystem;
using TMPro;

namespace UISystem
{
    public class EquipmentItem : MonoBehaviour
    {
        public PlayerInventoryDataBase playerInventory;
        [SerializeField] private TextMeshProUGUI BonusText;

        public ItemData RightEquip;
        public ItemData LeftEquip;

        public void EquipItem(ItemData ITemData)
        {
            EquipItemData itemData = ITemData as EquipItemData;
            switch (itemData.EquipType)
            {
                case 0:
                    /*if (LeftEquip != null && ((EquipItemData)LeftEquip).EquipType != (EquipType)1)
                    {
                        RightEquip = itemData;
                        break;
                    }*/
                    if(LeftEquip != null) ((EquipItemData)LeftEquip).Equipped = false;
                    ((EquipItemData)itemData).Equipped = true;
                    LeftEquip = itemData;
                    break;
                case (EquipType)1:
                    if (LeftEquip != null) ((EquipItemData)LeftEquip).Equipped = false;
                    ((EquipItemData)itemData).Equipped = true;
                    LeftEquip = itemData;
                    if(RightEquip != null) ((EquipItemData)RightEquip).Equipped = false;
                    RightEquip = null;
                    break;
                case (EquipType)2:
                    ((EquipItemData)itemData).Equipped = true;
                    RightEquip = itemData;
                    if (LeftEquip != null && ((EquipItemData)LeftEquip).EquipType == (EquipType)1)
                    {
                        ((EquipItemData)LeftEquip).Equipped = false;
                        LeftEquip = null;
                    }
                    break;
            }
            SetButtonData();
            SetBonusText();
        }

        public void UnequipItem(ItemData itemData)
        {
            if (RightEquip == itemData)
            {
                ((EquipItemData)RightEquip).Equipped = false;
                RightEquip = null;
            }
            if (LeftEquip == itemData)
            {
                ((EquipItemData)LeftEquip).Equipped = false;
                LeftEquip = null;
            }
            SetBonusText(); 
        }

        public void SetButtonData()
        {
            EquipmentItemButton leftButton = transform.Find("LeftHandButton").GetComponent<EquipmentItemButton>();
            leftButton.SetEquip(LeftEquip);
            
            EquipmentItemButton rightButton = transform.Find("RightHandButton").GetComponent<EquipmentItemButton>();
            rightButton.SetEquip(RightEquip);
        }

        public void SetBonusText()
        {
            EquipItemData rightequip = RightEquip as EquipItemData;
            EquipItemData leftequip = LeftEquip as EquipItemData;
            float addAttack = (rightequip != null ? rightequip.AttackBonus : 0) + (leftequip != null ? leftequip.AttackBonus : 0);
            float addDefence = (rightequip != null ? rightequip.DefenseBonus : 0) + (leftequip != null ? leftequip.DefenseBonus : 0);
            float RangeBonus = (rightequip != null ? rightequip.WeaponRange : 0) + (leftequip != null ? leftequip.WeaponRange : 0);
            float DistanceBonus = (rightequip != null ? rightequip.WeaponDistance : 0) + (leftequip != null ? leftequip.WeaponDistance : 0);


            string output = "Attack Bonus\t"+addAttack+"\n" +
                            "Defense Bonus\t"+addDefence+"\n" +
                            "Range Bonus\t"+RangeBonus+"\n" +
                            "Distance Bonus\t"+DistanceBonus;

            BonusText.text = (output);
        }

    }
}