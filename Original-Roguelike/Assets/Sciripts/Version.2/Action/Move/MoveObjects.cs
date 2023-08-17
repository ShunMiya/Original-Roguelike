using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoveSystem
{
    public class MoveObjects : MonoBehaviour
    {
        public List<MoveAction> objectsToMove = new List<MoveAction>();

        public IEnumerator MoveAllObjects()
        {
            List<Coroutine> moveCoroutines = new List<Coroutine>();

            foreach (MoveAction moveAction in objectsToMove)
            {
                Coroutine coroutine = StartCoroutine(moveAction.MoveObjectCoroutine(transform));
                moveCoroutines.Add(coroutine);
            }

            foreach (Coroutine coroutine in moveCoroutines)
            {
                yield return coroutine;
            }

            // 全ての移動が完了した後の処理
            objectsToMove.Clear();
        }
    }
}