using MoveSystem;
using System.Collections.Generic;
using UnityEngine;

namespace EnemySystem
{
    public class EnemyObjects : MonoBehaviour
    {
        public GameObject player;
        public void EnemiesActionSets()
        {
            EnemyAction[] src = GetComponentsInChildren<EnemyAction>();
            List<EnemyAction> enemies = new List<EnemyAction>();
            enemies.AddRange(src);
            Pos2D pgrid = player.GetComponent<MoveAction>().grid;
            System.Comparison<EnemyAction> p = (a, b) =>
            {
                Pos2D agrid = a.GetComponent<MoveAction>().grid;
                Pos2D bgrid = b.GetComponent<MoveAction>().grid;
                int p_a = Mathf.Abs(agrid.x - pgrid.x) + Mathf.Abs(agrid.z - pgrid.z);
                int p_b = Mathf.Abs(bgrid.x - pgrid.x) + Mathf.Abs(bgrid.z - pgrid.z);
                return p_a - p_b;
            };
            enemies.Sort(p);
            foreach (var EA in enemies)
            {
                EA.EnemyActionSet();
            }
        }
    }
}