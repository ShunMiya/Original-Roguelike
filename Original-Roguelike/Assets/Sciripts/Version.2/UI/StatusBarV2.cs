using ItemSystemV2.Inventory;
using PlayerStatusSystemV2;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UISystemV2
{
    public class StatusBarV2 : MonoBehaviour
    {
        private SqliteDatabase sqlDB;
        [SerializeField] private TextMeshProUGUI LevelText;
        [SerializeField] private Image LevelGauge;
        private float Levelper = 0;
        [SerializeField] private TextMeshProUGUI HPText;
        [SerializeField] private Image HPGauge;
        private float HPper = 1;
        [SerializeField] private TextMeshProUGUI HungryText;
        [SerializeField] private Image HungryGauge;
        private float Hungryper = 1;
        [SerializeField] private GameObject LWeaponLevelBar;
        [SerializeField] private TextMeshProUGUI LWeaponLevelText;
        [SerializeField] private Image LWeaponLevelGauge;
        //private float LWeaponLevelper = 0;
        [SerializeField] private GameObject RWeaponLevelBar;
        [SerializeField] private TextMeshProUGUI RWeaponLevelText;
        [SerializeField] private Image RWeaponLevelGauge;
        //private float RWeaponLevelper = 0;

        private PlayerLevelData playerlevelData;

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
            int playerLevel = Convert.ToInt32(Data[0]["PlayerLevel"]);
            playerlevelData = PlayerLevelDataCache.GetPlayerLevelData(playerLevel);

            UpdateLevelText(Data);
            UpdateHPText(Data);
            UpdateHungryText(Data);
            //UpdateLweaponBar(Data);
            //UpdateRweaponBar(Data);
        }


        public void UpdateLevelText(DataTable Data)
        {
            float Level = Convert.ToInt32(Data[0]["PlayerLevel"]);
            LevelText.text = "Level:" + Level;
            float PlayerExp = Convert.ToInt32(Data[0]["PlayerExp"]);

            float per = PlayerExp / playerlevelData.NextLevelExp;
            if (Levelper != per)
            {
                if (per > 1) per--;
                FillGauge(LevelGauge, per);
                Levelper = per;
            }
        }

        public void UpdateHPText(DataTable Data)
        {
            float CurrentHP = Convert.ToInt32(Data[0]["CurrentHP"]);
            float MaxHP = Convert.ToInt32(Data[0]["MaxHP"]);
            HPText.text = "HP:" + CurrentHP + "/" + MaxHP;

            float per = CurrentHP / MaxHP;
            if (HPper != per)
            {
                FillGauge(HPGauge, per);
                HPper = per;
            }
        }

        public void UpdateHungryText(DataTable Data)
        {
            float CurrentHungry = Convert.ToInt32(Data[0]["CurrentHungry"]);
            float MaxHungry = Convert.ToInt32(Data[0]["MaxHungry"]);
            HungryText.text = "–ž• “x:" + CurrentHungry + "/" + MaxHungry;

            float per = CurrentHungry / MaxHungry;
            if (Hungryper != per)
            {
                FillGauge(HungryGauge, per);
                Hungryper = per;
            }
        }

        public void UpdateLweaponBar(DataTable Data)
        {
        }

        public void UpdateRweaponBar(DataTable Data)
        {
        }

        public void FillGauge(Image img, float per)
        {
            img.fillAmount = per;
        }
    }
}