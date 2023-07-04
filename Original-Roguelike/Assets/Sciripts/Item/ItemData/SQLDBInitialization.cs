using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace ItemSystem
{
    public class SQLDBInitialization : MonoBehaviour
    {
        private void Awake()
        {
            string originalDatabasePath = Path.Combine(Application.streamingAssetsPath, "ItemDataBase.db");
            string copiedDatabasePath = Path.Combine(Application.persistentDataPath, "ItemDataBase.db");

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