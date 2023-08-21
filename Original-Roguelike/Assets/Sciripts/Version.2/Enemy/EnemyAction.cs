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
            //Status��ID������Cache�̍s���^�C�v���m�F������ɉ����ď�������B
            switch(enemy.AIType)
            {
                case 0: //��{�ړ�
                    break;
                case 1: //����
                    break;
                case 2: //�s��
                    NoMove();
                    break;
                case 3: //�C����
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