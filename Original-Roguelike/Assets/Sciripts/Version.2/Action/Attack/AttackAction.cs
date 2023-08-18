using Field;
using MoveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerStatusSystemV2;

namespace AttackSystem
{
    public class AttackAction : MonoBehaviour
    {
        public MoveAction MA;
        public int range; public int damage;

        private void Start()
        {
            MA = GetComponent<MoveAction>();
        }
        public void AttackStance()
        {
            //�U�����o
            Debug.Log("Attack");

            //�U�����������Ă������`�F�b�N
            int R = (int)transform.rotation.eulerAngles.y;
            GameObject HitObj =GetComponentInParent<Areamap>().IsCollideHit(MA.grid, R, range);
            if (HitObj != null) //�������Ă��̂œ��������Ώۂ̃_���[�W�����{���o
            {                
                if (HitObj.CompareTag("Player"))
                {
                    // �v���C���[�Ƀ_���[�W��^���鏈��
                    HitObj.GetComponent<PlayerHPV2>().TakeDamage(damage);
                    //�v���C���[�̃_���[�W���o
                }
                else if (HitObj.CompareTag("Enemy"))
                {
                    // �G�Ƀ_���[�W��^���鏈��
                    //HitObj.GetComponent<EnemyStatusV2>().TakeDamage();
                    // �G�l�~�[�̃_���[�W���o
                }
            }
        }
    }
}
