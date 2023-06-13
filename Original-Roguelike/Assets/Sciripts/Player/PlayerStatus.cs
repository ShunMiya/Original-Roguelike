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

        public bool PlayerActive = false; //移動、攻撃、アイテムの使用(装備の着脱含)

        public bool IsPlayerActive()
        {
            return playerMove.IsMoving() || attackMotion.IsAttacking();
        }

    }
}
