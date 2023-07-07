using UnityEngine;

namespace ItemSystem
{
    public class GetItemForItemSide : MonoBehaviour
    {
        [SerializeField] private int itemId;
        [SerializeField] private int itemStock;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                PlayerGetItem playerGetItem = other.GetComponent<PlayerGetItem>();

                bool ItemGet =playerGetItem.GetItem(itemId , itemStock);

                if (ItemGet == false) Debug.Log("持ち物がいっぱいだよ！");

                if(ItemGet == true) Destroy(gameObject);
            }
        }
    }
}