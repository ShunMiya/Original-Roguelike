using ItemSystemSQL.Inventory;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace ItemSystem
{
    public class PlayerGetItemSQL : MonoBehaviour
    {
        [SerializeField] private SQLInventoryAdd SQLinventoryadd;

        public bool GetItem(int itemId, int num)
        {
            bool ItemGet = SQLinventoryadd.AddItem(itemId, num);

            return ItemGet;
        }

    }
}