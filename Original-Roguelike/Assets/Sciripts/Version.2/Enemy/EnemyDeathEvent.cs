using DeathSystem;
using UnityEngine;

namespace EnemySystem
{
    public class EnemyDeathEvent : MonoBehaviour
    {
        private DeathAction deathAction;
        private EnemyDestroyV2 enemyDestroy;

        void Start()
        {
            deathAction = GetComponent<DeathAction>();
            enemyDestroy = GetComponent<EnemyDestroyV2>();

            deathAction.EnemyDeath += enemyDestroy.DropItem;
            deathAction.EnemyDeath += enemyDestroy.Destroy;
        }
    }
}