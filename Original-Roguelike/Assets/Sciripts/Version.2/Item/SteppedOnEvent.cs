using ItemSystemV2.Inventory;
using UnityEngine;
using System.Collections;
using UISystemV2;
using UnityEngine.EventSystems;

namespace ItemSystemV2
{
    public class SteppedOnEvent : MonoBehaviour
    {
        [SerializeField] private int ObjType;
        [SerializeField] private int itemId;
        public int num;
        private PauseSystemV2 pauseSystemV2;

        private void Start ()
        {
            pauseSystemV2 = FindObjectOfType<PauseSystemV2>();
        }
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
                case 2:
                    yield return StartCoroutine(Stairs());
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

        private void Trap()
        {

        }

        private IEnumerator Stairs()
        {
            yield return StartCoroutine(pauseSystemV2.StairsMenuOpen());
        }
    }
}