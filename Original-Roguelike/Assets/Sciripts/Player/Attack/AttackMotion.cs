using UnityEngine;
using PlayerFrontChecker;
using PlayerStatusList;

namespace Combat.AttackMotion
{
    public class AttackMotion : MonoBehaviour
    {
        bool isAttacking = false;
        private PlayerFrontCheck playerFrontCheck;
        public PlayerStatusSQL playerStatusSQL;

        private void Start()
        {
            playerFrontCheck = GetComponentInChildren<PlayerFrontCheck>();
        }
        public void AttackStance()
        {
            isAttacking = true;
            Debug.Log("Attack!");
            if (playerFrontCheck.IsAttackHitCheck())
            {
                playerStatusSQL.WeaponStatusPlus();//UseItemErrorCare
                playerFrontCheck.Attacked(playerStatusSQL.Attack);
            }
            isAttacking = false;
        }

        public bool IsAttacking()
        {
            return isAttacking;
        }
    }
}