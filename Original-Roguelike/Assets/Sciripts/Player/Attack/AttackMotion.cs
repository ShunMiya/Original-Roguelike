using UnityEngine;
using PlayerFrontChecker;
using PlayerStatusList;
using UISystem;

namespace Combat.AttackMotion
{
    public class AttackMotion : MonoBehaviour
    {
        bool isAttacking = false;
        private PlayerFrontCheck playerFrontCheck;
        public PlayerStatusSQL playerStatusSQL;
        public SystemText systemText;

        private void Start()
        {
            playerFrontCheck = GetComponentInChildren<PlayerFrontCheck>();
        }
        public void AttackStance()
        {
            isAttacking = true;
            systemText.TextSet("Attack!");
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