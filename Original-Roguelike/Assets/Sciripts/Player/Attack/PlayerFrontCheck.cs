using UnityEngine;
using Enemy;

namespace PlayerFrontChecker
{
    public class PlayerFrontCheck : MonoBehaviour
    {
        public Collider Enemycollider;
        public bool isMoveFail = false;
        public bool isAttackHit = false;

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.CompareTag("Wall") || collider.gameObject.CompareTag("Enemy"))
            {
                isMoveFail = true;
            }

            if (collider.gameObject.CompareTag("Enemy"))
            {
                Enemycollider = collider;
                isAttackHit = true;
            }
        }

        private void OnTriggerExit(Collider collider)
        {
            if (collider.gameObject.CompareTag("Wall") || collider.gameObject.CompareTag("Enemy"))
            {
                isMoveFail = false;
            }

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
            isMoveFail = false;
        }

        public bool IsMoveFailCheck()
        {
            return isMoveFail;
        }

        public bool IsAttackHitCheck()
        {
            return isAttackHit;
        }

        public void Attacked(int damage)
        {
            EnemyStatus enemyStatus = Enemycollider.GetComponent<EnemyStatus>();

            enemyStatus.TakeDamage(damage);
        }
    }
}