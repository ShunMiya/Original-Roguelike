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
            string originalDatabasePath = Path.Combine(Application.streamingAssetsPath, "PlayerDataBase.db");
            string copiedDatabasePath = Path.Combine(Application.persistentDataPath, "PlayerDataBase.db");

            #region �N����DB�����Z�b�g�B�C���x���g���A�X�e�[�^�X������
            
            if (File.Exists(copiedDatabasePath))
            {
                File.Delete(copiedDatabasePath);
            }
            
            #endregion

            if (!File.Exists(copiedDatabasePath))
            {
                try
                {
                    File.Copy(originalDatabasePath, copiedDatabasePath);
                }
                catch (FileNotFoundException e)
                {
                    Debug.LogError("�t�@�C����������܂���: " + e.Message);
                }
                catch (DirectoryNotFoundException e)
                {
                    Debug.LogError("�R�s�[��̃f�B���N�g����������܂���: " + e.Message);
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