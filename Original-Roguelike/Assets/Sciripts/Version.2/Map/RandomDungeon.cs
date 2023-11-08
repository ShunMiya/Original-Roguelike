using System.Collections.Generic;
using UnityEngine;
using ItemSystemV2.Inventory;
using System;
using Random = UnityEngine.Random;

namespace Field
{
    public class RandomDungeon : MonoBehaviour
    {
        private const int minArea = 7;
        private const int maxArea = 10;
        private const int minRoom = 3;
        private const int margin = 2;
        private Array2D data;
        private List<Area2D> areas;
        private int Proom;
        private int PRCount = 0;



        /**
        * ダンジョンを作成する
        */
        public Array2D Create(int w, int h, Areamap field)
        {
            PRCount = 0;

            data = new Array2D(w + 4, h + 4); //外枠２マスで壁を生成する
            for (int x = 0; x < data.width; x++)
            {
                for (int y = 0; y < data.height; y++)
                {
                    data.Set(x, y, 1);
                }
            }
            areas = new List<Area2D>();
            Area2D area = new Area2D();
            area.outLine = new Rect2D(2, 2, w + 2, h + 2); //内側に30*30で初期エリア作成
            Split(area, Random.Range(0, 2) == 0);
            CreateRooms(field);
            CreateRoads(field);
            SetObjects(field);
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
        private void CreateRooms(Areamap field)
        {
            foreach (var area in areas)
            {
                int aw = area.outLine.width - margin * 2;
                int ah = area.outLine.height - margin * 2;
                int width = Random.Range(minRoom - 1, aw);
                int height = Random.Range(minRoom - 1, ah);
                int rw = area.outLine.width - width;
                int rh = area.outLine.height - height;
                int rx = Random.Range(margin, rw - margin);
                int ry = Random.Range(margin, rh - margin);
                int left = area.outLine.left + rx;
                int top = area.outLine.top + ry;
                int right = left + width;
                int bottom = top + height;

                area.room = new Rect2D(left, top, right, bottom);
                FillRoom(area.room);
                field.SetObject("Room", "Room", left, top, width + 1, height + 1);
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
        private void CreateHorizontalRoad(Area2D area1, Area2D area2, Areamap field)
        {
            int y1 = Random.Range(area1.room.top, area1.room.bottom);
            int y2 = Random.Range(area2.room.top, area2.room.bottom);
            for (int x = area1.room.right; x < area1.outLine.right; x++)
            {
                if (data.Get(x, y1 - 1) == 0) y1--;
                if (data.Get(x, y1 + 1) == 0) y1++;
                data.Set(x, y1, 0);
            }
            for (int x = area2.outLine.left; x < area2.room.left; x++)
            {
                if (data.Get(x, y2 - 1) == 0) y2--;
                if (data.Get(x, y2 + 1) == 0) y2++;
                data.Set(x, y2, 0);
            }
            for (int y = Mathf.Min(y1, y2), end = Mathf.Max(y1, y2); y <= end; y++)
            {
                int outLinex = area1.outLine.right;
                if (y == Mathf.Min(y1, y2) || y == Mathf.Max(y1, y2))
                {
                    data.Set(outLinex, y, 0);
                    continue;
                }
                if (data.Get(area1.outLine.right - 1, y) == 0) outLinex--;
                if (data.Get(area1.outLine.right + 1, y) == 0) outLinex++;

                data.Set(outLinex, y, 0);
            }
            field.SetObject("Connection", "Connection", area1.room.right, y1, 1, 1);
            field.SetObject("Connection", "Connection", area1.outLine.right, y1, 1, 1);
            field.SetObject("Connection", "Connection", area2.room.left, y2, 1, 1);
            field.SetObject("Connection", "Connection", area2.outLine.left - 1, y2, 1, 1);
        }

        /**
        * 垂直方向に道をのばす
        */
        private void CreateVerticalRoad(Area2D area1, Area2D area2, Areamap field)
        {
            int x1 = Random.Range(area1.room.left, area1.room.right);
            int x2 = Random.Range(area2.room.left, area2.room.right);
            for (int y = area1.room.bottom; y < area1.outLine.bottom; y++)
            {
                if (data.Get(x1 - 1, y) == 0) x1--;
                if (data.Get(x1 + 1, y) == 0) x1++;
                data.Set(x1, y, 0);
            }
            for (int y = area2.outLine.top; y < area2.room.top; y++)
            {
                if (data.Get(x2 - 1, y) == 0) x2--;
                if (data.Get(x2 + 1, y) == 0) x2++;
                data.Set(x2, y, 0);
            }
            for (int x = Mathf.Min(x1, x2), end = Mathf.Max(x1, x2); x <= end; x++)
            {
                int outLiney = area1.outLine.bottom;
                if(x == Mathf.Min(x1, x2) || x == Mathf.Max(x1, x2))
                {
                    data.Set(x, outLiney, 0);
                    continue;
                }
                if (data.Get(x, area1.outLine.bottom - 1) == 0) outLiney--;
                if (data.Get(x, area1.outLine.bottom + 1) == 0) outLiney++;

                data.Set(x, outLiney, 0);
            }
            field.SetObject("Connection", "Connection", x1, area1.room.bottom, 1, 1);
            field.SetObject("Connection", "Connection", x1, area1.outLine.bottom, 1, 1);
            field.SetObject("Connection", "Connection", x2, area2.room.top, 1, 1);
            field.SetObject("Connection", "Connection", x2, area2.outLine.top - 1, 1, 1);
        }

        /**
        * 道を作成
        */
        private void CreateRoads(Areamap field)
        {
            for (int i = 0; i < areas.Count - 1; i++)
            {
                if (areas[i].outLine.right < areas[i + 1].outLine.left)
                    CreateHorizontalRoad(areas[i], areas[i + 1], field);
                if (areas[i + 1].outLine.right < areas[i].outLine.left)
                    CreateHorizontalRoad(areas[i + 1], areas[i], field);
                if (areas[i].outLine.bottom < areas[i + 1].outLine.top)
                    CreateVerticalRoad(areas[i], areas[i + 1], field);
                if (areas[i + 1].outLine.bottom < areas[i].outLine.top)
                    CreateVerticalRoad(areas[i + 1], areas[i], field);
            }
            for (int i = 0; i < areas.Count - 2; i++)
            {
                if (areas[i].outLine.right < areas[i + 2].outLine.left)
                    CreateHorizontalRoad(areas[i], areas[i + 2], field);
                if (areas[i + 2].outLine.right < areas[i].outLine.left)
                    CreateHorizontalRoad(areas[i + 2], areas[i], field);
                if (areas[i].outLine.bottom < areas[i + 2].outLine.top)
                    CreateVerticalRoad(areas[i], areas[i + 2], field);
                if (areas[i + 2].outLine.bottom < areas[i].outLine.top)
                    CreateVerticalRoad(areas[i + 2], areas[i], field);
            }
            for (int i = 0; i < areas.Count - 3; i++)
            {
                if (Random.Range(0, 3) == 0) continue;
                if (areas[i].outLine.right < areas[i + 3].outLine.left)
                    CreateHorizontalRoad(areas[i], areas[i + 3], field);
                if (areas[i + 3].outLine.right < areas[i].outLine.left)
                    CreateHorizontalRoad(areas[i + 3], areas[i], field);
                if (areas[i].outLine.bottom < areas[i + 3].outLine.top)
                    CreateVerticalRoad(areas[i], areas[i + 3], field);
                if (areas[i + 3].outLine.bottom < areas[i].outLine.top)
                    CreateVerticalRoad(areas[i + 3], areas[i], field);
            }

        }

        private void SetObject(string name, string type, Areamap field, Array2D data)
        {
            while (true)
            {
                int areaIdx = Random.Range(0, areas.Count);

                if(type == "Enemy" && areaIdx == Proom)
                {
                    if (PRCount >= 3) continue;
                    PRCount++;
                }

                Rect2D room = areas[areaIdx].room;
                int x = Random.Range(room.left, room.right + 1);
                int y = Random.Range(room.top, room.bottom + 1);
                if (data.Get(x, y) != 0) continue;
                if (name == "Player") Proom = areaIdx;
                data.Set(x, y, 1);
                field.SetObject(name, type, x, y, 1, 1);
                break;
            }
        }

        private void SetObjectPlayerRoomAvoidance(string name, string type, Areamap field, Array2D data)
        {
            while (true)
            {
                int areaIdx = Random.Range(0, areas.Count);

                if (areaIdx == Proom) continue;

                Rect2D room = areas[areaIdx].room;
                int x = Random.Range(room.left, room.right + 1);
                int y = Random.Range(room.top, room.bottom + 1);
                if (data.Get(x, y) != 0) continue;
                data.Set(x, y, 1);
                field.SetObject(name, type, x, y, 1, 1);
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
            SetObject("Stairs", "Stairs", field, tmpData);
            SetObject("Player", "Player", field, tmpData);

            string databasePath = SQLDBInitializationV2.GetDatabasePath();
            SqliteDatabase sqlDB = new SqliteDatabase(databasePath);
            string query = "SELECT FloorLevel FROM PlayerStatus WHERE PlayerID = 1;";
            DataTable Data = sqlDB.ExecuteQuery(query);
            int FloorLevel = Convert.ToInt32(Data[0]["FloorLevel"]);
            FloorInfomationData FloorInfo = DungeonDataCache.GetFloorInformation(FloorLevel);

            int PopItem = Random.Range(FloorInfo.MinItems, FloorInfo.MaxItems + 1);
            for (int i = 0; i < PopItem; i++) SetObject("Random", "Item", field, tmpData);

            int PopEnemy = Random.Range(FloorInfo.MinEnemies, FloorInfo.MaxEnemies + 1);
            for (int i = 0; i < PopEnemy; i++)
            {
                if(FloorLevel <= 2) SetObjectPlayerRoomAvoidance("Random", "Enemy", field, tmpData);
                else SetObject("Random", "Enemy", field, tmpData);
            }

            int PopTrap = FloorInfo.TrapNum;
            for (int i = 0; i < PopTrap; i++) SetObject("Random", "Trap", field, tmpData);
        }

        private class Area2D
        {
            public Rect2D outLine;
            public Rect2D room;
        }
    }

}