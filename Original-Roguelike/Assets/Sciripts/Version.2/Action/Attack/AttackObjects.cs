using MoveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AttackSystem
{
    public class AttackObjects : MonoBehaviour
    {
        public List<AttackAction> objectsToAttack = new List<AttackAction>();

        public IEnumerator AttackAllObject()
        {
            foreach (AttackAction AttackEnemy in objectsToAttack)
            {
                Coroutine coroutine = StartCoroutine(AttackEnemy.AttackPreparationEnemy(AttackEnemy.gameObject));
                yield return coroutine;
            }

            // 全ての行動が完了した後の処理
            objectsToAttack.Clear();
        }

        public void DeleteList()
        {
            objectsToAttack.Clear();
        }
    }
}