using ItemSystemSQL;
using UnityEngine;

namespace Enemy
{
    public class EnemyDestroy : MonoBehaviour
    {
        [SerializeField]private ItemFactory itemFactory;

        public void DropItem()
        {
            // �G�l�~�[�����j���ꂽ�ʒu
            Vector3 dropPosition = transform.position;

            // �A�C�e���̐����Ɣz�u
            itemFactory.ItemCreate(dropPosition);
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}