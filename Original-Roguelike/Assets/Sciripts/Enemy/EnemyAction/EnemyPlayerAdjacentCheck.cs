using UnityEngine;
using PlayerStatusList;

namespace Enemy
{
    public class EnemyPlayerAdjacentCheck : MonoBehaviour
    {
        public Collider Playercollider;
        public bool isAttackHit;
        private EnemyStatus enemyStatus;


        public void Start()
        {
            enemyStatus = GetComponentInParent<EnemyStatus>();
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                Playercollider = collider;
                isAttackHit = true;
            }
        }

        private void OnTriggerExit(Collider collider)
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                Playercollider = null;
                isAttackHit = false;
            }
        }

        public bool IsAttackHit()
        {
            return isAttackHit;
        }


        public void Attacked()
        {
            PlayerHP playerHP = Playercollider.GetComponent<PlayerHP>();
            EnemyData enemy = EnemyDataCache.GetEnemyData(enemyStatus.EnemyID);
            playerHP.TakeDamage(enemy.Attack);
        }
    }
}