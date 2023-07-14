using UnityEngine;
using PlayerMovement;
using Combat.AttackMotion;
using ItemSystemSQL.Inventory;
using ItemSystemSQL;
using System;

namespace PlayerStatusList
{
    public class PlayerStatusSQL : MonoBehaviour
    {
        private PlayerMove playerMove;
        private AttackMotion attackMotion;

        public int inventorySize;

        private SqliteDatabase sqlDB;

        [SerializeField] private SQLInventoryAdd SQLInventory;

        public bool PlayerActive = false; //移動、攻撃、アイテムの使用(装備の着脱含)

        private void Start()
        {
            playerMove = GetComponent<PlayerMove>();
            attackMotion = GetComponent<AttackMotion>();

            inventorySizeUpDate();
            WeaponStatusPlus();
        }

        public void inventorySizeUpDate()
        {
            SQLInventory.InitializeFromPlayerStatus(inventorySize);
        }

        public bool IsPlayerActive()
        {
            PlayerActive = playerMove.IsMoving() || attackMotion.IsAttacking();
            return playerMove.IsMoving() || attackMotion.IsAttacking();
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