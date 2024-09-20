using ItemSystemV2.Inventory;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItemSystemV2
{
    public class EquipmentItemObj : MonoBehaviour
    {
        [SerializeField] private GameObject Weapon;
        [SerializeField] private GameObject Shield;

        private bool WeaponEquip = false;
        private bool ShieldEquip = false;
        private SqliteDatabase sqlDB;

        void Start()
        {
            if (sqlDB == null)
            {
                string databasePath = SQLDBInitializationV2.GetDatabasePath();
                sqlDB = new SqliteDatabase(databasePath);
            }

            EquipmentObj();
        }

        public void Update()
        {
            Weapon.SetActive(WeaponEquip);
            Shield.SetActive(ShieldEquip);
        }

        public void EquipmentObj()
        {
            if (sqlDB == null)
            {
                string databasePath = SQLDBInitializationV2.GetDatabasePath();
                sqlDB = new SqliteDatabase(databasePath);
            }

            WeaponEquip = false; ShieldEquip = false;

            string checkEquippedQuery = "SELECT Equipped FROM Inventory WHERE Equipped IN (1, 2)";
            DataTable equippedItems = sqlDB.ExecuteQuery(checkEquippedQuery);
            foreach (DataRow row in equippedItems.Rows)
            {
                int equippedItemId = Convert.ToInt32(row["Equipped"]);

                switch (equippedItemId)
                {
                    case 1:
                        WeaponEquip = true; break;
                    case 2:
                        ShieldEquip = true; break;
                }
            }
        }
    }
}