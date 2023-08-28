using UnityEngine;
using TMPro;
using System;
using ItemSystemV2;
using ItemSystemV2.Inventory;

namespace UISystemV2
{
    public class EquipmentItemV2 : MonoBehaviour
    {
        private SqliteDatabase sqlDB;
        [SerializeField] private TextMeshProUGUI BonusText;

        public void SetButtonData()
        {
            if(sqlDB == null)
            {
                string databasePath = SQLDBInitializationV2.GetDatabasePath();
                sqlDB = new SqliteDatabase(databasePath);

            }
            EquipmentItemButtonV2 leftButton = transform.Find("LeftHandButton").GetComponent<EquipmentItemButtonV2>();
            EquipmentItemButtonV2 rightButton = transform.Find("RightHandButton").GetComponent<EquipmentItemButtonV2>();
            DataRow Leftrow = null;
            DataRow Rightrow = null;
            string checkEquippedQuery = "SELECT * FROM Inventory WHERE Equipped IN (1, 2)";
            DataTable equippedItems = sqlDB.ExecuteQuery(checkEquippedQuery);
            foreach (DataRow row in equippedItems.Rows)
            {
                if (Convert.ToInt32(row["Equipped"]) == 1) Leftrow = row;
                if (Convert.ToInt32(row["Equipped"]) == 2) Rightrow = row;
            }
            leftButton.SetEquip(Leftrow);
            rightButton.SetEquip(Rightrow);
        }

        public void SetBonusText()
        {
            if (sqlDB == null)
            {
                string databasePath = SQLDBInitializationV2.GetDatabasePath();
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
                EquipmentDataV2 equippedItem = ItemDataCacheV2.GetEquipment(equippedItemId);

                addAttack += equippedItem.AttackBonus;
                addDefense += equippedItem.DefenseBonus;
                RangeBonus += equippedItem.WeaponRange;
                DistanceBonus += equippedItem.WeaponDistance;
            }

            string output = "Attack Bonus\t" + addAttack + "\n" +
                            "Defense Bonus\t" + addDefense + "\n" +
                            "Range Bonus\t" + RangeBonus + "\n" +
                            "Distance Bonus\t" + DistanceBonus;

            BonusText.text = (output);
        }
    }
}