using ItemSystemV2.Inventory;
using System;
using UnityEngine;

public class StairSaveSystem : MonoBehaviour
{
    private string databasePath;
    private SqliteDatabase sqlDB;
    string query;

    // Start is called before the first frame update
    void OnEnable()
    {
        databasePath = SQLDBInitializationV2.GetDatabasePath();
        sqlDB = new SqliteDatabase(databasePath);
        query = "SELECT * FROM PlayerStatus WHERE PlayerID = 1";
        DataTable Data = sqlDB.ExecuteQuery(query);
        int CurrentDungeonId = Convert.ToInt32(Data[0]["DungeonId"]);
        int CurrentFloorLevel = Convert.ToInt32(Data[0]["FloorLevel"]);

        query = "SELECT TopFloor FROM DungeonChallengeStatus WHERE DungeonId = '" + CurrentDungeonId + "'";
        Data = sqlDB.ExecuteQuery(query);
        int TopFloor = Convert.ToInt32(Data[0]["TopFloor"]);

        if(CurrentFloorLevel == TopFloor) gameObject.SetActive(false);
    }
}