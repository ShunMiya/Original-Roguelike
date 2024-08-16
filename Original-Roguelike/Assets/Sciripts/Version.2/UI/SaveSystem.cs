using UnityEngine;
using ItemSystemV2.Inventory;
using System.IO;

namespace SaveLoad
{
    public class SaveSystem : MonoBehaviour
    {
        private string databasePath;
        [SerializeField] private GameObject Text;

        // Start is called before the first frame update
        void Start()
        {
            databasePath = SQLDBInitializationV2.GetDatabasePath();

        }

        public void Save()
        {
            string saveDatabasePath = Path.Combine(Application.persistentDataPath, "SaveDataBase01.db");

            try
            {
                // PlayerDataBase.dbをSaveDataBase01.dbとして複製
                File.Copy(databasePath, saveDatabasePath, true); // trueは、既存ファイルを上書きするオプション
                Debug.Log("データベースが正常に複製されました。");
            }
            catch (IOException ex)
            {
                Debug.LogError("データベースの複製に失敗しました: " + ex.Message);
            }

            Text.SetActive(true);

            Invoke("DisableText", 1f);
        }

        private void DisableText()
        {
            Text.SetActive(false);
        }
    }
}