using Field;
using MoveSystem;
using System.Collections;
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
        private Vector3 originalPosition;
        private Vector3 targetPosition;
        private float elapsedTime = 0f;
        [SerializeField]private float moveDuration = 0.2f; // 移動にかける時間

        public int EnemyY;

        private void Start()
        {
            MA = GetComponent<MoveAction>();
        }

        public IEnumerator AttackPreparationPlayer()
        {
            if (sqlDB == null)
            {
                string databasePath = SQLDBInitializationV2.GetDatabasePath();
                sqlDB = new SqliteDatabase(databasePath);
            }
            string query = "SELECT * FROM PlayerStatus WHERE PlayerID = 1;";
            DataTable Data = sqlDB.ExecuteQuery(query);
            int attack = Convert.ToInt32(Data[0]["Attack"]);
            int range = Convert.ToInt32(Data[0]["AttackRange"]);
            int AttackType = Convert.ToInt32(Data[0]["AttackType"]);

            float CurrentHitRate = GameRule.HitRate;

            PlayerCondition PCondition = GetComponent<PlayerCondition>();
            if (PCondition.BlindTurn != 0)
            {
                PCondition.BlindEvent();
                CurrentHitRate -= 50;
            }
            yield return StartCoroutine(AttackObjectCoroutine(attack, range, CurrentHitRate, AttackType));
        }

        public IEnumerator AttackPreparationEnemy()
        {
            EnemyStatusV2 enemyStatus = GetComponent<EnemyStatusV2>();
            EnemyDataV2 enemy = EnemyDataCacheV2.GetEnemyData(enemyStatus.EnemyID);

            bool PHit = GetComponentInParent<Areamap>().IsPlayerHitCheckAfterMoving(MA.grid, EnemyY, enemy.Range);

            if (!PHit) yield break;

            transform.rotation = Quaternion.Euler(0, EnemyY, 0);

            float CurrentHitRate = GameRule.HitRate;

            switch(enemy.ThrowAttack)
            {
                case 1:
                    yield return StartCoroutine(GetComponent<EnemyThrowAttack>().ThrowAttack(enemy));
                    break;
                default:
                    yield return StartCoroutine(AttackObjectCoroutine(enemy.Attack, enemy.Range, CurrentHitRate, enemy.AttackType));
                    break;
            }
        }

        public IEnumerator AttackObjectCoroutine(int damage, int range, float HitRate, int AttackType)
        {
            yield return StartCoroutine(BeginAttack());  //攻撃開始演出

            //攻撃が当たっていたかチェック
            int R = (int)transform.rotation.eulerAngles.y;
            if (R > 180) R -= 360;
            GameObject HitObj = GetComponentInParent<Areamap>().IsCollideHit(MA.grid, R, range);
            if (HitObj != null) //当たってたので当たった対象のダメージ処理＋演出
            {
                switch(HitObj.tag)
                {
                    case "Player":
                        HitObj.GetComponent<PlayerHPV2>().TakeDamage(damage, R, HitRate, AttackType);
                        break;

                    case "Enemy":
                        HitObj.GetComponent<EnemyStatusV2>().TakeDamage(damage, R, HitRate, gameObject, AttackType);
                        break;
                }
            }
            yield return StartCoroutine(EndAttack()); //仮置き.アニメーション等の処理終了まで待機させる。
        }

        public IEnumerator BeginAttack()
        {
            originalPosition = transform.position;
            targetPosition = transform.position + transform.forward * 0.3f; // フロント方向に0.3移動

            elapsedTime = 0f;

            while (elapsedTime < moveDuration)
            {
                transform.position = Vector3.Lerp(originalPosition, targetPosition, elapsedTime / moveDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.position = targetPosition; // 念のため位置を合わせる
        }

        public IEnumerator EndAttack()
        {
            elapsedTime = 0f;

            while (elapsedTime < moveDuration)
            {
                transform.position = Vector3.Lerp(targetPosition, originalPosition, elapsedTime / moveDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.position = originalPosition; // 念のため位置を合わせる
        }
    }
}
