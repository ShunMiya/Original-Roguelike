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
            //Status��ID������Cache�̍s���^�C�v���m�F������ɉ����ď�������B
            switch(enemy.AIType)
            {
                case 0: //��{�ړ�
                    break;
                case 1: //����
                    break;
                case 2: //�s��
                    break;
                case 3: //�C����
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