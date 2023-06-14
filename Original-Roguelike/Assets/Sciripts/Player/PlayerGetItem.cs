using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace ItemSystem
{
    public class PlayerGetItem : MonoBehaviour
    {
        public PlayerInventoryDataBase playerInventory;

        public void GetItem(string itemId)
        {
            playerInventory.AddItem(itemId);
        }

    }
}
