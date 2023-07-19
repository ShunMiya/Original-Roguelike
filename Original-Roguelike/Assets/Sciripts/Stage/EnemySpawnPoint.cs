using UnityEngine;

namespace Enemy
{
    public class EnemySpawnPoint : MonoBehaviour
    {
        [SerializeField]private EnemyFactory enemyFactory;

        private void Start()
        {
            enemyFactory.EnemyCreate(transform.position,transform.parent);
            Destroy(gameObject);
        }
    }
}