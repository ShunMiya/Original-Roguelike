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

            // �S�Ă̍s��������������̏���
            objectsToAttack.Clear();
        }
    }

}
