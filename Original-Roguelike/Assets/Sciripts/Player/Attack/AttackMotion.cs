using UnityEngine;
using PlayerFrontChecker;


namespace Combat.AttackMotion
{
    public class AttackMotion : MonoBehaviour
    {
        [SerializeField]private int damage;
        bool isAttacking = false;
        private PlayerFrontCheck playerFrontCheck;

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
                playerFrontCheck.Attacked(damage);
            }
            isAttacking = false;
        }

        public bool IsAttacking()
        {
            return isAttacking;
        }
    }
}
