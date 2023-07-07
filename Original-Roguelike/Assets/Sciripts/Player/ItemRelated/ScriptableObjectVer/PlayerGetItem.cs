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