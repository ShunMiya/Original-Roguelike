using UnityEngine;
using PlayerMovement;
using Combat.AttackMotion;
using ItemSystem;

namespace PlayerStatusList
{
    public class PlayerStatus : MonoBehaviour
    {
        private PlayerMove playerMove;
        private AttackMotion attackMotion;

        public int inventorySize;
        public Equipment RightEquip;
        public Equipment LeftEquip;

        public PlayerInventoryDataBase inventoryData;

        public bool PlayerActive = false; //移動、攻撃、アイテムの使用(装備の着脱含)

        private void Start()
        {
            playerMove = GetComponent<PlayerMove>();
            attackMotion = GetComponent<AttackMotion>();

            inventorySizeUpDate();
        }

        public void inventorySizeUpDate()
        {
            inventoryData.InitializeFromPlayerStatus(inventorySize);
        }

        public void EquipItem(IItemData ITemData)
        {
            Equipment itemData = ITemData as Equipment;
            switch (itemData.EquipType)
            {
                case 0:
                    if (LeftEquip != null) ((Equipment)LeftEquip).Equipped = false;
                    ((Equipment)itemData).Equipped = true;
                    LeftEquip = itemData;
                    break;
                case (EquipType)1:
                    if (LeftEquip != null) ((Equipment)LeftEquip).Equipped = false;
                    ((Equipment)itemData).Equipped = true;
                    LeftEquip = itemData;
                    if (RightEquip != null) ((Equipment)RightEquip).Equipped = false;
                    RightEquip = null;
                    break;
                case (EquipType)2:
                    ((Equipment)itemData).Equipped = true;
                    RightEquip = itemData;
                    if (LeftEquip != null && ((Equipment)LeftEquip).EquipType == (EquipType)1)
                    {
                        ((Equipment)LeftEquip).Equipped = false;
                        LeftEquip = null;
                    }
                    break;
            }
        }
        public void UnequipItem(IItemData itemData)
        {
            if (RightEquip == itemData as Equipment)
            {
                ((Equipment)RightEquip).Equipped = false;
                RightEquip = null;
                return;
            }
            if (LeftEquip == itemData as Equipment)
            {
                ((Equipment)LeftEquip).Equipped = false;
                LeftEquip = null;
                return;
            }
        }

        public bool IsPlayerActive()
        {
            return playerMove.IsMoving() || attackMotion.IsAttacking();
        }

    }
}
