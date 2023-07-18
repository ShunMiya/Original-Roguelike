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
            PlayerStatusSQL playerStatus = Playercollider.GetComponent<PlayerStatusSQL>();
            EnemyData enemy = EnemyDataCashe.GetEnemyData(enemyStatus.EnemyID);
            playerStatus.TakeDamage(enemy.Attack);
        }
    }
}
