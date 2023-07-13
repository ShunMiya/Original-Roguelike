using UnityEngine;
using Enemy;

namespace PlayerFrontChecker
{
    public class PlayerFrontCheck : MonoBehaviour
    {
        public Collider Enemycollider;
        public bool isAttackHit;

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.CompareTag("Enemy"))
            {
                Enemycollider = collider;
                isAttackHit = true;
            }
        }

        private void OnTriggerExit(Collider collider)
        {
            if (collider.gameObject.CompareTag("Enemy"))
            {
                Enemycollider = null;
                isAttackHit = false;
            }
        }

        public void EnemyDestroy()
        {
            Enemycollider = null;
            isAttackHit = false;
        }

        public bool IsAttackHitCheck()
        {
            return isAttackHit;
        }

        public void Attacked(float damage)
        {
            EnemyStatus enemyStatus = Enemycollider.GetComponent<EnemyStatus>();

            enemyStatus.TakeDamage(damage);
        }
    }
}