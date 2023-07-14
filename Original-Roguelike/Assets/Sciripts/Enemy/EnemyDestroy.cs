using ItemSystemSQL;
using UnityEngine;

namespace Enemy
{
    public class EnemyDestroy : MonoBehaviour
    {
        [SerializeField]private ItemFactory itemFactory;

        public void DropItem()
        {
            Vector3 dropPosition = transform.position;

            itemFactory.ItemCreate(dropPosition);
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}