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

        private void Start()
        {
            field = GetComponentInParent<Areamap>();
            move = GetComponent<MoveAction>();
            oldgrid = move.grid;
        }

        public IEnumerator EventCheck()
        {
            if (move.grid != oldgrid)
            {
                GameObject AreaObj = field.IsCollideReturnAreaObj(move.grid.x, move.grid.z);
                if (AreaObj != null)
                {
                    SteppedOnEvent ObjEvent = AreaObj.GetComponent<SteppedOnEvent>();
                    yield return StartCoroutine(ObjEvent.Event());
                }
            }
            oldgrid = move.grid;
        }
    }
}