using MoveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AttackSystem;

namespace EnemySystem
{
    public class EnemyAction : MonoBehaviour
    {
        public MoveAction moveAction;
        public AttackAction attackAction;
        private EnemyStatusV2 enemyStatus;
        private EnemyDataV2 enemy;

        public void Start()
        {
            enemyStatus = GetComponent<EnemyStatusV2>();
            enemy = EnemyDataCacheV2.GetEnemyData(enemyStatus.EnemyID);
        }
        public void EnemyActionSet()
        {
            //StatusのIDを元にCacheの行動タイプを確認しそれに応じて処理分岐。
            switch(enemy.AIType)
            {
                case 0: //基本移動
                    break;
                case 1: //逃走
                    break;
                case 2: //不動
                    break;
                case 3: //気紛れ
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
            AttackObjects attackObjects = FindObjectOfType<AttackObjects>();
            attackObjects.objectsToAttack.Add(attackAction);
        }
    }
}