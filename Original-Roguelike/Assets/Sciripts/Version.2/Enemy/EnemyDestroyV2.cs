using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemySystem
{
    public class EnemyDestroyV2 : MonoBehaviour
    {
        /*[SerializeField] private ItemFactory itemFactory;

        public void DropItem()
        {
            Vector3 dropPosition = transform.position;

            itemFactory.ItemCreate(dropPosition);
        }*/

        public void Destroy()
        {

            Destroy(gameObject);
        }
    }

}