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

        public bool PlayerActive = false; //�ړ��A�U���A�A�C�e���̎g�p(�����̒��E��)

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
