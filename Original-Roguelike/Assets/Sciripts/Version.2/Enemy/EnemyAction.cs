using MoveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AttackSystem;

namespace EnemySystem
{
    public class EnemyAction : MonoBehaviour
    {
        [SerializeField] private int ActionAI;
        public MoveAction moveAction;
        public AttackAction attackAction;
        public void EnemyActionSet()
        {
            //StatusのIDを元にDBの行動タイプを確認しそれに応じて処理分岐。
            switch(ActionAI)
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                default:
                    AllRandom();
                    break;
            }
        }

        private void AllRandom()
        {
            Vector3 PosRota = DirUtil.SetNewPosRotation(DirUtil.RandomDirection());
            transform.rotation = Quaternion.Euler(0, PosRota.y, 0);
            if (Random.Range(0, 3) > 0)
            {
                moveAction.MoveStance(PosRota.x, PosRota.z);
                return;
            }
            attackAction.AttackStance();
        }
    }
}