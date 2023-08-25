using ItemSystemV2.Inventory;
using UnityEngine;

namespace ItemSystemV2
{
    public class GetItemV2 : MonoBehaviour
    {
        [SerializeField] private int itemId;
        public int num;

        private void GetItem(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                SQLInventoryAddV2 playerInventoryV2 = other.GetComponent<SQLInventoryAddV2>();

                bool ItemGet = playerInventoryV2.AddItem(itemId, num);

                if (ItemGet == false) Debug.Log("éùÇøï®Ç™Ç¢Ç¡ÇœÇ¢ÇæÇÊÅI");
                if (ItemGet == true) Destroy(gameObject);
            }
        }
    }
}