using ItemSystemSQL.Inventory;
using UnityEngine;

namespace ItemSystemSQL
{
    public class SQLDBGetItem : MonoBehaviour
    {
        [SerializeField] private int itemId;
        [SerializeField] private int num;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                SQLInventoryAdd SQLplayerInventory = other.GetComponent<SQLInventoryAdd>();

                bool ItemGet = SQLplayerInventory.AddItem(itemId, num);

                if (ItemGet == false) Debug.Log("�������������ς�����I");

                if (ItemGet == true) Destroy(gameObject);
            }
        }
    }
}