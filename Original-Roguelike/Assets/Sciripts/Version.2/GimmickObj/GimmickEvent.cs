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
                    systemText.TextSet("落石の罠だ!");
                    if(HitCheck == false)
                    {
                        systemText.TextSet("しかし何も起きなかった");
                        break;
                    }
                    string query = "SELECT CurrentHP FROM PlayerStatus WHERE PlayerID = 1;";
                    DataTable Data = sqlDB.ExecuteQuery(query);
                    int CurrentHP = Convert.ToInt32(Data[0]["CurrentHP"]);
                    int damage = (int)(CurrentHP * 0.7f);
                    PlayerHPV2 playerHPV2 = FindAnyObjectByType<PlayerHPV2>();
                    systemText.TextSet("<color=blue>Player</color>は" + damage + "ダメージを受けた!");
                    playerHPV2.DirectDamage(damage);
                    break;
                case 2:
                    systemText.TextSet("空腹の罠だ!");
                    if (HitCheck == false)
                    {
                        systemText.TextSet("しかし何も起きなかった");
                        break;
                    }
                    query = "SELECT CurrentHungry FROM PlayerStatus WHERE PlayerID = 1;";
                    Data = sqlDB.ExecuteQuery(query);
                    int CurrentHungry = Convert.ToInt32(Data[0]["CurrentHungry"]);
                    damage = UnityEngine.Random.Range(10, 20);
                    int newHungry = CurrentHungry - damage;
                    if (newHungry < 0) newHungry = 0;
                    string updateStatusQuery = "UPDATE PlayerStatus SET CurrentHungry = " + newHungry + " WHERE PlayerID = 1;";
                    systemText.TextSet("<color=blue>Player</color>は空腹値が" + damage + "減った!");
                    sqlDB.ExecuteNonQuery(updateStatusQuery);
                    break;
                case 3:
                    systemText.TextSet("毒の罠だ!");
                    if (HitCheck == false)
                    {
                        systemText.TextSet("しかし何も起きなかった");
                        break;
                    }
                    systemText.TextSet("<color=blue>Player</color>は毒状態になった");
                    playerCondition.SetCondition(1, 5);
                    break;
                case 4:
                    systemText.TextSet("混乱の罠だ!");
                    if (HitCheck == false)
                    {
                        systemText.TextSet("しかし何も起きなかった");
                        break;
                    }
                    systemText.TextSet("<color=blue>Player</color>は混乱状態になった");
                    playerCondition.SetCondition(2, 5);
                    break;
                case 5:
                    systemText.TextSet("気絶の罠だ!");
                    if (HitCheck == false)
                    {
                        systemText.TextSet("しかし何も起きなかった");
                        break;
                    }
                    systemText.TextSet("<color=blue>Player</color>は気絶状態になった");
                    playerCondition.SetCondition(3, 3);
                    break;
                case 6:
                    systemText.TextSet("盲目の罠だ!");
                    if (HitCheck == false)
                    {
                        systemText.TextSet("しかし何も起きなかった");
                        break;
                    }
                    systemText.TextSet("<color=blue>Player</color>は盲目状態になった");
                    playerCondition.SetCondition(4, 10);
                    break;

                default:
                    break;
            }

            yield return new WaitForSeconds(0.5f);
        }
    }
}