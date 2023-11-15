using EnemySystem;
using ItemSystemV2;
using MoveSystem;
using PlayerStatusSystemV2;
using System;
using System.Collections;
using UnityEngine;

namespace AttackSystem
{
    public class ThrowHitEvent : MonoBehaviour
    {

        public IEnumerator Event(int Id, int Num, int R, int DamageNum)
        {
            switch (Id)
            {
                case 0:
                    EnemyThrowHit(DamageNum);
                    break;
                case 201:
                    OffensiveDataV2 itemData = ItemDataCacheV2.GetOffensive(Id);
                    yield return StartCoroutine(GetComponent<MoveAction>().ThrowStance(R, itemData.DamageNum, FindObjectOfType<PlayerStatusV2>().gameObject));
                    break;
                case 202:
                    int HitObjx = GetComponent<MoveAction>().grid.x;
                    int HitObjz = GetComponent<MoveAction>().grid.z;
                    int Playerx = FindObjectOfType<PlayerStatusV2>().gameObject.GetComponent<MoveAction>().grid.x;
                    int Playerz = FindObjectOfType<PlayerStatusV2>().gameObject.GetComponent<MoveAction>().grid.z;

                    FindObjectOfType<PlayerStatusV2>().gameObject.GetComponent<MoveAction>().SetPosition(HitObjx, HitObjz);
                    GetComponent<MoveAction>().SetPosition(Playerx, Playerz);
                    break;
                case 203:
                    itemData = ItemDataCacheV2.GetOffensive(Id);

                    gameObject.GetComponent<EnemyStatusV2>().DirectDamage(itemData.DamageNum * Num, 1, GameRule.HitRate, FindObjectOfType<PlayerStatusV2>().gameObject);
                    break;
                default:
                    gameObject.GetComponent<EnemyStatusV2>().TakeDamage(1, 1, GameRule.HitRate, FindObjectOfType<PlayerStatusV2>().gameObject, 0);

                    break;
            }

            yield return new WaitForSeconds(0.3f);

        }

        public void EnemyThrowHit(int DamageNum)
        {
            switch(gameObject.tag)
            {
                case "Player":
                    gameObject.GetComponent<PlayerHPV2>().TakeDamage(DamageNum, 1, GameRule.HitRate, 0);
                    break;
                case "Enemy":
                    gameObject.GetComponent<EnemyStatusV2>().TakeDamage(DamageNum, 1, GameRule.HitRate, gameObject, 0);
                    break;
            }
        }
    }
}