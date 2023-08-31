using ItemSystemV2.Inventory;
using UnityEngine;
using System.Collections;

namespace ItemSystemV2
{
    public class SteppedOnEvent : MonoBehaviour
    {
        [SerializeField] private int ObjType;
        [SerializeField] private int itemId;
        private GameObject StairsMenu;
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
                case 2:
                    yield return StartCoroutine(AreaChange());
                    break;
            }
            yield return null;
        }

        private void GetItem()
        {
            SQLInventoryAddV2 playerInventoryV2 = FindObjectOfType<SQLInventoryAddV2>();
            
            bool ItemGet = playerInventoryV2.AddItem(itemId, num);
            
            if (ItemGet == false) Debug.Log("持ち物がいっぱいだよ！");
            if (ItemGet == true) Destroy(gameObject);
        }

        private void Trap()
        {

        }

        private IEnumerator AreaChange()
        {
            StairsMenu.SetActive(true);

            // StairsMenu が非アクティブになるまで待機する
            while (StairsMenu.activeSelf)
            {
                yield return null;
            }
        }
    }
}