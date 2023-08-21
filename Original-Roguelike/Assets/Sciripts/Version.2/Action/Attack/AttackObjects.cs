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

            // ‘S‚Ä‚Ìs“®‚ªŠ®—¹‚µ‚½Œã‚Ìˆ—
            objectsToAttack.Clear();
        }
    }
}