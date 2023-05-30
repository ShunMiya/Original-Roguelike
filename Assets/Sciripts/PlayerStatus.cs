using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerMovement;
using Combat.AttackMotion;

namespace PlayerStatusList
{
    public class PlayerStatus : MonoBehaviour
    {
        private PlayerMove playerMove;
        private AttackMotion attackMotion;

        private void Start()
        {
            playerMove = GetComponent<PlayerMove>();
            attackMotion = GetComponent<AttackMotion>();
        }

        public bool PlayerActive = false; //�ړ��A�U���A�A�C�e���̎g�p(�����̒��E��)

        public bool IsPlayerActive()
        {
            return playerMove.IsMoving() || attackMotion.IsAttacking();
        }

    }
}
