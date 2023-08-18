using ItemSystemV2.Inventory;
using System;
using TMPro;
using UnityEngine;

namespace UISystemV2
{
    public class StatusBarV2 : MonoBehaviour
    {
        private SqliteDatabase sqlDB;
        [SerializeField] private TextMeshProUGUI HPText;
        [SerializeField] private TextMeshProUGUI HungryText;
        [SerializeField] private TextMeshProUGUI AttackText;
        [SerializeField] private TextMeshProUGUI DefenseText;
        [SerializeField] private TextMeshProUGUI InventoryText;

        void Start()
        {
            string databasePath = SQLDBInitializationV2.GetDatabasePath();
            sqlDB = new SqliteDatabase(databasePath);
        }

        void Update()
        {
            if (sqlDB == null)
            {
                string databasePath = SQLDBInitializationV2.GetDatabasePath();
                sqlDB = new SqliteDatabase(databasePath);
            }

            string query = "SELECT * FROM PlayerStatus WHERE PlayerID = 1;";
            DataTable Data = sqlDB.ExecuteQuery(query);

            UpdateHPText(Data);
            UpdateHungryText(Data);
            UpdateAttackText(Data);
            UpdateDefenseText(Data);
            UpdateInventoryText(Data);
        }

        public void UpdateHPText(DataTable Data)
        {
            int CurrentHP = Convert.ToInt32(Data[0]["CurrentHP"]);
            int MaxHP = Convert.ToInt32(Data[0]["MaxHP"]);
            HPText.text = "HP:" + CurrentHP + "/" + MaxHP;
        }

        public void UpdateHungryText(DataTable Data)
        {
            int CurrentHungry = Convert.ToInt32(Data[0]["CurrentHungry"]);
            int MaxHungry = Convert.ToInt32(Data[0]["MaxHungry"]);
            HungryText.text = "Hungry:" + CurrentHungry + "/" + MaxHungry;
        }

        public void UpdateAttackText(DataTable Data)
        {
            int Attack = Convert.ToInt32(Data[0]["Attack"]);
            AttackText.text = "Attack:" + Attack;
        }

        public void UpdateDefenseText(DataTable Data)
        {
            int Defense = Convert.ToInt32(Data[0]["Defense"]);
            DefenseText.text = "Defense:" + Defense;
        }

        public void UpdateInventoryText(DataTable Data)
        {
            int Size = Convert.ToInt32(Data[0]["InventorySize"]);

            string query = "SELECT COUNT(*) AS TotalCount FROM Inventory";
            DataTable result = sqlDB.ExecuteQuery(query);
            object Count = result.Rows[0]["TotalCount"];
            InventoryText.text = "Inventory:" + Count + "/" + Size;
        }
    }
}