using UnityEngine;

namespace EnemySystem
{
    public class EnemyDeathEvent : MonoBehaviour
    {
        private EnemyStatusV2 enemyStatus;
        private EnemyDestroyV2 enemyDestroy;

        void Start()
        {
            enemyStatus = GetComponent<EnemyStatusV2>();
            enemyDestroy = GetComponent<EnemyDestroyV2>();

            //enemyStatus.EnemyDefeated += enemyDestroy.DropItem;
            enemyStatus.EnemyDeath += enemyDestroy.Destroy;
        }
    }
}