using MoveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AttackSystem
{
    public class AttackObjects : MonoBehaviour
    {
        public List<MoveAction> objectsToAttack = new List<MoveAction>();

        public IEnumerator AttackAllObject()
        {
            foreach (MoveAction moveAction in objectsToAttack)
            {
                Coroutine coroutine = StartCoroutine(moveAction.MoveObjectCoroutine(transform));
                yield return coroutine;
            }

            // 全ての行動が完了した後の処理
            objectsToAttack.Clear();
        }
    }

}
