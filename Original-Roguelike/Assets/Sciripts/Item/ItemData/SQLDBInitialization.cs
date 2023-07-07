using System.IO;
using UnityEngine;

namespace ItemSystemSQL.Inventory
{
    public class SQLDBInitialization : MonoBehaviour
    {
        public static string databasePath;

        private void Awake()
        {
            string originalDatabasePath = Path.Combine(Application.streamingAssetsPath, "InventoryDataBase.db");
            string copiedDatabasePath = Path.Combine(Application.persistentDataPath, "InventoryDataBase.db");

            if (File.Exists(copiedDatabasePath))
            {
                File.Delete(copiedDatabasePath);
            }

            File.Copy(originalDatabasePath, copiedDatabasePath);

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