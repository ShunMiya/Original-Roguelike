using UnityEngine;
using PlayerMovement;
using Combat.AttackMotion;
using ItemSystemSQL.Inventory;
using ItemSystemSQL;
using System;
using Enemy;
using GameEndSystem;

namespace PlayerStatusList
{
    public class PlayerStatusSQL : MonoBehaviour
    {
        private PlayerMove playerMove;
        private AttackMotion attackMotion;
        private EnemyTurnStart enemyturn;
        private SQLInventoryAdd SQLInventory;
        private PlayerHP playerHP;
        private SqliteDatabase sqlDB;
        private GameClear gameClear;
        private GameOver gameOver;


        public bool PlayerActive = false; //移動、攻撃、アイテムの使用(装備の着脱含)

        private void Start()
        {
            playerMove = GetComponent<PlayerMove>();
            attackMotion = GetComponent<AttackMotion>();
            SQLInventory = GetComponent<SQLInventoryAdd>();
            playerHP = GetComponent<PlayerHP>();
            enemyturn = FindObjectOfType<EnemyTurnStart>();
            gameClear = FindObjectOfType<GameClear>();
            gameOver = FindObjectOfType<GameOver>();

            string databasePath = SQLDBInitialization.GetDatabasePath();
            sqlDB = new SqliteDatabase(databasePath);

            inventorySizeLoad();
            WeaponStatusPlus();
        }

        public void inventorySizeLoad()
        {
            string query = "SELECT InventorySize FROM PlayerStatus WHERE PlayerID = 1;";
            DataTable Data = sqlDB.ExecuteQuery(query);
            int Size = Convert.ToInt32(Data[0]["InventorySize"]);

            SQLInventory.inventorySizeSet(Size);
        }

        public bool IsPlayerActive()
        {
            bool previousActive = PlayerActive;
            PlayerActive = playerMove.IsMoving() || attackMotion.IsAttacking()
                || gameClear.IsGameClear() || gameOver.IsGameOver();

            if (previousActive && !PlayerActive)
            {
                HungryUpdate();
                enemyturn.EnemyTurn();
            }
            return PlayerActive;
        }

        public void HungryUpdate()
        {
            if (sqlDB == null)
            {
                string databasePath = SQLDBInitialization.GetDatabasePath();
                sqlDB = new SqliteDatabase(databasePath);
            }
            string query = "SELECT CurrentHungry FROM PlayerStatus WHERE PlayerID = 1;";
            DataTable Data = sqlDB.ExecuteQuery(query);
            int CurrentHungy = Convert.ToInt32(Data[0]["CurrentHungry"]);

            if(CurrentHungy > 0)
            {
                string updateStatusQuery = "UPDATE PlayerStatus SET CurrentHungry = (SELECT CurrentHungry FROM PlayerStatus WHERE PlayerID = 1) - " + 1 + " WHERE PlayerID = 1;";
                sqlDB.ExecuteNonQuery(updateStatusQuery);
            }
            else
            {
                playerHP.TakeDamage(1);
            }
        }

        public void WeaponStatusPlus()
        {
            if(sqlDB == null)
            {
                string databasePath = SQLDBInitialization.GetDatabasePath();
                sqlDB = new SqliteDatabase(databasePath);
            }

            float addAttack = 0;
            float addDefense = 0;
            float RangeBonus = 0;
            float DistanceBonus = 0;
            string checkEquippedQuery = "SELECT * FROM Inventory WHERE Equipped IN (1, 2)";
            DataTable equippedItems = sqlDB.ExecuteQuery(checkEquippedQuery);
            foreach (DataRow row in equippedItems.Rows)
            {
                int equippedItemId = Convert.ToInt32(row["Id"]);
                EquipmentData equippedItem = ItemDataCache.GetEquipment(equippedItemId);

                addAttack += equippedItem.AttackBonus;
                addDefense += equippedItem.DefenseBonus;
                RangeBonus += equippedItem.WeaponRange;
                DistanceBonus += equippedItem.WeaponDistance;
            }
            string updateStatusQuery = "UPDATE PlayerStatus SET Attack = (SELECT Strength FROM PlayerStatus WHERE PlayerID = 1) + " + addAttack + " WHERE PlayerID = 1;";
            sqlDB.ExecuteNonQuery(updateStatusQuery);
            updateStatusQuery = "UPDATE PlayerStatus SET Defense = " + addDefense + " WHERE PlayerID = 1;";
            sqlDB.ExecuteNonQuery(updateStatusQuery);
            updateStatusQuery = "UPDATE PlayerStatus SET AttackRange = " + 1 + " + " + RangeBonus + " WHERE PlayerID = 1;";
            sqlDB.ExecuteNonQuery(updateStatusQuery);
            updateStatusQuery = "UPDATE PlayerStatus SET AttackDistance = " + 1 + " + " + DistanceBonus + " WHERE PlayerID = 1;";
            sqlDB.ExecuteNonQuery(updateStatusQuery);
        }
    }
}