using ItemSystemSQL;
using UnityEngine;
using PlayerFrontChecker;

namespace Enemy
{
    public class EnemyEventHandler : MonoBehaviour
    {
        private EnemyStatus enemyStatus;
        private EnemyDestroy enemyDestroy;
        private PlayerFrontCheck playerfront;

        void Start()
        {
            enemyStatus = GetComponent<EnemyStatus>();
            enemyDestroy =GetComponent<EnemyDestroy>();
            playerfront = FindObjectOfType<PlayerFrontCheck>();

            enemyStatus.EnemyDefeated += enemyDestroy.DropItem;
            enemyStatus.EnemyDefeated += playerfront.EnemyDestroy;
            enemyStatus.EnemyDefeated += enemyDestroy.Destroy;
        }
    }
}