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
            int EnemyId = GetComponent<EnemyStatusV2>().EnemyID;
            EnemyDataV2 enemy = EnemyDataCacheV2.GetEnemyData(EnemyId);
            int DropProbability = enemy.DropProbability;

            if(Random.Range(1,101) <= DropProbability)
            {
                Pos2D dropPosition = GetComponent<MoveAction>().grid;

                itemFactory.RandomItemCreate(dropPosition);
            }
        }

        public void Destroy()
        {

            Destroy(gameObject);
        }
    }

}