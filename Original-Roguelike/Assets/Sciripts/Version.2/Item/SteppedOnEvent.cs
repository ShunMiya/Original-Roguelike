using ItemSystemV2.Inventory;
using UnityEngine;
using System.Collections;

namespace ItemSystemV2
{
    public class SteppedOnEvent : MonoBehaviour
    {
        [SerializeField] private int ObjType;
        [SerializeField] private int itemId;
        public int num;

        public IEnumerator Event()
        {
            switch (ObjType)
            {
                case 0:
                    GetItem();
                    break;
                case 1:
                    //Trap();
                    break;
            }
            yield return null;
        }

        private void GetItem()
        {
            SQLInventoryAddV2 playerInventoryV2 = FindObjectOfType<SQLInventoryAddV2>();
            
            bool ItemGet = playerInventoryV2.AddItem(itemId, num);
            
            if (ItemGet == false) Debug.Log("éùÇøï®Ç™Ç¢Ç¡ÇœÇ¢ÇæÇÊÅI");
            if (ItemGet == true) Destroy(gameObject);
        }
    }
}