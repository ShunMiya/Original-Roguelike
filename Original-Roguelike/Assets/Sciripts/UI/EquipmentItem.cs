using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItemSystem;
using TMPro;
using PlayerStatusList;

namespace UISystem
{
    public class EquipmentItem : MonoBehaviour
    {
        [SerializeField] private PlayerStatus playerStatus;
        public PlayerInventoryDataBase playerInventory;
        [SerializeField] private TextMeshProUGUI BonusText;

//        public ItemData RightEquip;
//        public ItemData LeftEquip;Å@Å@â¸èCó\íË

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
                    if(playerStatus.LeftEquip != null) ((EquipItemData)playerStatus.LeftEquip).Equipped = false;
                    ((EquipItemData)itemData).Equipped = true;
                    playerStatus.LeftEquip = itemData;
                    break;
                case (EquipType)1:
                    if (playerStatus.LeftEquip != null) ((EquipItemData)playerStatus.LeftEquip).Equipped = false;
                    ((EquipItemData)itemData).Equipped = true;
                    playerStatus.LeftEquip = itemData;
                    if(playerStatus.RightEquip != null) ((EquipItemData)playerStatus.RightEquip).Equipped = false;
                    playerStatus.RightEquip = null;
                    break;
                case (EquipType)2:
                    ((EquipItemData)itemData).Equipped = true;
                    playerStatus.RightEquip = itemData;
                    if (playerStatus.LeftEquip != null && ((EquipItemData)playerStatus.LeftEquip).EquipType == (EquipType)1)
                    {
                        ((EquipItemData)playerStatus.LeftEquip).Equipped = false;
                        playerStatus.LeftEquip = null;
                    }
                    break;
            }
            SetButtonData();
            SetBonusText();
        }

        public void UnequipItem(ItemData itemData)
        {
            if (playerStatus.RightEquip == itemData)
            {
                ((EquipItemData)playerStatus.RightEquip).Equipped = false;
                playerStatus.RightEquip = null;
            }
            if (playerStatus.LeftEquip == itemData)
            {
                ((EquipItemData)playerStatus.LeftEquip).Equipped = false;
                playerStatus.LeftEquip = null;
            }
            SetBonusText(); 
        }

        public void SetButtonData()
        {
            EquipmentItemButton leftButton = transform.Find("LeftHandButton").GetComponent<EquipmentItemButton>();
            leftButton.SetEquip(playerStatus.LeftEquip);
            
            EquipmentItemButton rightButton = transform.Find("RightHandButton").GetComponent<EquipmentItemButton>();
            rightButton.SetEquip(playerStatus.RightEquip);
        }

        public void SetBonusText()
        {
            EquipItemData rightequip = playerStatus.RightEquip as EquipItemData;
            EquipItemData leftequip = playerStatus.LeftEquip as EquipItemData;
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