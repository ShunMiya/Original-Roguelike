using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Field;

namespace PlayerV2
{
    public class PlayerMoveV2 : MonoBehaviour
    {
        float gridSize = GameRule.GridSize;
        private Vector3 InputPos;
        private Vector3 targetPos;
        public Pos2D grid = new Pos2D();
        private Pos2D newGrid = null;

        public float maxPerFrame = 1.67f;
        private float complementFrame;
        private int currentFrame = 0;

        private void Start ()
        {
            complementFrame = maxPerFrame / Time.deltaTime;
        }
        public void MoveStance(float movex, float movez)
        {
            if (currentFrame == 0)
            {
                targetPos = transform.position;
                InputPos = new Vector3(movex * gridSize, 0, movez * gridSize);
                if (InputPos == new Vector3(0, 0, 0)) return;

                targetPos += InputPos;
                transform.LookAt(targetPos);
                newGrid = MovePointCheck(GetComponentInParent<Areamap>(), grid,targetPos);
                grid = Move(grid, newGrid, ref currentFrame);
            }
            else grid = Move(grid, newGrid, ref currentFrame);
        }

        /**
        * * 補完で計算して進む
        */
        private Pos2D Move(Pos2D currentPos, Pos2D newPos, ref int frame)
        {
            float px1 = CoordinateTransformation.ToWorldX(currentPos.x);
            float pz1 = CoordinateTransformation.ToWorldZ(currentPos.z);
            float px2 = CoordinateTransformation.ToWorldX(newPos.x);
            float pz2 = CoordinateTransformation.ToWorldZ(newPos.z);
            frame += 1;
            float t = (float)frame / complementFrame;
            float newX = px1 + (px2 - px1) * t;
            float newZ = pz1 + (pz2 - pz1) * t;
            transform.position = new Vector3(newX, 0, newZ);
            if (complementFrame <= frame)
            {
                frame = 0;
                transform.position = new Vector3(px2, 0, pz2);
                return newPos;
            }
            return currentPos;
        }

        private Pos2D MovePointCheck(Areamap field, Pos2D position, Vector3 targetPos)
        {
            Pos2D newP = new Pos2D();
            newP.x = (int)targetPos.x;
            newP.z = (int)targetPos.z;
            if (field.IsCollide(newP.x, newP.z)) return position;
            return newP;
        }

        // インスペクターの値が変わった時に呼び出される
        void OnValidate()
        {
            if (grid.x != CoordinateTransformation.ToGridX(transform.position.x) || grid.z != CoordinateTransformation.ToGridZ(transform.position.z))
            {
                transform.position = new Vector3(CoordinateTransformation.ToWorldX(grid.x), 0, CoordinateTransformation.ToWorldZ(grid.z));
            }
        }

        /*
         * * 指定したグリッド座標に合わせて位置を変更する
         */
        public void SetPosition(int xgrid, int zgrid)
        {
            grid.x = xgrid;
            grid.z = zgrid;
            transform.position = new Vector3(CoordinateTransformation.ToWorldX(xgrid), 0, CoordinateTransformation.ToWorldZ(zgrid));
        }
    }
}