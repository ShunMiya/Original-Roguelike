using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItemSystem
{
    public class HealPixel : MonoBehaviour
    {
        [SerializeField] private ItemData itemData;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log(itemData.ItemName + "‚ðŽæ“¾");

                Destroy(gameObject);
            }
        }
    }

}
