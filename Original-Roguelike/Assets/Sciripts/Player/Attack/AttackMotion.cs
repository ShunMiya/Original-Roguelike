using UnityEngine;
using PlayerFrontChecker;
using UISystem;

namespace Combat.AttackMotion
{
    public class AttackMotion : MonoBehaviour
    {
        bool isAttacking = false;
        private PlayerFrontCheck playerFrontCheck;
        public SystemText systemText;

        private void Start()
        {
            playerFrontCheck = GetComponentInChildren<PlayerFrontCheck>();
        }
        public void AttackStance()
        {
            isAttacking = true;
            systemText.TextSet("Attack!");

            playerFrontCheck.Attacked();

            isAttacking = false;
        }

        public bool IsAttacking()
        {
            return isAttacking;
        }
    }
}