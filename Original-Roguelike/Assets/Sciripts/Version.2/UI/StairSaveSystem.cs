using UnityEngine;
using ItemSystemV2.Inventory;
using System.IO;
using Performances;
using UnityEngine.EventSystems;
using StairsMenu;

namespace SaveLoad
{
    public class StairSaveSystem : MonoBehaviour
    {
        private string databasePath;
        private string saveDatabasePath;
        [SerializeField] private GameObject Text;
        private MenuSoundEffect menuSE;
        [SerializeField] private GameObject returnButton;
        [SerializeField] private StairsMenuSystem stairsMenuSystem;
        private SqliteDatabase sqlDB;

        // Start is called before the first frame update
        void Awake()
        {
            databasePath = SQLDBInitializationV2.GetDatabasePath();
            saveDatabasePath = Path.Combine(Application.persistentDataPath, "SaveDataBase01.db");
            menuSE = FindObjectOfType<MenuSoundEffect>();
        }

        public void OnSelected()
        {
            menuSE.MenuOperationSE(0);
        }

        public void Save()
        {
            menuSE.MenuOperationSE(1);

            sqlDB = new SqliteDatabase(databasePath);
            string updateStatusQuery = "UPDATE PlayerStatus SET FloorLevel = (SELECT FloorLevel FROM PlayerStatus WHERE PlayerID = 1) + 1 WHERE PlayerID = 1;";
            sqlDB.ExecuteNonQuery(updateStatusQuery);

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

        public void SelectReturnButton()
        {
            menuSE.MenuOperationSE(2);

            EventSystem.current.SetSelectedGameObject(returnButton);
            stairsMenuSystem.BackHomeMenu();
        }
    }
}