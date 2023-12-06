using Field;
using MoveSystem;
using System.Collections;
using UnityEngine;
using ADVSystem;

namespace PlayerV2
{
    public class ADVTrigeerCheck : MonoBehaviour
    {
        private Areamap field;
        private MoveAction move;
        private GameObject AreaObj;

        private void Start()
        {
            field = GetComponentInParent<Areamap>();
            move = GetComponent<MoveAction>();
        }

        public bool TriggerCheck()
        {
            AreaObj = null;
            
            AreaObj = field.IsCollideReturnADVTriggerObj(move.grid.x, move.grid.z);

            return (AreaObj != null);
        }

        public IEnumerator EventStart()
        {
            if (AreaObj != null)
            {
                ADVEvent advEvent = AreaObj.GetComponent<ADVEvent>();
                yield return StartCoroutine(advEvent.Event());
            }
        }
    }
}