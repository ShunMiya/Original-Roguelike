using UnityEngine;
using ItemSystemV2.Inventory;
using ItemSystemV2;
using System;

namespace PlayerStatusSystemV2
{
    public class PlayerStatusV2 : MonoBehaviour
    {
        private SQLInventoryAddV2 SQLInventory;
        private SqliteDatabase sqlDB;

        private void Start()
        {
            SQLInventory = GetComponent<SQLInventoryAddV2>();

            string databasePath = SQLDBInitializationV2.GetDatabasePath();
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

        public void WeaponStatusPlus()
        {
            if (sqlDB == null)
            {
                string databasePath = SQLDBInitializationV2.GetDatabasePath();
                sqlDB = new SqliteDatabase(databasePath);
            }

            int AttackType = 0;
            float addAttack = 0;
            float addDefense = 0;
            float RangeBonus = 0;
            string checkEquippedQuery = "SELECT * FROM Inventory WHERE Equipped IN (1, 2)";
            DataTable equippedItems = sqlDB.ExecuteQuery(checkEquippedQuery);
            foreach (DataRow row in equippedItems.Rows)
            {
                int equippedItemId = Convert.ToInt32(row["Id"]);
                EquipmentDataV2 equippedItem = ItemDataCacheV2.GetEquipment(equippedItemId);

                AttackType += equippedItem.AttackType;
                addAttack += equippedItem.AttackBonus;
                addDefense += equippedItem.DefenseBonus;
                RangeBonus += equippedItem.WeaponRange;
            }
            string updateStatusQuery = "UPDATE PlayerStatus SET AttackType = " + AttackType + " WHERE PlayerID = 1;";
            sqlDB.ExecuteNonQuery(updateStatusQuery);
            updateStatusQuery = "UPDATE PlayerStatus SET Attack = (SELECT Strength FROM PlayerStatus WHERE PlayerID = 1) + " + addAttack + " WHERE PlayerID = 1;";
            sqlDB.ExecuteNonQuery(updateStatusQuery);
            updateStatusQuery = "UPDATE PlayerStatus SET Defense = " + addDefense + " WHERE PlayerID = 1;";
            sqlDB.ExecuteNonQuery(updateStatusQuery);
            updateStatusQuery = "UPDATE PlayerStatus SET AttackRange = " + 1 + " + " + RangeBonus + " WHERE PlayerID = 1;";
            sqlDB.ExecuteNonQuery(updateStatusQuery);
        }
    }
}