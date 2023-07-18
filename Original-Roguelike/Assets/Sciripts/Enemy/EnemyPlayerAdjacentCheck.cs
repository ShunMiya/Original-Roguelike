using ItemSystemSQL.Inventory;
using System;
using UnityEngine;
using PlayerStatusList;
using UISystem;

namespace Enemy
{
    public class EnemyPlayerAdjacentCheck : MonoBehaviour
    {
        public Collider Playercollider;
        public bool isAttackHit;
        private SqliteDatabase sqlDB;
        public SystemText systemText;


        public void Start()
        {
            string databasePath = SQLDBInitialization.GetDatabasePath();
            sqlDB = new SqliteDatabase(databasePath);
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                Playercollider = collider;
                isAttackHit = true;
            }
        }

        private void OnTriggerExit(Collider collider)
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                Playercollider = null;
                isAttackHit = false;
            }
        }

        public bool IsAttackHit()
        {
            return isAttackHit;
        }


        public void Attacked()
        {
            PlayerStatusSQL enemyStatus = Playercollider.GetComponent<PlayerStatusSQL>();
            systemText.TextSet("Player Damage!");

            /*string query = "SELECT Attack FROM PlayerStatus WHERE PlayerID = 1;";
            DataTable Data = sqlDB.ExecuteQuery(query);
            int attack = Convert.ToInt32(Data[0]["Attack"]);

            enemyStatus.TakeDamage(attack);*/
        }
    }
}
