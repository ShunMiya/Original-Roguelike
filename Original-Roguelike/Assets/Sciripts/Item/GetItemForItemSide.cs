using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItemSystem
{
    public class GetItemForItemSide : MonoBehaviour
    {
        [SerializeField] private string itemId;
        [SerializeField] private int itemStack;
        [SerializeField] private PlayerInventoryDataBase inventoryData;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                PlayerGetItem playerGetItem = other.GetComponent<PlayerGetItem>();

                bool ItemGet =playerGetItem.GetItem(itemId , itemStack);

                if (ItemGet == false) Debug.Log("持ち物がいっぱいだよ！");

                if(ItemGet == true) Destroy(gameObject);
            }
        }
    }
}