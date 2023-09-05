using UnityEngine;
using System.Xml.Linq;
using MoveSystem;
using ItemSystemV2.Inventory;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

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
        private RandomDungeon dungeon;

        void Start()
        {
            dungeon = new RandomDungeon();
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

            string query = "SELECT DungeonId FROM PlayerStatus WHERE PlayerID = 1;";
            DataTable Data = sqlDB.ExecuteQuery(query);
            int DungeonId = Convert.ToInt32(Data[0]["DungeonId"]);
            query = "SELECT DungeonName FROM DungeonChallengeStatus WHERE DungeonId = (SELECT DungeonId FROM PlayerStatus WHERE PlayerID = 1);)";
            Data = sqlDB.ExecuteQuery(query);
            string DungeonName = (string)Data[0]["DungeonName"];

            Array2D mapdata = readMapFile("Assets/Maps/"+DungeonName+ ".tmx");
            if (mapdata != null)
            {
                field.Create(mapdata);
                return;
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
                int w = int.Parse(map.Attribute("width").Value);
                int h = int.Parse(map.Attribute("height").Value);
                if (group == null)
                {
                    return dungeon.Create(w, h, field);
                }

                Array2D data = null;
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
                                field.SetObject(name, type, x/pw, ToMirrorZ(z / ph, h));
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

        private class RandomDungeon
        {
            private const int minArea = 3;
            private const int minRoom = 1;
            private const int margin = 1;
            private Array2D data;
            private List<Area2D> areas;

            /**
            * ダンジョンを作成する
            */
            public Array2D Create(int w, int h, Areamap field)
            {
                data = new Array2D(w, h);
                for (int x = 0; x < data.width; x++)
                {
                    for (int y = 0; y < data.height; y++)
                    {
                        data.Set(x, y, 1);
                    }
                }
                areas = new List<Area2D>();
                Area2D area = new Area2D();
                area.outLine = new Rect2D(0, 0, w - 1, h - 1);
                Split(area, Random.Range(0, 2) == 0);
                CreateRooms();
                SetObjects(field);
                CreateRoads();
                return data;
            }

            /**
            * 区画を分割
            */
            private void Split(Area2D baseArea, bool isVertical)
            {
                Rect2D rect1, rect2;
                if (isVertical)
                {
                    if (baseArea.outLine.left + minArea >= baseArea.outLine.right - minArea)
                    {
                        areas.Add(baseArea);
                        return;
                    }
                    int p = Random.Range(baseArea.outLine.left + minArea, baseArea.outLine.right - minArea);
                    rect1 = new Rect2D(baseArea.outLine.left, baseArea.outLine.top, p, baseArea.outLine.bottom);
                    rect2 = new Rect2D(p + 1, baseArea.outLine.top, baseArea.outLine.right, baseArea.outLine.bottom);
                    if ((rect1.width < rect2.width) ||
                        (rect1.width == rect2.width && Random.Range(0, 2) == 0))
                    {
                        Rect2D tmp = rect1;
                        rect1 = rect2;
                        rect2 = tmp;
                    }
                }
                else
                {
                    if (baseArea.outLine.top + minArea >= baseArea.outLine.bottom - minArea)
                    {
                        areas.Add(baseArea);
                        return;
                    }
                    int p = Random.Range(baseArea.outLine.top + minArea, baseArea.outLine.bottom - minArea);
                    rect1 = new Rect2D(baseArea.outLine.left, baseArea.outLine.top, baseArea.outLine.right, p);
                    rect2 = new Rect2D(baseArea.outLine.left, p + 1, baseArea.outLine.right, baseArea.outLine.bottom);
                    if ((rect1.height < rect2.height) ||
                        (rect1.height == rect2.height && Random.Range(0, 2) == 0))
                    {
                        Rect2D tmp = rect1;
                        rect1 = rect2;
                        rect2 = tmp;
                    }
                }
                Area2D area1 = new Area2D();
                area1.outLine = rect1;
                Area2D area2 = new Area2D();
                area2.outLine = rect2;
                areas.Add(area2);
                Split(area1, !isVertical);
            }

            /**
            * 部屋を作成する
            */
            private void CreateRooms()
            {
                foreach (var area in areas)
                {
                    int aw = area.outLine.width - margin * 2;
                    int ah = area.outLine.height - margin * 2;
                    int width = Random.Range(minRoom, aw);
                    int height = Random.Range(minRoom, ah);
                    int rw = aw - width;
                    int rh = ah - height;
                    int rx = Random.Range(margin, rw - margin);
                    int ry = Random.Range(margin, rh - margin);
                    int left = area.outLine.left + rx;
                    int top = area.outLine.top + ry;
                    int right = left + width;
                    int bottom = top + height;
                    area.room = new Rect2D(left, top, right, bottom);
                    FillRoom(area.room);
                }
            }

            /**
            * マップ配列に部屋を作る
            */
            private void FillRoom(Rect2D room)
            {
                for (int x = room.left; x <= room.right; x++)
                {
                    for (int y = room.top; y <= room.bottom; y++)
                    {
                        data.Set(x, y, 0);
                    }
                }
            }

            /**
            * 水平方向に道をのばす
            */
            private void CreateHorizontalRoad(Area2D area1, Area2D area2)
            {
                int y1 = Random.Range(area1.room.top, area1.room.bottom);
                int y2 = Random.Range(area2.room.top, area2.room.bottom);
                for (int x = area1.room.right; x < area1.outLine.right; x++)
                    data.Set(x, y1, 0);
                for (int x = area2.outLine.left; x < area2.room.left; x++)
                    data.Set(x, y2, 0);
                for (int y = Mathf.Min(y1, y2), end = Mathf.Max(y1, y2); y <= end; y++)
                    data.Set(area1.outLine.right, y, 0);
            }

            /**
            * 垂直方向に道をのばす
            */
            private void CreateVerticalRoad(Area2D area1, Area2D area2)
            {
                int x1 = Random.Range(area1.room.left, area1.room.right);
                int x2 = Random.Range(area2.room.left, area2.room.right);
                for (int y = area1.room.bottom; y < area1.outLine.bottom; y++)
                    data.Set(x1, y, 0);
                for (int y = area2.outLine.top; y < area2.room.top; y++)
                    data.Set(x2, y, 0);
                for (int x = Mathf.Min(x1, x2), end = Mathf.Max(x1, x2); x <= end; x++)
                    data.Set(x, area1.outLine.bottom, 0);
            }

            /**
            * 道を作成
            */
            private void CreateRoads()
            {
                for (int i = 0; i < areas.Count - 1; i++)
                {
                    if (areas[i].outLine.right < areas[i + 1].outLine.left)
                        CreateHorizontalRoad(areas[i], areas[i + 1]);
                    if (areas[i + 1].outLine.right < areas[i].outLine.left)
                        CreateHorizontalRoad(areas[i + 1], areas[i]);
                    if (areas[i].outLine.bottom < areas[i + 1].outLine.top)
                        CreateVerticalRoad(areas[i], areas[i + 1]);
                    if (areas[i + 1].outLine.bottom < areas[i].outLine.top)
                        CreateVerticalRoad(areas[i + 1], areas[i]);
                }
                for (int i = 0; i < areas.Count - 2; i++)
                {
                    if (Random.Range(0, 2) == 0) continue;
                    if (areas[i].outLine.right < areas[i + 2].outLine.left)
                        CreateHorizontalRoad(areas[i], areas[i + 2]);
                    if (areas[i + 2].outLine.right < areas[i].outLine.left)
                        CreateHorizontalRoad(areas[i + 2], areas[i]);
                    if (areas[i].outLine.bottom < areas[i + 2].outLine.top)
                        CreateVerticalRoad(areas[i], areas[i + 2]);
                    if (areas[i + 2].outLine.bottom < areas[i].outLine.top)
                        CreateVerticalRoad(areas[i + 2], areas[i]);
                }

            }

            private void SetObject(string name, string type, Areamap field, Array2D data)
            {
                while (true)
                {
                    int areaIdx = Random.Range(0, areas.Count);
                    Rect2D room = areas[areaIdx].room;
                    int x = Random.Range(room.left, room.right + 1);
                    int y = Random.Range(room.top, room.bottom + 1);
                    if (data.Get(x, y) != 0) continue;
                    data.Set(x, y, 1);
                    field.SetObject(name, type, x, y);
                    break;
                }
            }

            private void SetObjects(Areamap field)
            {
                Array2D tmpData = new Array2D(data.width, data.height);
                for (int x = 0; x < data.width; x++)
                {
                    for (int y = 0; y < data.height; y++)
                    {
                        tmpData.Set(x, y, data.Get(x, y));
                    }
                }
                SetObject("Stairs", "Gimmick", field, tmpData);
                SetObject("Player", "Player", field, tmpData);
            }

            public class Rect2D
            {
                public int left;
                public int top;
                public int right;
                public int bottom;
                public int width { get { return right - left + 1; } }
                public int height { get { return bottom - top + 1; } }

                public Rect2D(int l, int t, int r, int b)
                {
                    left = l;
                    top = t;
                    right = r;
                    bottom = b;
                }
            }

            private class Area2D
            {
                public Rect2D outLine;
                public Rect2D room;
            }
        }
    }
}