using ItemSystemSQL.Inventory;
using System;
using UnityEngine;

public class FloorSet : MonoBehaviour
{
    private SqliteDatabase sqlDB;

    // Start is called before the first frame update
    void Start()
    {
        string databasePath = SQLDBInitialization.GetDatabasePath();
        sqlDB = new SqliteDatabase(databasePath);

        SetFloor();
    }

    public void SetFloor()
    {
        if(sqlDB == null)
        {
            string databasePath = SQLDBInitialization.GetDatabasePath();
            sqlDB = new SqliteDatabase(databasePath);
        }

        string query = "SELECT FloorLevel FROM PlayerStatus WHERE PlayerID = 1;";
        DataTable Data = sqlDB.ExecuteQuery(query);
        int Floor = Convert.ToInt32(Data[0]["FloorLevel"]);

        string prefabPath = "Prefabs/Floor"+Floor;

        GameObject prefab = Resources.Load<GameObject>(prefabPath);
        Instantiate(prefab, prefab.transform.position, Quaternion.identity);
    }
}