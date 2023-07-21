using ItemSystemSQL.Inventory;
using System;
using TMPro;
using UnityEngine;

public class StatusBar : MonoBehaviour
{
    private SqliteDatabase sqlDB;
    [SerializeField]private TextMeshProUGUI HPText;
    [SerializeField]private TextMeshProUGUI HungryText;
    [SerializeField] private TextMeshProUGUI AttackText;
    [SerializeField]private TextMeshProUGUI DefenseText;
    [SerializeField] private TextMeshProUGUI InventoryText;

    // Start is called before the first frame update
    void Start()
    {
        string databasePath = SQLDBInitialization.GetDatabasePath();
        sqlDB = new SqliteDatabase(databasePath);
    }

    // Update is called once per frame
    void Update()
    {
        if (sqlDB == null)
        {
            string databasePath = SQLDBInitialization.GetDatabasePath();
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
        HPText.text = "HP:" + CurrentHP;
    }

    public void UpdateHungryText(DataTable Data)
    {
        int CurrentHungry = Convert.ToInt32(Data[0]["CurrentHungry"]);
        HungryText.text = "Hungry:" + CurrentHungry;
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
