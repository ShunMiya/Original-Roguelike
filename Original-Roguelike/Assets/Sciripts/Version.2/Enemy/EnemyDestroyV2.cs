using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItemSystemV2;
using MoveSystem;

namespace EnemySystem
{
    public class EnemyDestroyV2 : MonoBehaviour
    {
        [SerializeField] private ItemFactoryV2 itemFactory;

        private void Start()
        {
            itemFactory = FindFirstObjectByType<ItemFactoryV2>();
        }

        public void DropItem()
        {
            Pos2D dropPosition = GetComponent<MoveAction>().grid;

            itemFactory.RandomItemCreate(dropPosition);
        }

        public void Destroy()
        {

            Destroy(gameObject);
        }
    }

}