using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItemSystem
{
    public class HealPixel : MonoBehaviour
    {
        [SerializeField] private string itemId;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();
                ItemData itemData = ItemManager.Instance.GetItemDataById(itemId);

                playerInventory.AddItem(itemData);

//                Debug.Log(itemData.ItemName + "‚ðŽæ“¾");

                Destroy(gameObject);
            }
        }
    }

}
