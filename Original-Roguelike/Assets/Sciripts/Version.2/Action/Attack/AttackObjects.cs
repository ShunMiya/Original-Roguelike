using DeathSystem;
using MoveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AttackSystem
{
    public class AttackObjects : MonoBehaviour
    {
        public DeathObjects deathObjects;
        public List<AttackAction> objectsToAttack = new List<AttackAction>();

        public IEnumerator AttackAllObject()
        {
            foreach (AttackAction AttackEnemy in objectsToAttack)
            {
                Coroutine coroutine = StartCoroutine(AttackEnemy.AttackPreparationEnemy());
                yield return coroutine;
                yield return StartCoroutine(deathObjects.DeathAllObjects());

            }

            // �S�Ă̍s��������������̏���
            DeleteList();
        }

        public void DeleteList()
        {
            objectsToAttack.Clear();
        }
    }
}