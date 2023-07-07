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

                if (ItemGet == false) Debug.Log("‚¿•¨‚ª‚¢‚Á‚Ï‚¢‚¾‚æI");

                if(ItemGet == true) Destroy(gameObject);
            }
        }
    }
}