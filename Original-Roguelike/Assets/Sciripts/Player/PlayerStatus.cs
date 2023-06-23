using System.Collections;
using System.Collections.Generic;
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
        public ItemData RightEquip;
        public ItemData LeftEquip;

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
            inventoryData.InitializeFromPlayerStatus(this);
        }

        public bool IsPlayerActive()
        {
            return playerMove.IsMoving() || attackMotion.IsAttacking();
        }

    }
}
