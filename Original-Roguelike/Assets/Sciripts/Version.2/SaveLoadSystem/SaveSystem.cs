using UnityEngine;
using ItemSystemV2.Inventory;
using System.IO;

namespace SaveLoad
{
    public class SaveSystem : MonoBehaviour
    {
        private string databasePath;
        private string saveDatabasePath;
        private SqliteDatabase sqlDB;
        [SerializeField] private GameObject Text;

        // Start is called before the first frame update
        void Start()
        {
            databasePath = SQLDBInitializationV2.GetDatabasePath();
            saveDatabasePath = Path.Combine(Application.persistentDataPath, "SaveDataBase01.db");
        }

        public void Save()
        {
            try
            {
                File.Copy(databasePath, saveDatabasePath, true);
                Debug.Log("データベースが正常に複製されました。");
            }
            catch (IOException ex)
            {
                Debug.LogError("データベースの複製に失敗しました: " + ex.Message);
            }
        }

        public void StairsSave()
        {
            sqlDB = new SqliteDatabase(databasePath);
            string updateStatusQuery = "UPDATE PlayerStatus SET FloorLevel = (SELECT FloorLevel FROM PlayerStatus WHERE PlayerID = 1) + 1 WHERE PlayerID = 1;";
            sqlDB.ExecuteNonQuery(updateStatusQuery);

            Save();
        }

        public void TextDisplay()
        {
            Text.SetActive(true);

            Invoke("DisableText", 1f);
        }

        private void DisableText()
        {
            Text.SetActive(false);
        }
    }
}