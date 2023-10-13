using ItemSystemV2.Inventory;
using System;
using UISystemV2;
using UnityEngine;

namespace PlayerStatusSystemV2
{
    public class PlayerLevel : MonoBehaviour
    {
        private PlayerStatusV2 playerStatus;
        private SystemTextV2 systemText;
        private SqliteDatabase sqlDB;
        private PlayerLevelData playerlevelData;
        [SerializeField] private GameObject levelupText;

        private int playerLevel;

        public void Start ()
        {
            playerStatus = GetComponent<PlayerStatusV2>();
            systemText = FindObjectOfType<SystemTextV2>();

            string databasePath = SQLDBInitializationV2.GetDatabasePath();
            sqlDB = new SqliteDatabase(databasePath);

            string query = "SELECT PlayerLevel FROM PlayerStatus WHERE PlayerID = 1;";
            DataTable Data = sqlDB.ExecuteQuery(query);
            playerLevel = Convert.ToInt32(Data[0]["PlayerLevel"]);
            playerlevelData = PlayerLevelDataCache.GetPlayerLevelData(playerLevel);
        }

        public void PlayerGetExp(int Exp)
        {
            systemText.TextSet(Exp+"‚ÌŒoŒ±’l‚ðŠl“¾‚µ‚½");

            string updateStatusQuery = "UPDATE PlayerStatus SET PlayerExp = (SELECT PlayerExp FROM PlayerStatus WHERE PlayerID = 1) + " + Exp + " WHERE PlayerID = 1;";
            sqlDB.ExecuteNonQuery(updateStatusQuery);

            string query = "SELECT PlayerExp FROM PlayerStatus WHERE PlayerID = 1;";
            DataTable Data = sqlDB.ExecuteQuery(query);
            int PlayerExp = Convert.ToInt32(Data[0]["PlayerExp"]);

            if(PlayerExp >= playerlevelData.NextLevelExp)
            {
                PlayerLevelUp(PlayerExp);
            }
        }

        public void PlayerLevelUp(int PlayerExp)
        {
            levelupText.SetActive(true);

            while (PlayerExp >= playerlevelData.NextLevelExp)
            {
                PlayerExp -= playerlevelData.NextLevelExp;
                playerLevel++;
                playerlevelData = PlayerLevelDataCache.GetPlayerLevelData(playerLevel);
            }
            string updateStatusQuery = "UPDATE PlayerStatus SET PlayerLevel = " + playerLevel + " WHERE PlayerID = 1;";
            sqlDB.ExecuteNonQuery(updateStatusQuery);
            updateStatusQuery = "UPDATE PlayerStatus SET PlayerExp = " + PlayerExp + " WHERE PlayerID = 1;";
            sqlDB.ExecuteNonQuery(updateStatusQuery);

            int MaxHP = UnityEngine.Random.Range(playerlevelData.BasicHP - playerlevelData.HPVariance, playerlevelData.BasicHP + playerlevelData.HPVariance + 1);
            updateStatusQuery = "UPDATE PlayerStatus SET MaxHP = " + MaxHP + " WHERE PlayerID = 1;";
            sqlDB.ExecuteNonQuery(updateStatusQuery);
            updateStatusQuery = "UPDATE PlayerStatus SET Strength = " + playerlevelData.Strength + " WHERE PlayerID = 1;";
            sqlDB.ExecuteNonQuery(updateStatusQuery);

            playerStatus.WeaponStatusPlus();
        }
    }
}