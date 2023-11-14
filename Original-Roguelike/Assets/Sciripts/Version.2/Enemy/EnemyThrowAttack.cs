using EnemySystem;
using ItemSystemV2;
using MoveSystem;
using System.Collections;
using UnityEngine;

namespace AttackSystem
{
    public class EnemyThrowAttack : MonoBehaviour
    {
        [SerializeField]private MoveAction move;

        public IEnumerator ThrowAttack(EnemyDataV2 enemy)
        {
            GameObject itemObj = (GameObject)Resources.Load("PrefabsV2/CannonBall");
            GameObject Obj = Instantiate(itemObj, transform);
            Obj.GetComponent<MoveAction>().SetcomplementFrame();
            Obj.GetComponent<MoveAction>().SetPosition(move.grid.x, move.grid.z);
            Obj.GetComponent<ThrowObjData>().Id = 0;
            Obj.GetComponent<ThrowObjData>().Num = 1;
            Obj.GetComponent<ThrowObjData>().DamageNum = enemy.Attack;

            yield return new WaitForEndOfFrame();

            int R = (int)transform.rotation.eulerAngles.y;
            if (R > 180) R -= 360;
            yield return StartCoroutine(Obj.GetComponent<MoveThrownItem>().ThrowAttackObj(R, enemy.Range));

        }
    }

}