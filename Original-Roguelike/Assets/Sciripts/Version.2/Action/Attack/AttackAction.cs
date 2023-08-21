using Field;
using MoveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerStatusSystemV2;
using System;
using ItemSystemV2.Inventory;
using EnemySystem;

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

        public IEnumerator AttackPreparationPlayer()
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
            yield return StartCoroutine(AttackObjectCoroutine(attack, range));
        }

        public IEnumerator AttackPreparationEnemy(GameObject Enemy)
        {
            EnemyStatusV2 enemyStatus = GetComponent<EnemyStatusV2>();
            EnemyDataV2 enemy = EnemyDataCacheV2.GetEnemyData(enemyStatus.EnemyID);
            yield return StartCoroutine(AttackObjectCoroutine(enemy.Attack, enemy.Range));
        }

        public IEnumerator AttackObjectCoroutine(int damage, int range)
        {
            //攻撃演出
            Debug.Log("Attack");

            //攻撃が当たっていたかチェック
            int R = (int)transform.rotation.eulerAngles.y;
            if (R > 180)  R -= 360;
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
            yield return new WaitForSeconds(0.3f); //仮置き
        }
    }
}
