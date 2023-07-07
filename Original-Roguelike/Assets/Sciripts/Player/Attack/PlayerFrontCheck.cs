using UnityEngine;

namespace PlayerFrontChecker
{
    public class PlayerFrontCheck : MonoBehaviour
    {
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
                isAttackHit = false;
            }
        }

        public bool IsMoveFailCheck()
        {
            return isMoveFail;
        }

        public bool IsAttackHitCheck()
        {
            return isAttackHit;
        }
    }
}
