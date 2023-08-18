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
            //攻撃演出
            Debug.Log("Attack");

            //攻撃が当たっていたかチェック
            int R = (int)transform.rotation.eulerAngles.y;
            GameObject HitObj =GetComponentInParent<Areamap>().IsCollideHit(MA.grid, R, range);
            if (HitObj != null) //当たってたので当たった対象のダメージ処理＋演出
            {                
                if (HitObj.CompareTag("Player"))
                {
                    // プレイヤーにダメージを与える処理
                    HitObj.GetComponent<PlayerHPV2>().TakeDamage(damage);
                    //プレイヤーのダメージ演出
                }
                else if (HitObj.CompareTag("Enemy"))
                {
                    // 敵にダメージを与える処理
                    //HitObj.GetComponent<EnemyStatusV2>().TakeDamage();
                    // エネミーのダメージ演出
                }
            }
        }
    }
}
