using UnityEngine;
using Enemy;
using ItemSystemSQL.Inventory;
using System;

namespace PlayerFrontChecker
{
    public class PlayerFrontCheck : MonoBehaviour
    {
        public Collider Enemycollider;
        public bool isAttackHit;
        private SqliteDatabase sqlDB;

        public void Start()
        {
            string databasePath = SQLDBInitialization.GetDatabasePath();
            sqlDB = new SqliteDatabase(databasePath);
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.CompareTag("Enemy"))
            {
                Enemycollider = collider;
                isAttackHit = true;
            }
        }

        private void OnTriggerExit(Collider collider)
        {
            if (collider.gameObject.CompareTag("Enemy"))
            {
                Enemycollider = null;
                isAttackHit = false;
            }
        }

        public void EnemyDestroy()
        {
            Enemycollider = null;
            isAttackHit = false;
        }

        public void Attacked()
        {
            if (!isAttackHit) return;

            EnemyStatus enemyStatus = Enemycollider.GetComponent<EnemyStatus>();
            string query = "SELECT Attack FROM PlayerStatus WHERE PlayerID = 1;";
            DataTable Data = sqlDB.ExecuteQuery(query);
            int attack = Convert.ToInt32(Data[0]["Attack"]);
            
            enemyStatus.TakeDamage(attack);
        }
    }
}