using UnityEngine;
using System.Xml.Linq;
using MoveSystem;
using EnemySystem;
using System.Collections;
using ItemSystemV2.Inventory;
using System;

namespace Field
{
    public class LoadFieldMap : MonoBehaviour
    {
        public Areamap field;

        public GameObject enemies;
        public GameObject items;
        public GameObject gimmicks;
        public MoveAction player;
        private SqliteDatabase sqlDB;

        void Start()
        {
            Load();
        }

        public void Load()
        {
            field.Reset();

            if (sqlDB == null)
            {
                string databasePath = SQLDBInitializationV2.GetDatabasePath();
                sqlDB = new SqliteDatabase(databasePath);
            }

            string query = "SELECT DungeonName FROM PlayerStatus WHERE PlayerID = 1;";
            DataTable Data = sqlDB.ExecuteQuery(query);
            string DungeonName = (string)Data[0]["DungeonName"];

            Array2D mapdata = readMapFile("Assets/Maps/"+DungeonName+ ".tmx");
            if (mapdata != null)
            {
                field.Create(mapdata);
            }
        }

        /**
        * TMXファイルからマップデータを取得する
        */
        private Array2D readMapFile(string path)
        {
            if (sqlDB == null)
            {
                string databasePath = SQLDBInitializationV2.GetDatabasePath();
                sqlDB = new SqliteDatabase(databasePath);
            }

            string query = "SELECT FloorLevel FROM PlayerStatus WHERE PlayerID = 1;";
            DataTable Data = sqlDB.ExecuteQuery(query);
            int FloorLevel = Convert.ToInt32(Data[0]["FloorLevel"]);

            try
            {
                XDocument xml = XDocument.Load(path);
                XElement map = xml.Element("map");
                XElement group = null;
                foreach (var gp in map.Elements("group"))
                {
                    if (gp.Attribute("name").Value.Equals(FloorLevel + "F"))
                    {
                        group = gp;
                        break;
                    }
                }
                if (group == null)  return new Array2D(1, 1);

                Array2D data = null;
                int w = 0, h = 0;
                foreach (var layer in group.Elements("layer"))
                {
                    switch (layer.Attribute("name").Value)
                    {
                        case "TileLayer":
                            string[] sdata = (layer.Element("data").Value).Split(',');
                            w = int.Parse(layer.Attribute("width").Value);
                            h = int.Parse(layer.Attribute("height").Value);
                            data = new Array2D(w, h);
                            for (int z = 0; z < h; z++)
                            {
                                for (int x = 0; x < w; x++)
                                {
                                    data.Set(x, z, int.Parse(sdata[x + ToMirrorZ(z, h) * w]) - 1);
                                    //data.Set(x, z, int.Parse(sdata[ToMirrorX(x, w) + z * w]) - 1);
                                }
                            }
                            break;
                    }
                }
                foreach (var objgp in group.Elements("objectgroup"))
                {
                    switch (objgp.Attribute("name").Value)
                    {
                        case "ObjectLayer":
                            foreach (var obj in objgp.Elements("object"))
                            {
                                int x = int.Parse(obj.Attribute("x").Value);
                                int z = int.Parse(obj.Attribute("y").Value);
                                int pw = int.Parse(obj.Attribute("width").Value);
                                int ph = int.Parse(obj.Attribute("height").Value);
                                string name = obj.Attribute("name").Value;
                                string type = "";
                                foreach (var prop in obj.Element("properties").Elements("property"))
                                {
                                    switch (prop.Attribute("name").Value)
                                    {
                                        case "Type":
                                            type = prop.Attribute("value").Value;
                                            break;
                                    }
                                }
                                if (type == "Player")
                                {
                                    player.SetPosition(x / pw, ToMirrorZ(z / ph, h));
                                    continue;
                                }
                                if(type.Contains("Enemy"))
                                {
                                    GameObject enemyObj = (GameObject)Resources.Load("PrefabsV2/" + name);
                                    GameObject enemy = Instantiate(enemyObj, enemies.transform);
                                    enemy.GetComponent<MoveAction>().SetPosition(x / pw, ToMirrorZ(z / ph, h));
                                    enemy.GetComponent<EnemyAction>().target = player;
                                }
                                if (type.Contains("Item"))
                                {
                                    GameObject itemObj = (GameObject)Resources.Load("PrefabsV2/" + name);
                                    GameObject item = Instantiate(itemObj, items.transform);
                                    item.GetComponent<MoveAction>().SetPosition(x / pw, ToMirrorZ(z / ph, h));
                                }
                                if(type.Contains("Gimmick"))
                                {
                                    GameObject GimmickObj = (GameObject)Resources.Load("PrefabsV2/" + name);
                                    GameObject Gimmick = Instantiate(GimmickObj, gimmicks.transform);
                                    Gimmick.GetComponent<ObjectPosition>().SetPosition(x / pw, ToMirrorZ(z / ph, h));
                                }
                            }
                            break;
                    }
                }

                return data;
            }
            catch (System.Exception i_exception)
            {
                Debug.LogErrorFormat("{0}", i_exception);
            }
            return null;
        }

        /**
         * * Z軸に対して反対の値を返す
         */
        private int ToMirrorX(int xgrid, int mapWidth)
        {
            return mapWidth - xgrid - 1;
        }

        /**
         * * X軸に対して反対の値を返す。
         */
        private int ToMirrorZ(int zgrid, int mapHeight)
        {
            return mapHeight - zgrid - 1;
        }
    }
}