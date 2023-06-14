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

                playerGetItem.GetItem(itemId);

                Destroy(gameObject);
            }
        }
    }
}