using MoveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyObjects : MonoBehaviour
    {

        public void EnemiesActionSets()
        {
            foreach (var EA in GetComponentsInChildren<EnemyAction>())
            {
                EA.EnemyActionSet();
            }
        }
    }
}