using System.IO;
using UnityEngine;
using System;

namespace ItemSystemSQL.Inventory
{
    public class SQLDBInitialization : MonoBehaviour
    {
        public static string databasePath;

        private void Awake()
        {
            string originalDatabasePath = Path.Combine(Application.streamingAssetsPath, "InventoryDataBase.db");
            string copiedDatabasePath = Path.Combine(Application.persistentDataPath, "InventoryDataBase.db");

            #region 起動時DBをリセットしたい場合。
            /*
            if (File.Exists(copiedDatabasePath))
            {
                File.Delete(copiedDatabasePath);
            }
            */
            #endregion

            if (!File.Exists(copiedDatabasePath))
            {
                try
                {
                    File.Copy(originalDatabasePath, copiedDatabasePath);
                }
                catch (FileNotFoundException e)
                {
                    Debug.LogError("ファイルが見つかりません: " + e.Message);
                }
                catch (DirectoryNotFoundException e)
                {
                    Debug.LogError("コピー先のディレクトリが見つかりません: " + e.Message);
                }
                catch (Exception e)
                {
                    Debug.LogError("Unknown Error: " + e.Message);
                }
            }

            databasePath = copiedDatabasePath;

            string dataPath = Application.persistentDataPath;
            Debug.Log("Data Path: " + dataPath);
        }

        public static string GetDatabasePath()
        {
            return databasePath;
        }
    }
}