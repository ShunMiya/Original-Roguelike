using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItemSystem
{
    public class GetItemForItemSide : MonoBehaviour
    {
        [SerializeField] private string itemId;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                PlayerGetItem playerGetItem = other.GetComponent<PlayerGetItem>();
                ItemData itemData = ItemManager.Instance.GetItemDataById(itemId);

                playerGetItem.GetItem(itemData);

//                Debug.Log(itemData.ItemName + "‚ðŽæ“¾");

                Destroy(gameObject);
            }
        }
    }
}