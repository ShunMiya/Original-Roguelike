using Field;
using MoveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerStatusSystemV2;
using System;
using ItemSystemV2.Inventory;

namespace AttackSystem
{
    public class AttackAction : MonoBehaviour
    {
        private MoveAction MA;
        private SqliteDatabase sqlDB;

        private void Start()
        {
            MA = GetComponent<MoveAction>();
        }

        public void AttackPreparationPlayer()
        {
            if(sqlDB == null)
            {
                string databasePath = SQLDBInitializationV2.GetDatabasePath();
                sqlDB = new SqliteDatabase(databasePath);
            }
            string query = "SELECT Attack FROM PlayerStatus WHERE PlayerID = 1;";
            DataTable Data = sqlDB.ExecuteQuery(query);
            int attack = Convert.ToInt32(Data[0]["Attack"]);
            query = "SELECT AttackRange FROM PlayerStatus WHERE PlayerID = 1;";
            Data = sqlDB.ExecuteQuery(query);
            int range = Convert.ToInt32(Data[0]["AttackRange"]);
            AttackStance(attack, range);
        }

        public void AttackStance(int damage, int range)
        {
            //�U�����o
            //Debug.Log("Attack");

            //�U�����������Ă������`�F�b�N
            int R = (int)transform.rotation.eulerAngles.y;
            GameObject HitObj =GetComponentInParent<Areamap>().IsCollideHit(MA.grid, R, range);
            if (HitObj != null) //�������Ă��̂œ��������Ώۂ̃_���[�W�����{���o
            {                
                if (HitObj.CompareTag("Player"))
                {
                    Debug.Log(damage + "��" + range + "�̎˒��Ŕ�e");
                    // �v���C���[�Ƀ_���[�W��^���鏈��
                    HitObj.GetComponent<PlayerHPV2>().TakeDamage(damage);
                    //�v���C���[�̃_���[�W���o
                }
                else if (HitObj.CompareTag("Enemy"))
                {
                    Debug.Log(damage+"��"+range+"�̎˒��Ŗ���");
                    // �G�Ƀ_���[�W��^���鏈��
                    //HitObj.GetComponent<EnemyStatusV2>().TakeDamage();
                    // �G�l�~�[�̃_���[�W���o
                }
            }
        }
    }
}
