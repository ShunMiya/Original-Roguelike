using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace ItemSystem
{
    public class PlayerGetItem : MonoBehaviour
    {
        public PlayerInventoryDataBase playerInventory;

        public bool GetItem(int itemId , int itemStack)
        {
            bool ItemGet =playerInventory.AddItem(itemId , itemStack);

            return ItemGet;
        }

    }
}