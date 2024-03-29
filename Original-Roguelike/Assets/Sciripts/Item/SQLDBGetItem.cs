using ItemSystemSQL.Inventory;
using UnityEngine;

namespace ItemSystemSQL
{
    public class SQLDBGetItem : MonoBehaviour
    {
        [SerializeField] private int itemId;
        public int num;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                SQLInventoryAdd SQLplayerInventory = other.GetComponent<SQLInventoryAdd>();

                bool ItemGet = SQLplayerInventory.AddItem(itemId, num);

                if (ItemGet == false)Debug.Log("持ち物がいっぱいだよ！");
                if (ItemGet == true) Destroy(gameObject);
            }
        }
    }
}