using Field;
using ItemSystemV2;
using MoveSystem;
using System.Collections;
using UnityEngine;

namespace PlayerV2
{
    public class PlayerEventAfterMove : MonoBehaviour
    {
        private Areamap field;
        private MoveAction move;

        private void Start()
        {
            field = GetComponentInParent<Areamap>();
            move = GetComponent<MoveAction>();
        }

        public IEnumerator EventCheck()
        {
            GameObject AreaObj = field.IsCollideReturnAreaObj(move.grid.x, move.grid.z);
            if (AreaObj == null) yield return null;

            else
            {
                SteppedOnEvent ObjEvent = AreaObj.GetComponent<SteppedOnEvent>();
                yield return StartCoroutine(ObjEvent.Event());
            }
        }
    }
}