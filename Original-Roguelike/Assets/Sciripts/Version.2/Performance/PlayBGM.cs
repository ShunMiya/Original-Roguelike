using ItemSystemV2.Inventory;
using UnityEngine;

public class PlayBGM : MonoBehaviour
{
    private SqliteDatabase sqlDB;
    [SerializeField] private AudioSource BGM;

    [SerializeField] private AudioClip TutorialDungeonBGM;
    [SerializeField] private AudioClip ForestBGM;
    [SerializeField] private AudioClip MountainBGM;

    private void Start()
    {
        if (sqlDB == null)
        {
            string databasePath = SQLDBInitializationV2.GetDatabasePath();
            sqlDB = new SqliteDatabase(databasePath);
        }

        string query = "SELECT DungeonId FROM PlayerStatus WHERE PlayerID = 1;";
        DataTable Data = sqlDB.ExecuteQuery(query);
        int DungeonId = (int)Data[0]["DungeonId"];

        switch (DungeonId)
        {
            case 1:
                BGM.clip = TutorialDungeonBGM;
                BGM.volume = 0.3f;
                break;
            case 2:
                BGM.clip = ForestBGM;
                BGM.volume = 0.15f;
                break;
            case 3:
                BGM.clip = MountainBGM;
                break;
        }
        BGM.Play();
    }
}
