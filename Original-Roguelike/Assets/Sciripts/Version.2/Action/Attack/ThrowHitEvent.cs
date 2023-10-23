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

        public IEnumerator Event(int Id, int Num)
        {
            if (Id == 0) yield return null;

            switch(Id)
            {
                case 202:
                    int HitObjx = GetComponent<MoveAction>().grid.x;
                    int HitObjz = GetComponent<MoveAction>().grid.z;
                    int Playerx = FindObjectOfType<PlayerStatusV2>().gameObject.GetComponent<MoveAction>().grid.x;
                    int Playerz = FindObjectOfType<PlayerStatusV2>().gameObject.GetComponent<MoveAction>().grid.z;

                    FindObjectOfType<PlayerStatusV2>().gameObject.GetComponent<MoveAction>().SetPosition(HitObjx, HitObjz);
                    GetComponent<MoveAction>().SetPosition(Playerx, Playerz);
                    break;
                case 203:
                    OffensiveDataV2 itemData = ItemDataCacheV2.GetOffensive(Id);

                    gameObject.GetComponent<EnemyStatusV2>().DirectDamage(itemData.DamageNum * Num, 1, GameRule.HitRate, FindObjectOfType<PlayerStatusV2>().gameObject);
                    break;
                default:
                    gameObject.GetComponent<EnemyStatusV2>().TakeDamage(1, 1, GameRule.HitRate, FindObjectOfType<PlayerStatusV2>().gameObject);

                    break;
            }
            yield return new WaitForEndOfFrame();

        }
    }
}