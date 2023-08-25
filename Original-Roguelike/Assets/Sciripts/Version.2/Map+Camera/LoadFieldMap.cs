using UnityEngine;
using System.Xml.Linq;
using MoveSystem;
using EnemySystem;

namespace Field
{
    public class LoadFieldMap : MonoBehaviour
    {
        public string mapName;
        public Areamap field;

        public GameObject enemies;

        public MoveAction player;

        void Start()
        {
            field.Reset();
            Array2D mapdata = readMapFile(mapName);
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
            try
            {
                XDocument xml = XDocument.Load(path);
                XElement map = xml.Element("map");
                Array2D data = null;
                int w = 0, h = 0;
                foreach (var layer in map.Elements("layer"))
                {
                    switch (layer.Attribute("id").Value)
                    {
                        case "1":
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
                foreach (var objgp in map.Elements("objectgroup"))
                {
                    switch (objgp.Attribute("id").Value)
                    {
                        case "3":
                            foreach (var obj in objgp.Elements("object"))
                            {
                                int x = int.Parse(obj.Attribute("x").Value);
                                int z = int.Parse(obj.Attribute("y").Value);
                                int pw = int.Parse(obj.Attribute("width").Value);
                                int ph = int.Parse(obj.Attribute("height").Value);
                                string name = obj.Attribute("name").Value;
                                if(name == "Player")
                                {
                                    player.SetPosition(x / pw, ToMirrorZ(z / ph, h));
                                    continue;
                                }
                                if(name.Contains("Enemy"))
                                {
                                    GameObject enemyObj = (GameObject)Resources.Load("PrefabsV2/" + name);
                                    GameObject enemy = Instantiate(enemyObj, enemies.transform);
                                    enemy.GetComponent<MoveAction>().SetPosition(x / pw, ToMirrorZ(z / ph, h));
                                    enemy.GetComponent<EnemyAction>().target = player;
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