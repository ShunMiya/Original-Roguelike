using UnityEngine;
using PlayerFrontChecker;
using UISystem;
using PlayerStatusList;

namespace Combat.AttackMotion
{
    public class AttackMotion : MonoBehaviour
    {
        bool isAttacking = false;
        private PlayerFrontCheck playerFrontCheck;
        private PlayerStatusSQL playerStatusSQL;
        public SystemText systemText;

        private void Start()
        {
            playerFrontCheck = GetComponentInChildren<PlayerFrontCheck>();
            playerStatusSQL = GetComponent<PlayerStatusSQL>();
        }
        public void AttackStance()
        {
            isAttacking = true;
            systemText.TextSet("Attack!");

            playerFrontCheck.Attacked();

            playerStatusSQL.IsPlayerActive();//Attack ends in 1 frame. take enemy turn

            isAttacking = false;
        }

        public bool IsAttacking()
        {
            return isAttacking;
        }
    }
}