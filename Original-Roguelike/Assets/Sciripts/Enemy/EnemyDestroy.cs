using ItemSystemSQL;
using UnityEngine;

namespace Enemy
{
    public class EnemyDestroy : MonoBehaviour
    {
        [SerializeField]private ItemFactory itemFactory;
        private EnemyTurnStart enemyTurnStart;

        public void DropItem()
        {
            Vector3 dropPosition = transform.position;

            itemFactory.ItemCreate(dropPosition);
        }

        public void Destroy()
        {
            enemyTurnStart = GetComponentInParent<EnemyTurnStart>();

            enemyTurnStart.RemoveEnemy(gameObject);
            Destroy(gameObject);
        }
    }
}