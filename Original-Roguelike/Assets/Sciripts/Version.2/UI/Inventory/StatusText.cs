using ItemSystemV2.Inventory;
using PlayerStatusSystemV2;
using System;
using TMPro;
using UnityEngine;

namespace UISystemV2
{
    public class StatusText : MonoBehaviour
    {
        private SqliteDatabase sqlDB;
        [SerializeField] private TextMeshProUGUI statusText;
        [SerializeField] private TextMeshProUGUI conditionText;
        private int playerLevel;
        private PlayerLevelData playerlevelData;
        [SerializeField] private PlayerCondition playerCondition;

        private void OnEnable()
        {
            SetstatusText();
            SetConditionText();
        }

        public void SetstatusText()
        {
            if (sqlDB == null)
            {
                string databasePath = SQLDBInitializationV2.GetDatabasePath();
                sqlDB = new SqliteDatabase(databasePath);
            }

            string query = "SELECT * FROM PlayerStatus WHERE PlayerID = 1;";
            DataTable Data = sqlDB.ExecuteQuery(query);

            playerLevel = Convert.ToInt32(Data[0]["PlayerLevel"]);
            playerlevelData = PlayerLevelDataCache.GetPlayerLevelData(playerLevel);

            string output = "Level:\t" + Convert.ToInt32(Data[0]["PlayerLevel"]) + "\n" +
                            "Exp:\t\t" + Convert.ToInt32(Data[0]["PlayerExp"]) + " / " + playerlevelData.NextLevelExp +"\n" +
                            "HP:\t\t" + Convert.ToInt32(Data[0]["CurrentHP"]) + " / " + Convert.ToInt32(Data[0]["MaxHP"]) + "\n" +
                            "�����x:\t" + Convert.ToInt32(Data[0]["CurrentHungry"]) + " / " + Convert.ToInt32(Data[0]["MaxHungry"]) + "\n" +
                            "�U����:\t" + Convert.ToInt32(Data[0]["Attack"]) + "\n" +
                            "�h���:\t" + Convert.ToInt32(Data[0]["Defense"]) + "\n";


            statusText.text = (output);
        }

        public void SetConditionText()
        {
            int PoisonTurn = playerCondition.GetConditionTurn(1);
            int ConfusionTurn = playerCondition.GetConditionTurn(2);
            int StunTurn = playerCondition.GetConditionTurn(3);
            int BlindTurn = playerCondition.GetConditionTurn(4);

            /*string poison = "Poison :\t" + PoisonTurn + "Turn";
            string confusion = "Confusion :\t" + ConfusionTurn + "Turn";
            string stun = "Stun :\t" + StunTurn + "Turn";
            string blind = "Blind :\t" + BlindTurn + "Turn";*/

            string output = "";

            if (PoisonTurn > 0)
            {
                output += "�ŏ�� :\t�@" + PoisonTurn + "Turn\n";
            }

            if (ConfusionTurn > 0)
            {
                output += "������� :�@" + ConfusionTurn + "Turn\n";
            }

            if (StunTurn > 0)
            {
                output += "�C���� :�@" + StunTurn + "Turn\n";
            }

            if (BlindTurn > 0)
            {
                output += "�Ӗڏ�� :�@" + BlindTurn + "Turn\n";
            }

            conditionText.text = (output);
        }
    }
}