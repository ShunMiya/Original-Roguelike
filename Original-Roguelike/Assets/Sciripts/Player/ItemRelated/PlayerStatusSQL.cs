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

        [SerializeField] private int STR;
        [SerializeField] private int VIT;
        public float Attack;
        public float Defense;
        [HideInInspector]public float Range;
        [HideInInspector]public float Distance;
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
            return playerMove.IsMoving() || attackMotion.IsAttacking();
        }

        public void WeaponStatusPlus()
        {
            float addAttack = 0;
            float addDefense = 0;
            float RangeBonus = 0;
            float DistanceBonus = 0;
            string databasePath = SQLDBInitialization.GetDatabasePath();
            sqlDB = new SqliteDatabase(databasePath);
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
            Attack = STR + addAttack;
            Defense = VIT + addDefense;
            Range = 1 + RangeBonus;
            Distance = 1 + DistanceBonus;
            Debug.Log("Attack" + Attack);
        }
    }
}