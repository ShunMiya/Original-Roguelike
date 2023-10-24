using ItemSystemV2.Inventory;
using PlayerStatusSystemV2;
using System;
using System.Collections;
using UISystemV2;
using UnityEngine;

namespace Field
{
    public class GimmickEvent : MonoBehaviour
    {
        private SystemTextV2 systemText;
        private SqliteDatabase sqlDB;
        private PlayerCondition playerCondition;

        private void Start()
        {
            systemText = FindObjectOfType<SystemTextV2>();
            playerCondition = FindObjectOfType<PlayerCondition>();
        }

        public IEnumerator Event(int num)
        {
            bool HitCheck = true;
            if (sqlDB == null)
            {
                string databasePath = SQLDBInitializationV2.GetDatabasePath();
                sqlDB = new SqliteDatabase(databasePath);
            }

            if(gameObject.transform.GetChild(0).gameObject.activeSelf)
            {
                if(UnityEngine.Random.Range(1, 11) >= 4)
                {
                    HitCheck = false;
                }
            }

            gameObject.transform.GetChild(0).gameObject.SetActive(true);


            switch (num)
            {
                case 1:
                    systemText.TextSet("���΂�㩂�!");
                    if(HitCheck == false)
                    {
                        systemText.TextSet("�����������N���Ȃ�����");
                        break;
                    }
                    string query = "SELECT CurrentHP FROM PlayerStatus WHERE PlayerID = 1;";
                    DataTable Data = sqlDB.ExecuteQuery(query);
                    int CurrentHP = Convert.ToInt32(Data[0]["CurrentHP"]);
                    int damage = (int)(CurrentHP * 0.7f);
                    PlayerHPV2 playerHPV2 = FindAnyObjectByType<PlayerHPV2>();
                    systemText.TextSet("<color=blue>Player</color>��" + damage + "�_���[�W���󂯂�!");
                    playerHPV2.DirectDamage(damage);
                    break;
                case 2:
                    systemText.TextSet("�󕠂�㩂�!");
                    if (HitCheck == false)
                    {
                        systemText.TextSet("�����������N���Ȃ�����");
                        break;
                    }
                    query = "SELECT CurrentHungry FROM PlayerStatus WHERE PlayerID = 1;";
                    Data = sqlDB.ExecuteQuery(query);
                    int CurrentHungry = Convert.ToInt32(Data[0]["CurrentHungry"]);
                    damage = UnityEngine.Random.Range(10, 20);
                    int newHungry = CurrentHungry - damage;
                    if (newHungry < 0) newHungry = 0;
                    string updateStatusQuery = "UPDATE PlayerStatus SET CurrentHungry = " + newHungry + " WHERE PlayerID = 1;";
                    systemText.TextSet("<color=blue>Player</color>�͋󕠒l��" + damage + "������!");
                    sqlDB.ExecuteNonQuery(updateStatusQuery);
                    break;
                case 3:
                    systemText.TextSet("�ł�㩂�!");
                    if (HitCheck == false)
                    {
                        systemText.TextSet("�����������N���Ȃ�����");
                        break;
                    }
                    systemText.TextSet("<color=blue>Player</color>�͓ŏ�ԂɂȂ���");
                    playerCondition.SetCondition(1, 5);
                    break;
                case 4:
                    systemText.TextSet("������㩂�!");
                    if (HitCheck == false)
                    {
                        systemText.TextSet("�����������N���Ȃ�����");
                        break;
                    }
                    systemText.TextSet("<color=blue>Player</color>�͍�����ԂɂȂ���");
                    playerCondition.SetCondition(2, 5);
                    break;
                case 5:
                    systemText.TextSet("�C���㩂�!");
                    if (HitCheck == false)
                    {
                        systemText.TextSet("�����������N���Ȃ�����");
                        break;
                    }
                    systemText.TextSet("<color=blue>Player</color>�͋C���ԂɂȂ���");
                    playerCondition.SetCondition(3, 3);
                    break;
                case 6:
                    systemText.TextSet("�Ӗڂ�㩂�!");
                    if (HitCheck == false)
                    {
                        systemText.TextSet("�����������N���Ȃ�����");
                        break;
                    }
                    systemText.TextSet("<color=blue>Player</color>�͖Ӗڏ�ԂɂȂ���");
                    playerCondition.SetCondition(4, 10);
                    break;

                default:
                    break;
            }

            yield return new WaitForSeconds(0.5f);
        }
    }
}