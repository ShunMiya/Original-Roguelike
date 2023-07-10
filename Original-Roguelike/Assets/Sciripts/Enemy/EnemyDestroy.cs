using ItemSystemSQL;
using UnityEngine;

namespace Enemy
{
    public class EnemyDestroy : MonoBehaviour
    {
        [SerializeField]private ItemFactory itemFactory;

        public void DropItem()
        {
            // エネミーが撃破された位置
            Vector3 dropPosition = transform.position;

            // アイテムの生成と配置
            itemFactory.ItemCreate(dropPosition);
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}