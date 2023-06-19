using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace ItemSystem
{
    public class PlayerGetItem : MonoBehaviour
    {
        public PlayerInventoryDataBase playerInventory;

        public void GetItem(string itemId , int itemStack)
        {
            playerInventory.AddItem(itemId , itemStack);
        }

    }
}