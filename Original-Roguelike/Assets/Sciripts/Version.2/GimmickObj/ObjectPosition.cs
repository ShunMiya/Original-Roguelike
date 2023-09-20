using UnityEngine;

namespace Field
{
    public class ObjectPosition : MonoBehaviour
    {
        public Pos2D grid = new Pos2D();
        public Rect2D range = new Rect2D(0, 0, 0, 0);

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
            transform.position = new Vector3(CoordinateTransformation.ToWorldX(xgrid), (float)-0.5, CoordinateTransformation.ToWorldZ(zgrid));
        }
        public void SetRange(int left, int right, int width, int height)
        {
            range.left = left;
            range.right = right;
            range.right = left + width - 1;
            range.bottom = right + height - 1;
        }
    }
}