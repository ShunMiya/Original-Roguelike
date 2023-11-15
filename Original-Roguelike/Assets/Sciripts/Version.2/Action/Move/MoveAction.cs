using System.Collections;
using UnityEngine;
using Field;
using EnemySystem;
using PlayerStatusSystemV2;
using AttackSystem;
using ItemSystemV2;

namespace MoveSystem
{
    public class MoveAction : MonoBehaviour
    {
        private float gridSize = GameRule.GridSize;
        private Vector3 InputPos;
        private Vector3 targetPos;
        public Pos2D grid = new Pos2D();
        public Pos2D newGrid = null;

        private float complementFrame;
        private Areamap field;

        private void Awake ()
        {
            field = GetComponentInParent<Areamap>();
            complementFrame = GameRule.MoveSpeed;
            newGrid = grid;
        }

        public void ChangeDirectionOnTheSpot(float movex, float movez)
        {
            targetPos = transform.position;
            InputPos = new Vector3(movex * gridSize, 0, movez * gridSize);

            targetPos += InputPos;
            transform.LookAt(targetPos);
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

            int numFrames = Mathf.CeilToInt(complementFrame / Time.fixedDeltaTime);

            for (int currentFrame = 0; currentFrame <= numFrames; currentFrame++)
            {
                float t = (float)currentFrame / numFrames;
                float newX = px1 + (px2 - px1) * t;
                float newZ = pz1 + (pz2 - pz1) * t;
                transform.position = new Vector3(newX, 0, newZ);

                yield return new WaitForFixedUpdate();
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

        public void SetPositionItem(int xgrid, int zgrid)
        {
            grid.x = xgrid;
            grid.z = zgrid;
            transform.position = new Vector3(CoordinateTransformation.ToWorldX(xgrid), (float)-0.45, CoordinateTransformation.ToWorldZ(zgrid));
            newGrid = grid;
        }

        public IEnumerator ThrowMove(int R, int movex, int movez)
        {
            if (field.IsCollidediagonal(movex, movez) || (field.IsCollidediagonal(grid.x, movez) && field.IsCollidediagonal(movex, grid.z)))
            {
                bool Throw = false;
                yield return Throw;
                GameRule.WalkMove();
                yield break;
            }
            GameObject Char = field.IsCollideReturnCharObj(movex, movez);
            if (Char != null)
            {
                int Id =gameObject.GetComponent<ThrowObjData>().Id;
                int Num = gameObject.GetComponent<ThrowObjData>().Num;
                int DamageNum = gameObject.GetComponent<ThrowObjData>().DamageNum;
                GetComponent<Renderer>().enabled = false;
                yield return StartCoroutine(Char.GetComponent<ThrowHitEvent>().Event(Id, Num, R, DamageNum));
                bool Throw = false;
                yield return Throw;
                GameRule.WalkMove();
                Destroy(gameObject);
                yield break;
            }

            newGrid = new Pos2D { x = movex, z = movez };

            yield return StartCoroutine(MoveObjectCoroutine(transform));
            yield return true;
        }

        public IEnumerator ThrowStance(int R, int range, GameObject attacker)
        {
            SetcomplementFrame();
            Pos2D Pos = DirUtil.SetAttackPoint(R);
            int xgrid = grid.x;
            int zgrid = grid.z;
            xgrid += Pos.x;
            zgrid += Pos.z;

            for (int i = 1; i <= range; i++)
            {
                IEnumerator Coroutine = ThrowMoveChar(xgrid, zgrid, attacker);
                yield return StartCoroutine(Coroutine);

                bool Throw = (bool)Coroutine.Current;
                if (!Throw)
                {
                    yield break;
                }

                xgrid += Pos.x;
                zgrid += Pos.z;
            }

            SetPosition(grid.x, grid.z);
        }

        public IEnumerator ThrowMoveChar(int movex, int movez, GameObject attacker)
        {

            if (field.IsCollidediagonal(movex, movez) || field.IsCollidediagonal(grid.x, movez) || field.IsCollidediagonal(movex, grid.z))
            {
                bool Throw = false;
                yield return Throw;

                if (gameObject.CompareTag("Player")) GetComponent<PlayerHPV2>().DirectDamage(GameRule.CharaThrowHitDamage);
                else if (gameObject.CompareTag("Enemy")) GetComponent<EnemyStatusV2>().DirectDamage(GameRule.CharaThrowHitDamage, 1, 100, attacker);

                yield break;
            }
            GameObject Char = field.IsCollideReturnCharObj(movex, movez);
            if (Char != null)
            {
                bool Throw = false;
                yield return Throw;

                if (gameObject.CompareTag("Player")) GetComponent<PlayerHPV2>().DirectDamage(GameRule.CharaThrowHitDamage);
                else if (gameObject.CompareTag("Enemy")) GetComponent<EnemyStatusV2>().DirectDamage(GameRule.CharaThrowHitDamage, 1, 100, attacker);

                if (Char.CompareTag("Player")) Char.GetComponent<PlayerHPV2>().DirectDamage(GameRule.CharaThrowHitDamage);
                else if (Char.CompareTag("Enemy")) Char.GetComponent<EnemyStatusV2>().DirectDamage(GameRule.CharaThrowHitDamage, 1, 100, attacker);

                yield break;
            }

            newGrid = new Pos2D { x = movex, z = movez };

            yield return StartCoroutine(MoveObjectCoroutine(transform));
            yield return true;
        }

        public void SetcomplementFrame()
        {
            GameRule.ThrowMove();
            complementFrame = GameRule.MoveSpeed;
        }
    }
}