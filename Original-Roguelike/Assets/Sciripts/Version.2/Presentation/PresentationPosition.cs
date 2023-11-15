using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Field
{
    public class PresentationPosition : MonoBehaviour
    {
        public Pos2D grid = new Pos2D();


        // インスペクターの値が変わった時に呼び出される
        void OnValidate()
        {
            if (grid.x != CoordinateTransformation.ToGridX(transform.position.x) || grid.z != CoordinateTransformation.ToGridZ(transform.position.z))
            {
                transform.position = new Vector3(CoordinateTransformation.ToWorldX(grid.x), 0, CoordinateTransformation.ToWorldZ(grid.z));
            }
        }

        /*
         * * * 指定したグリッド座標に合わせて位置を変更する
         * */
        public void SetPosition(int xgrid, int zgrid)
        {
            grid.x = xgrid;
            grid.z = zgrid;
            transform.position = new Vector3(CoordinateTransformation.ToWorldX(xgrid), 0, CoordinateTransformation.ToWorldZ(zgrid));
        }
    }
}