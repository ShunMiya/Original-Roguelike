using MoveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AttackSystem;
using Field;

namespace EnemySystem
{
    public class EnemyAction : MonoBehaviour
    {
        public MoveAction moveAction;
        public AttackAction attackAction;
        private EnemyStatusV2 enemyStatus;
        private EnemyDataV2 enemy;
        private Areamap areamap;

        public void Start()
        {
            enemyStatus = GetComponent<EnemyStatusV2>();
            enemy = EnemyDataCacheV2.GetEnemyData(enemyStatus.EnemyID);
            areamap = FindObjectOfType<Areamap>();
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
                    NoMove();
                    break;
                case 3: //気紛れ
                    break;
                default:
                    AllRandom();
                    break;
            }
        }

        private void NoMove()
        {
            Vector3 d = areamap.IsPlayerHitCheckBeforeMoving(moveAction.grid, enemy.Range);
            if (d == new Vector3 (0,0,0)) return;
            attackAction.EnemyY = (int)d.y;
            AttackObjects attackObjects = FindObjectOfType<AttackObjects>();
            attackObjects.objectsToAttack.Add(attackAction);

        }

        private void AllRandom()
        {
            Vector3 PosRota = DirUtil.SetNewPosRotation(DirUtil.RandomDirection());
            if (Random.Range(0, 3) > 0)
            {
                transform.rotation = Quaternion.Euler(0, PosRota.y, 0);
                moveAction.MoveStance(PosRota.x, PosRota.z);
                return;
            }
            attackAction.EnemyY = (int)PosRota.y;
            AttackObjects attackObjects = FindObjectOfType<AttackObjects>();
            attackObjects.objectsToAttack.Add(attackAction);
        }
    }
}