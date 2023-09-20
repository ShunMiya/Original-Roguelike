using System.IO;
using UnityEngine;
using System;

namespace ItemSystemV2.Inventory
{
    public class SQLDBInitializationV2 : MonoBehaviour
    {
        public static string databasePath;

        private void Awake()
        {
            string originalDatabasePath = Path.Combine(Application.streamingAssetsPath, "PlayerDataBase.db");
            string copiedDatabasePath = Path.Combine(Application.persistentDataPath, "PlayerDataBase.db");

            #region 起動時DBをリセット。インベントリ、ステータス初期化

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

        public static void PlayerStatusInitialization()
        {
            SqliteDatabase copiedsqlDB = new SqliteDatabase(databasePath);

            string updateQuery = "UPDATE PlayerStatus " +
                                "SET CurrentHP = (SELECT CurrentHP FROM PlayerStatus WHERE PlayerID = 99), " +
                                "MaxHP = (SELECT MaxHP FROM PlayerStatus WHERE PlayerID = 99), " +
                                "CurrentHungry = (SELECT CurrentHungry FROM PlayerStatus WHERE PlayerID = 99), " +
                                "MaxHungry = (SELECT MaxHungry FROM PlayerStatus WHERE PlayerID = 99), " +
                                "Strength = (SELECT Strength FROM PlayerStatus WHERE PlayerID = 99), " +
                                "Vitality = (SELECT Vitality FROM PlayerStatus WHERE PlayerID = 99), " +
                                "Attack = (SELECT Attack FROM PlayerStatus WHERE PlayerID = 99), " +
                                "Defense = (SELECT Defense FROM PlayerStatus WHERE PlayerID = 99), " +
                                "AttackRange = (SELECT AttackRange FROM PlayerStatus WHERE PlayerID = 99), " +
                                "AttackDistance = (SELECT AttackDistance FROM PlayerStatus WHERE PlayerID = 99), " +
                                "ActionSpeed = (SELECT ActionSpeed FROM PlayerStatus WHERE PlayerID = 99), " +
                                "InventorySize = (SELECT InventorySize FROM PlayerStatus WHERE PlayerID = 99), " +
                                "FloorLevel = (SELECT FloorLevel FROM PlayerStatus WHERE PlayerID = 99) " +
                                "WHERE PlayerID = 1;";
            try
            {
                copiedsqlDB.ExecuteNonQuery(updateQuery);
            }
            catch (Exception e)
            {
                Debug.LogError("エクスポートエラー: " + e.Message);
            }
        }

        public static void PlayerInventoryInitialization()
        {
            SqliteDatabase copiedsqlDB = new SqliteDatabase(databasePath);

            string deleteQuery = "DELETE FROM Inventory WHERE Equipped IS NULL OR (Equipped <> 1 AND Equipped <> 2);";
            try
            {
                copiedsqlDB.ExecuteNonQuery(deleteQuery);
            }
            catch (Exception e)
            {
                Debug.LogError("削除エラー: " + e.Message);
            }
        }
    }
}