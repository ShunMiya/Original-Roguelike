using ItemSystemV2.Inventory;
using UnityEngine;
using System.Collections;
using UISystemV2;
using Field;
using Performances;

namespace ItemSystemV2
{
    public class SteppedOnEvent : MonoBehaviour
    {
        public int ObjType;
        public int Id;
        public int num;
        private PauseSystemV2 pauseSystemV2;
        private FieldSoundEffect fieldSoundEffect;

        private void Start ()
        {
            pauseSystemV2 = FindObjectOfType<PauseSystemV2>();
            fieldSoundEffect = FindObjectOfType<FieldSoundEffect>();
        }
        public IEnumerator Event()
        {
            switch (ObjType)
            {
                case 0:
                    GetItem();
                    break;
                case 1:
                    yield return StartCoroutine(Trap());
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
            
            bool ItemGet = playerInventoryV2.AddItem(Id, num);

            if (ItemGet == true)
            {
                fieldSoundEffect.GimmickSE(7);
                Destroy(gameObject);
            }

        }

        private IEnumerator Trap()
        {
            GimmickEvent GE = GetComponent<GimmickEvent>();

            yield return StartCoroutine(GE.Event(Id));

            GimmickData trapData = GimmickDataCache.GetGimmickData(Id);
            int BrakeCheck = Random.Range(1, 101);
            if(BrakeCheck <= trapData.BrakeRate) Destroy(gameObject);

        }

        private IEnumerator Stairs()
        {
            yield return StartCoroutine(pauseSystemV2.StairsMenuOpen());
        }
    }
}