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
        private Pos2D oldgrid;
        private GameObject AreaObj;

        private void Start()
        {
            field = GetComponentInParent<Areamap>();
            move = GetComponent<MoveAction>();
            oldgrid = move.grid;
        }

        public bool EventCheck()
        {
            if (move.grid != oldgrid)
            {
                AreaObj = field.IsCollideReturnAreaObj(move.grid.x, move.grid.z);
            }
            oldgrid = move.grid;

            return (AreaObj != null);
        }

        public IEnumerator EventStart()
        {
            if (AreaObj != null)
            {
                SteppedOnEvent ObjEvent = AreaObj.GetComponent<SteppedOnEvent>();
                yield return StartCoroutine(ObjEvent.Event());
            }
        }
    }
}