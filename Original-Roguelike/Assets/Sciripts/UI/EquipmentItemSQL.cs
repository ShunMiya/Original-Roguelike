using UnityEngine;
using TMPro;
using ItemSystemSQL;
using System;
using ItemSystemSQL.Inventory;
using PlayerStatusList;

namespace UISystem
{
    public class EquipmentItemSQL : MonoBehaviour
    {
        private SqliteDatabase sqlDB;
        private SystemText systemText;

        [SerializeField] private TextMeshProUGUI BonusText;
        public PlayerStatusSQL playerStatusSQL;

        public void Awake()
        {
            string databasePath = SQLDBInitialization.GetDatabasePath();
            sqlDB = new SqliteDatabase(databasePath);
            systemText = FindObjectOfType<SystemText>();
        }

        public void EquipItem(DataRow row)
        {
            if (systemText == null) systemText = FindObjectOfType<SystemText>();
            int IID = Convert.ToInt32(row["IID"]);
            int itemId = Convert.ToInt32(row["Id"]);
            EquipmentData equipmentItem = ItemDataCache.GetEquipment(itemId);

            switch(equipmentItem.EquipType)
            {
                case 0:
                    string checkEquippedQuery = "SELECT * FROM Inventory WHERE Equipped = 1";
                    DataTable LeftEquipped = sqlDB.ExecuteQuery(checkEquippedQuery);
                    if (LeftEquipped.Rows.Count > 0)
                    {
                        int equippedIID = Convert.ToInt32(LeftEquipped.Rows[0]["IID"]);
                        string unequipQuery = "UPDATE Inventory SET Equipped = 0 WHERE IID = " + equippedIID;
                        sqlDB.ExecuteNonQuery(unequipQuery);
                    }

                    string equipQuery = "UPDATE Inventory SET Equipped = " + 1 + " WHERE IID = " + row["IID"];
                    sqlDB.ExecuteNonQuery(equipQuery);
                    systemText.TextSet(equipmentItem.ItemName + " Equip!");
                    break;
                case 1:
                    checkEquippedQuery = "SELECT * FROM Inventory WHERE Equipped IN (1, 2)";
                    DataTable Equipped = sqlDB.ExecuteQuery(checkEquippedQuery);
                    foreach (DataRow equippedRow in Equipped.Rows)
                    {
                        int equippedIID = Convert.ToInt32(equippedRow["IID"]);
                        string unequipQuery = "UPDATE Inventory SET Equipped = 0 WHERE IID = " + equippedIID;
                        sqlDB.ExecuteNonQuery(unequipQuery);
                    }

                    equipQuery = "UPDATE Inventory SET Equipped = " + 1 + " WHERE IID = " + row["IID"];
                    sqlDB.ExecuteNonQuery(equipQuery);
                    systemText.TextSet(equipmentItem.ItemName + " Equip!");
                    break;
                case 2:
                    checkEquippedQuery = "SELECT * FROM Inventory WHERE Equipped = 1";
                    LeftEquipped = sqlDB.ExecuteQuery(checkEquippedQuery);
                    if (LeftEquipped.Rows.Count > 0)
                    {
                        int equippedIID = Convert.ToInt32(LeftEquipped.Rows[0]["IID"]);
                        int equippedItemId = Convert.ToInt32(LeftEquipped.Rows[0]["Id"]);
                        EquipmentData equippedItem = ItemDataCache.GetEquipment(equippedItemId);

                        if (equippedItem.EquipType == 1)
                        {
                            string unequipQuery = "UPDATE Inventory SET Equipped = 0 WHERE IID = " + equippedIID;
                            sqlDB.ExecuteNonQuery(unequipQuery);
                        }
                    }
                    checkEquippedQuery = "SELECT * FROM Inventory WHERE Equipped = 2";
                    DataTable RightEquipped = sqlDB.ExecuteQuery(checkEquippedQuery);
                    if (RightEquipped.Rows.Count > 0)
                    {
                        int equippedIID = Convert.ToInt32(RightEquipped.Rows[0]["IID"]);
                        string unequipQuery = "UPDATE Inventory SET Equipped = 0 WHERE IID = " + equippedIID;
                        sqlDB.ExecuteNonQuery(unequipQuery);
                    }

                    equipQuery = "UPDATE Inventory SET Equipped = " + 2 + " WHERE IID = " + row["IID"];
                    sqlDB.ExecuteNonQuery(equipQuery);
                    systemText.TextSet(equipmentItem.ItemName + " Equip!");
                    break;
            }

            SetButtonData();
            SetBonusText();
            playerStatusSQL.WeaponStatusPlus();
        }

        public void UnequipItem(DataRow row)
        {
            if (systemText == null) systemText = FindObjectOfType<SystemText>();

            int IID = Convert.ToInt32(row["IID"]);
            string unequipQuery = "UPDATE Inventory SET Equipped = 0 WHERE IID = " + IID;
            sqlDB.ExecuteNonQuery(unequipQuery);

            SetBonusText();
            playerStatusSQL.WeaponStatusPlus();
        }

        public void SetButtonData()
        {
            EquipmentItemButtonSQL leftButton = transform.Find("LeftHandButton").GetComponent<EquipmentItemButtonSQL>();
            EquipmentItemButtonSQL rightButton = transform.Find("RightHandButton").GetComponent<EquipmentItemButtonSQL>();
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

            string output = "Attack Bonus\t" + addAttack + "\n" +
                            "Defense Bonus\t" + addDefense + "\n" +
                            "Range Bonus\t" + RangeBonus + "\n" +
                            "Distance Bonus\t" + DistanceBonus;

            BonusText.text = (output);
        }
    }
}