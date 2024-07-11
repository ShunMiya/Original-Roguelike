using Field;
using MoveSystem;
using System.Collections;
using UnityEngine;
using ADVSystem;
using ItemSystemV2.Inventory;
using System;
using UISystemV2;

namespace PlayerV2
{
    public class ADVTrigeerCheck : MonoBehaviour
    {
        private Areamap field;
        private MoveAction move;
        private GameObject AreaObj;
        private string Type;
        private int OldPlayerLevel = 0;
        private int dungeonId;
        private SqliteDatabase sqlDB;
        [SerializeField] private ADVText aDVText;



        private void Start()
        {
            field = GetComponentInParent<Areamap>();
            move = GetComponent<MoveAction>();

            string databasePath = SQLDBInitializationV2.GetDatabasePath();
            sqlDB = new SqliteDatabase(databasePath);

            string query = "SELECT * FROM PlayerStatus WHERE PlayerID = 1;";
            DataTable PlayerDB = sqlDB.ExecuteQuery(query);
            dungeonId = Convert.ToInt32(PlayerDB[0]["DungeonId"]);
        }

        public bool TriggerCheck()
        {
            AreaObj = null;
            Type = null;
            
            AreaObj = field.IsCollideReturnADVTriggerObj(move.grid.x, move.grid.z);

            if(dungeonId == 1)
            {
                string query = "SELECT * FROM PlayerStatus WHERE PlayerID = 1;";
                DataTable PlayerDB = sqlDB.ExecuteQuery(query);
                int PlayerLevel = Convert.ToInt32(PlayerDB[0]["PlayerLevel"]);
                if (PlayerLevel == 2 && OldPlayerLevel == 1) Type = "レベルアップ説明";
                OldPlayerLevel = PlayerLevel;
            }

            return (AreaObj != null || Type != null);
        }

        public IEnumerator EventStart()
        {
            if (AreaObj != null)
            {
                ADVEvent advEvent = AreaObj.GetComponent<ADVEvent>();
                yield return StartCoroutine(advEvent.Event());
            }

            if(Type != null)
            {
                aDVText.TypeSet(Type);
                yield return StartCoroutine(aDVText.TextBoxSet());
            }
        }
    }
}