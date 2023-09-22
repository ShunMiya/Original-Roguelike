using System.Collections;
using UnityEngine;
using Field;
using ItemSystemV2;
using Unity.VisualScripting;
using EnemySystem;

namespace MoveSystem
{
    public class MoveAction : MonoBehaviour
    {
        private float gridSize = GameRule.GridSize;
        private Vector3 InputPos;
        private Vector3 targetPos;
        public Pos2D grid = new Pos2D();
        public Pos2D newGrid = null;

        public float ThrowMoveFrame = 15f;
        [SerializeField]private float complementFrame;
        private Areamap field;

        private void Awake ()
        {
            field = GetComponentInParent<Areamap>();
            complementFrame = GameRule.MoveSpeed;
            //complementFrame = maxPerFrame / Time.deltaTime;
            newGrid = grid;
        }

        public bool MoveStance(float movex, float movez)
        {
            targetPos = transform.position;
            InputPos = new Vector3(movex * gridSize, 0, movez * gridSize);
            if (InputPos == new Vector3(0, 0, 0)) return false;
            
            targetPos += InputPos;
            transform.LookAt(targetPos);
            newGrid = MovePointCheck(field, grid,targetPos);
            if (newGrid == grid) return false;
            MoveObjects moveObjects = FindObjectOfType<MoveObjects>();
            if (moveObjects != null)
            {
                complementFrame = GameRule.MoveSpeed;
                moveObjects.objectsToMove.Add(this);
            }
            return true;
        }

        /**
        * * 補完で計算して進む
        */
        public IEnumerator MoveObjectCoroutine(Transform objTransform)
        {
            float px1 = CoordinateTransformation.ToWorldX(grid.x);
            float pz1 = CoordinateTransformation.ToWorldZ(grid.z);
            float px2 = CoordinateTransformation.ToWorldX(newGrid.x);
            float pz2 = CoordinateTransformation.ToWorldZ(newGrid.z);

            for (int currentFrame = 0; currentFrame <= complementFrame; currentFrame++)
            {
                float t = (float)currentFrame / complementFrame;
                float newX = px1 + (px2 - px1) * t;
                float newZ = pz1 + (pz2 - pz1) * t;
                transform.position = new Vector3(newX, 0, newZ);

                yield return new WaitForEndOfFrame();
            }
            transform.position = new Vector3(px2, 0, pz2);
            grid = newGrid;
        }

        private Pos2D MovePointCheck(Areamap field, Pos2D Currentgrid, Vector3 targetPos)
        {
            Pos2D newP = new Pos2D();
            newP.x = (int)targetPos.x;
            newP.z = (int)targetPos.z;
            if (newP.x != Currentgrid.x && newP.z != Currentgrid.z) //斜め移動時左右の障害物判定も取る
            {
                if (field.IsCollidediagonal(Currentgrid.x, newP.z) || field.IsCollidediagonal(newP.x, Currentgrid.z) || field.IsCollide(newP.x, newP.z)) return Currentgrid;
            }
            else
            {
                if (field.IsCollide(newP.x, newP.z)) return Currentgrid;
            }

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
            newGrid = grid;
        }

        public IEnumerator ThrowMove(int movex, int movez)
        {
            if (field.IsCollidediagonal(movex, movez))
            {
                bool Throw = false;
                yield return Throw;
                yield break;
            }
            GameObject Char = field.IsCollideReturnCharObj(movex, movez);
            if (Char != null)
            {
                Char.GetComponent<EnemyStatusV2>().TakeDamage(1, 1, GameRule.HitRate);
                bool Throw = false;
                yield return Throw;
                Destroy(gameObject);
                yield break;
            }

            newGrid = new Pos2D { x = movex, z = movez };

            yield return StartCoroutine(MoveObjectCoroutine(transform));
            yield return true;
        }

        public void SetcomplementFrame()
        {
            complementFrame = ThrowMoveFrame;
        }
    }
}