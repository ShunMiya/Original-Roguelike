using System.IO;
using UnityEngine;

namespace ItemSystemSQL.Inventory
{
    public class SQLDBInitialization : MonoBehaviour
    {
        private void Awake()
        {
            string originalDatabasePath = Path.Combine(Application.streamingAssetsPath,"InventoryDataBase.db");
            string copiedDatabasePath = Path.Combine(Application.persistentDataPath,"InventoryDataBase.db");

            if (File.Exists(copiedDatabasePath))
            {
                File.Delete(copiedDatabasePath);
            }

            File.Copy(originalDatabasePath, copiedDatabasePath);

            Debug.Log("DBÇèâä˙âª");
            string dataPath = Application.persistentDataPath;
            Debug.Log("Data Path: " + dataPath);
        }
    }
}