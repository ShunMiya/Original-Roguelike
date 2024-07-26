using UnityEngine;
using ItemSystemV2.Inventory;
using System;
using UnityEngine.EventSystems;
using Performances;

public class TutorialSkipButton : MonoBehaviour
{
    private SqliteDatabase sqlDB;
    [SerializeField] private GameObject DugeonSelectButton;
    private string query;
    private MenuSoundEffect menuSE;

    // Start is called before the first frame update
    void Start()
    {
        menuSE = FindObjectOfType<MenuSoundEffect>();

        if (sqlDB == null)
        {
            string databasePath = SQLDBInitializationV2.GetDatabasePath();
            sqlDB = new SqliteDatabase(databasePath);
        }
        query = "SELECT Cleared FROM DungeonChallengeStatus WHERE DungeonId = 1";
        DataTable TutorialClear = sqlDB.ExecuteQuery(query);

        int TutorialClearInt = Convert.ToInt32(TutorialClear[0]["Cleared"]);

        if (TutorialClearInt == 1) Destroy(gameObject);
    }

    public void OnSelected()
    {
        if (EventSystem.current.currentSelectedGameObject != gameObject)
        {
            EventSystem.current.SetSelectedGameObject(gameObject);
        }
        menuSE.MenuOperationSE(0);
    }

    public void TutorialSkip()
    {
        menuSE.MenuOperationSE(1);

        query = "UPDATE DungeonChallengeStatus SET Cleared = 1 WHERE DungeonId = 1;";
        sqlDB.ExecuteNonQuery(query);

        EventSystem.current.SetSelectedGameObject(DugeonSelectButton);
        Destroy(gameObject);
    }

}