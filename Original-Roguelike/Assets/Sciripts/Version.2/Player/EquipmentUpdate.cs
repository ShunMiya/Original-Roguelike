using ItemSystemV2.Inventory;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UISystemV2;
using UnityEngine;

public class EquipmentUpdate : MonoBehaviour
{
    private SqliteDatabase sqlDB;
    private SystemTextV2 systemText;

    void Start()
    {
        systemText = FindObjectOfType<SystemTextV2>();

        string databasePath = SQLDBInitializationV2.GetDatabasePath();
        sqlDB = new SqliteDatabase(databasePath);

    }

    public void WeaponUpdate(int ReinNum)
    {
        if (sqlDB == null)
        {
            string databasePath = SQLDBInitializationV2.GetDatabasePath();
            sqlDB = new SqliteDatabase(databasePath);
        }

        string checkEquippedQuery = "SELECT COUNT(*) as ItemCount FROM Inventory WHERE Equipped = 1";
        DataTable equippedItem = sqlDB.ExecuteQuery(checkEquippedQuery);
        int equipcheck = int.Parse(equippedItem.Rows[0]["ItemCount"].ToString());

        if (equipcheck > 0)
        {
            string updateQuery = "UPDATE Inventory SET ReinforceNum = COALESCE(ReinforceNum, 0) + " + ReinNum + " WHERE Equipped = 1";
            sqlDB.ExecuteNonQuery(updateQuery);

            systemText.TextSet("武器を強化しました。");
        }
        else systemText.TextSet("装備中の武器が見つかりませんでした。");
    }

        public void ShieldUpdate(int ReinNum)
    {
        if (sqlDB == null)
        {
            string databasePath = SQLDBInitializationV2.GetDatabasePath();
            sqlDB = new SqliteDatabase(databasePath);
        }

        string checkEquippedQuery = "SELECT COUNT(*) as ItemCount FROM Inventory WHERE Equipped = 2";
        DataTable equippedItem = sqlDB.ExecuteQuery(checkEquippedQuery);
        int equipcheck = int.Parse(equippedItem.Rows[0]["ItemCount"].ToString());

        if (equipcheck > 0)
        {
            string updateQuery = "UPDATE Inventory SET ReinforceNum = COALESCE(ReinforceNum, 0) + " + ReinNum + " WHERE Equipped = 2";
            sqlDB.ExecuteNonQuery(updateQuery);

            systemText.TextSet("防具を強化しました。");
        }
        else systemText.TextSet("装備中の防具が見つかりませんでした。");
    }
}