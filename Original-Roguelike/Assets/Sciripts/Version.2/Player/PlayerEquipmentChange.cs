using ItemSystemV2.Inventory;
using PlayerStatusSystemV2;
using System;
using UISystemV2;
using UnityEngine;

namespace ItemSystemV2
{
    public class PlayerEquipmentChange : MonoBehaviour
    {
        private SystemTextV2 systemText;
        private SqliteDatabase sqlDB;
        private PlayerStatusV2 playerStatusV2;

        public void Start()
        {
            string databasePath = SQLDBInitializationV2.GetDatabasePath();
            sqlDB = new SqliteDatabase(databasePath);
            systemText = FindObjectOfType<SystemTextV2>();
            playerStatusV2 = GetComponent<PlayerStatusV2>();
        }

        public void EquipItem(DataRow row)
        {
            if (systemText == null) systemText = FindObjectOfType<SystemTextV2>();
            int IID = Convert.ToInt32(row["IID"]);
            int itemId = Convert.ToInt32(row["Id"]);
            EquipmentDataV2 equipmentItem = ItemDataCacheV2.GetEquipment(itemId);

            switch (equipmentItem.EquipType)
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
                        EquipmentDataV2 equippedItem = ItemDataCacheV2.GetEquipment(equippedItemId);

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

            playerStatusV2.WeaponStatusPlus();
        }
        public void UnequipItem(DataRow row)
        {
            if (systemText == null) systemText = FindObjectOfType<SystemTextV2>();

            int IID = Convert.ToInt32(row["IID"]);
            string unequipQuery = "UPDATE Inventory SET Equipped = 0 WHERE IID = " + IID;
            sqlDB.ExecuteNonQuery(unequipQuery);

            playerStatusV2.WeaponStatusPlus();
        }
    }
}