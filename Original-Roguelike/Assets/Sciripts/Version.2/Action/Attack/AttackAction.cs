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
            //攻撃演出
            //Debug.Log("Attack");

            //攻撃が当たっていたかチェック
            int R = (int)transform.rotation.eulerAngles.y;
            GameObject HitObj =GetComponentInParent<Areamap>().IsCollideHit(MA.grid, R, range);
            if (HitObj != null) //当たってたので当たった対象のダメージ処理＋演出
            {                
                if (HitObj.CompareTag("Player"))
                {
                    Debug.Log(damage + "を" + range + "の射程で被弾");
                    // プレイヤーにダメージを与える処理
                    HitObj.GetComponent<PlayerHPV2>().TakeDamage(damage);
                    //プレイヤーのダメージ演出
                }
                else if (HitObj.CompareTag("Enemy"))
                {
                    Debug.Log(damage+"を"+range+"の射程で命中");
                    // 敵にダメージを与える処理
                    //HitObj.GetComponent<EnemyStatusV2>().TakeDamage();
                    // エネミーのダメージ演出
                }
            }
        }
    }
}
