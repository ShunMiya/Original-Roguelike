using EnemySystem;
using ItemSystemV2;
using ItemSystemV2.Inventory;
using Minimap;
using MoveSystem;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Field
{
    public class Areamap : MonoBehaviour
    {
        public GameObject floor;
        public GameObject wall;
        public GameObject line;

        public MoveAction playerMovement;
        public GameObject enemies;
        public GameObject items;
        public GameObject gimmicks;
        public GameObject traps;
        public GameObject connections;
        public GameObject rooms;
        public AutoMapping autoMapping;

        private Array2D map;
        private static float onetile = GameRule.GridSize;
        private static float floorSize = 10.0f / onetile;

        /**
        * マップデータの生成
        */
        public void Create(Array2D mapdata)
        {
            map = mapdata;

            autoMapping.ResetMinimap(map.width, map.height);

            ShowFloor();
            ShowWall();

            ShowGridEffects();
        }

        private void ShowFloor()
        {
            float floorw = map.width / floorSize;
            float floorh = map.height / floorSize;
            floor.transform.localScale = new Vector3(floorw, 1, floorh);
            float floorx = (map.width - 1) / 2.0f * onetile;
            float floorz = (map.height - 1) / 2.0f * onetile;
            floor.transform.position = new Vector3(floorx, (float)-0.5, floorz);
        }

        private void ShowWall()
        {
            for (int z = 0; z < map.height; z++)
            {
                for (int x = 0; x < map.width; x++)
                {
                    if (map.Get(x, z) > 0)
                    {
                        GameObject block = Instantiate(wall);
                        float xblock = CoordinateTransformation.ToWorldX(x);
                        float zblock = CoordinateTransformation.ToWorldZ(z);
                        block.transform.localScale = new Vector3(onetile, 1, onetile);
                        block.transform.position = new Vector3(xblock, 0, zblock);
                        block.transform.SetParent(floor.transform.GetChild(0));
                    }
                    autoMapping.Mapping(x, z, map.Get(x, z) + 1);
                }
            }

        }

        private void ShowGridEffects()
        {
            for (int x = 1; x < map.width; x++)
            {
                GameObject obj = Instantiate(line, floor.transform.GetChild(1));
                obj.transform.position = new Vector3(x * onetile - onetile / 2, -0.4f, -onetile / 2);
                obj.transform.localScale = new Vector3(1, 1, floorSize * onetile);
            }
            for (int z = 1; z < map.height; z++)
            {
                GameObject obj = Instantiate(line, floor.transform.GetChild(1));
                obj.transform.position = new Vector3(-onetile / 2, -0.4f, z * onetile - onetile / 2);
                obj.transform.rotation = Quaternion.Euler(0, 90, 0);
                obj.transform.localScale = new Vector3(1, 1, floorSize * onetile);
            }
        }


        public void SetObject(string name, string type, int xgrid, int zgrid, int width, int height)
        {

            switch (type)
            {
                case "Connection":
                    GameObject connectObj = (GameObject)Resources.Load("PrefabsV2/Connection");
                    GameObject connect = Instantiate(connectObj, connections.transform);
                    connect.GetComponent<ObjectPosition>().SetPosition(xgrid, zgrid);
                    break;
                case "Room":
                    GameObject roomObj = (GameObject)Resources.Load("PrefabsV2/Room");
                    GameObject room = Instantiate(roomObj, rooms.transform);
                    room.GetComponent<ObjectPosition>().SetPosition(xgrid, zgrid);
                    room.GetComponent<ObjectPosition>().SetRange(0, 0, width, height);
                    break;
                case "Stairs":
                    GameObject StairsObj = (GameObject)Resources.Load("PrefabsV2/" + name);
                    GameObject stairs = Instantiate(StairsObj, gimmicks.transform);
                    stairs.GetComponent<ObjectPosition>().SetPosition(xgrid, zgrid);
                    break;
                case "Gimmick":
                    SetGimmick(name, xgrid, zgrid);
                    break;
                case "Trap":
                    SetTrap(name, xgrid, zgrid);
                    break;
                case "Enemy":
                    SetEnemy(name, xgrid, zgrid);
                    break;
                case "Item":
                    SetItem(name, xgrid, zgrid);
                    break;
                case "Player":
                    playerMovement.SetPosition(xgrid, zgrid);
                    break;
            }
        }

        public void SetItem(string name, int xgrid, int zgrid)
        {
            if(name.Equals("Random"))
            {
                int itemId = 0;
                string databasePath = SQLDBInitializationV2.GetDatabasePath();
                SqliteDatabase sqlDB = new SqliteDatabase(databasePath);
                string query = "SELECT FloorLevel FROM PlayerStatus WHERE PlayerID = 1;";
                DataTable Data = sqlDB.ExecuteQuery(query);
                int FloorLevel = Convert.ToInt32(Data[0]["FloorLevel"]);
                List<ItemAppearData> ItemList = DungeonDataCache.GetItemsAppearInFloor(FloorLevel);
                int MaxRate = 0;
                foreach (ItemAppearData itemAppear in ItemList) MaxRate += itemAppear.GenerationRate;
                int p = UnityEngine.Random.Range(1, MaxRate + 1);
                foreach (ItemAppearData itemAppear in ItemList)
                {
                    p -= itemAppear.GenerationRate;
                    if (p <= 0)
                    {
                        itemId = itemAppear.ItemId;
                        break;
                    }
                }
                IItemDataV2 itemData = ItemDataCacheV2.GetIItemData(itemId);
                GameObject ItemObj = (GameObject)Resources.Load("PrefabsV2/" + itemData.PrefabName);
                GameObject Item = Instantiate(ItemObj, items.transform);
                Item.GetComponent<MoveAction>().SetPosition(xgrid, zgrid);
                int randomnum = RandomNum.NumSetStock();
                Item.GetComponent<SteppedOnEvent>().num = randomnum;
                return;
            }
            GameObject itemObj = (GameObject)Resources.Load("PrefabsV2/" + name);
            GameObject item = Instantiate(itemObj, items.transform);
            item.GetComponent<MoveAction>().SetPosition(xgrid, zgrid);
            int randomNum = RandomNum.NumSetStock();
            item.GetComponent<SteppedOnEvent>().num = randomNum;
        }

        public void SetEnemy(string name, int xgrid, int zgrid)
        {
            if (name.Equals("Random"))
            {
                int enemyId = 0;
                string databasePath = SQLDBInitializationV2.GetDatabasePath();
                SqliteDatabase sqlDB = new SqliteDatabase(databasePath);
                string query = "SELECT FloorLevel FROM PlayerStatus WHERE PlayerID = 1;";
                DataTable Data = sqlDB.ExecuteQuery(query);
                int FloorLevel = Convert.ToInt32(Data[0]["FloorLevel"]);
                List<EnemyAppearData> EnemyList = DungeonDataCache.GetEnemyAppearInFloor(FloorLevel);
                if (EnemyList.Count == 0) return;
                int MaxRate = 0;
                foreach (EnemyAppearData enemyAppear in EnemyList) MaxRate += enemyAppear.GenerationRate;
                int p = UnityEngine.Random.Range(1, MaxRate + 1);
                foreach (EnemyAppearData enemyAppear in EnemyList)
                {
                    p -= enemyAppear.GenerationRate;
                    if (p <= 0)
                    {
                        enemyId = enemyAppear.EnemyId;
                        break;
                    }
                }
                EnemyDataV2 enemyData = EnemyDataCacheV2.GetEnemyData(enemyId);
                GameObject EnemyObj = (GameObject)Resources.Load("PrefabsV2/" + enemyData.PrefabName);
                GameObject Enemy = Instantiate(EnemyObj, enemies.transform);
                Enemy.GetComponent<MoveAction>().SetPosition(xgrid, zgrid);
                Enemy.GetComponent<EnemyAction>().target = playerMovement;
                return;
            }
            GameObject enemyObj = (GameObject)Resources.Load("PrefabsV2/" + name);
            GameObject enemy = Instantiate(enemyObj, enemies.transform);
            enemy.GetComponent<MoveAction>().SetPosition(xgrid, zgrid);
            enemy.GetComponent<EnemyAction>().target = playerMovement;

        }

        public void SetTrap(string name, int xgrid, int zgrid)
        {
            if(name.Equals("RandomTrap"))
            {
                int trapId = 0;
                List<GimmickAppearData> TrapList = DungeonDataCache.GetGimmickAppearInGimmickType(1);
                int MaxRate = 0;
                foreach (GimmickAppearData gimmickAppear in TrapList) MaxRate += gimmickAppear.GenerationRate;
                int p = UnityEngine.Random.Range(1, MaxRate + 1);
                foreach (GimmickAppearData gimmickAppear in TrapList)
                {
                    p -= gimmickAppear.GenerationRate;
                    if (p <= 0)
                    {
                        trapId = gimmickAppear.GimmickId;
                        break;
                    }
                }
                GimmickData TrapData = GimmickDataCache.GetGimmickData(trapId);
                GameObject TrapObj = (GameObject)Resources.Load("PrefabsV2/" + TrapData.PrefabName);
                GameObject Trap = Instantiate(TrapObj, traps.transform);
                Trap.GetComponent<ObjectPosition>().SetPosition(xgrid, zgrid);
                Trap.GetComponent<SteppedOnEvent>().Id = TrapData.GimmickId; //同一Objのマテリアルを変えて見た目を変えるなら、ここでIdを変える必要がある。別Objなら必要なし。
                Trap.transform.GetChild(0).gameObject.SetActive(false);

                return;
            }
            GimmickData trapData = GimmickDataCache.GetGimmickDataInGimmickName(name);
            GameObject trapObj = (GameObject)Resources.Load("PrefabsV2/" + trapData.PrefabName);
            GameObject trap = Instantiate(trapObj, traps.transform);
            trap.GetComponent<ObjectPosition>().SetPosition(xgrid, zgrid);
            trap.GetComponent<SteppedOnEvent>().Id = trapData.GimmickId; //同上
            trap.transform.GetChild(0).gameObject.SetActive(false);
        }

        public void SetGimmick(string name, int xgrid, int zgrid)
        {
            if (name.Equals("RandomGimmick"))
            {
                int gimmickId = 0;
                List<GimmickAppearData> GimmickList = DungeonDataCache.GetGimmickAppearInGimmickType(2);
                int MaxRate = 0;
                foreach (GimmickAppearData gimmickAppear in GimmickList) MaxRate += gimmickAppear.GenerationRate;
                int p = UnityEngine.Random.Range(1, MaxRate + 1);
                foreach (GimmickAppearData gimmickAppear in GimmickList)
                {
                    p -= gimmickAppear.GenerationRate;
                    if (p <= 0)
                    {
                        gimmickId = gimmickAppear.GimmickId;
                        break;
                    }
                }
                GimmickData GimmickData = GimmickDataCache.GetGimmickData(gimmickId);
                GameObject GimmickObj = (GameObject)Resources.Load("PrefabsV2/" + GimmickData.PrefabName);
                GameObject Gimmick = Instantiate(GimmickObj, traps.transform);
                Gimmick.GetComponent<ObjectPosition>().SetPosition(xgrid, zgrid);
                Gimmick.GetComponent<SteppedOnEvent>().Id = GimmickData.GimmickId; //同一Objのマテリアルを変えて見た目を変えるなら、ここでIdを変える必要がある。別Objなら必要なし。

                return;
            }
            GimmickData gimmickData = GimmickDataCache.GetGimmickDataInGimmickName(name);
            GameObject gimmickObj = (GameObject)Resources.Load("PrefabsV2/" + gimmickData.PrefabName);
            GameObject gimmick = Instantiate(gimmickObj, traps.transform);
            gimmick.GetComponent<ObjectPosition>().SetPosition(xgrid, zgrid);
            gimmick.GetComponent<SteppedOnEvent>().Id = gimmickData.GimmickId; //同上
        }

        public void PopEnemy()
        {
            int RoomCount = rooms.transform.childCount;
            while(true)
            {
                int RandomRoom = UnityEngine.Random.Range(0, RoomCount);
                Transform selectedRoom = rooms.transform.GetChild(RandomRoom);
                ObjectPosition room = selectedRoom.GetComponent<ObjectPosition>();
                if (IsInRoom(room, playerMovement.grid.x, playerMovement.grid.z)) continue;
                int x = UnityEngine.Random.Range(room.grid.x, room.grid.x + room.range.right + 1);
                int y = UnityEngine.Random.Range(room.grid.z, room.grid.z + room.range.bottom + 1);
                if (IsCollide(x, y)) continue;
                SetEnemy("Random", x, y);
                break;
            }

        }

        /**
        * 生成したマップのリセット
        */
        public void Reset()
        {
            Transform walls = floor.transform.GetChild(0);
            for (int i = 0; i < walls.childCount; i++)
                Destroy(walls.GetChild(i).gameObject);
            Transform effects = floor.transform.GetChild(1);
            for (int i = 0; i < effects.childCount; i++)
                Destroy(effects.GetChild(i).gameObject);
            for (int i = 0; i < enemies.transform.childCount; i++)
                Destroy(enemies.transform.GetChild(i).gameObject);
            for (int i = 0; i < items.transform.childCount; i++)
                Destroy(items.transform.GetChild(i).gameObject);
            for (int i = 0; i < gimmicks.transform.childCount; i++)
                Destroy(gimmicks.transform.GetChild(i).gameObject);
            for (int i = 0; i < traps.transform.childCount; i++)
                Destroy(traps.transform.GetChild(i).gameObject);
            for (int i = 0; i < connections.transform.childCount; i++)
                Destroy(connections.transform.GetChild(i).gameObject);
            for (int i = 0; i < rooms.transform.childCount; i++)
                Destroy(rooms.transform.GetChild(i).gameObject);
        }

        /**
         * * マップデータを返す
         */
        public Array2D GetMapData()
        {
            Array2D mapdata = new Array2D(map.width, map.height);
            for (int z = 0; z < map.height; z++)
            {
                for (int x = 0; x < map.width; x++)
                {
                    mapdata.Set(x, z, map.Get(x, z));
                }
            }
            return mapdata;
        }


        /**
        * 指定の座標が移動可能かどうかをチェック
        */
        public bool IsCollide(int xgrid, int zgrid)
        {
            if (map.Get(xgrid, zgrid) != 0) return true;
            if (xgrid == playerMovement.newGrid.x && zgrid == playerMovement.newGrid.z)
                return true;
            foreach (var enemyMovement in enemies.GetComponentsInChildren<MoveAction>())
            {
                if (xgrid == enemyMovement.newGrid.x && zgrid == enemyMovement.newGrid.z)
                    return true;
            }
            return false;
        }

        public bool IsCollidediagonal(int xgrid, int zgrid)
        {
            if (map.Get(xgrid, zgrid) != 0) return true;
            return false;
        }

        public GameObject IsCollideReturnCharObj(int xgrid, int zgrid)
        {
            if (xgrid == playerMovement.newGrid.x && zgrid == playerMovement.newGrid.z)
                return playerMovement.gameObject;
            foreach (var enemyMovement in enemies.GetComponentsInChildren<MoveAction>())
            {
                if (xgrid == enemyMovement.newGrid.x && zgrid == enemyMovement.newGrid.z)
                    return enemyMovement.gameObject;
            }
            return null;
        }

        public GameObject IsCollideReturnAreaObj(int xgrid, int zgrid)
        {
            foreach (var itemMovement in items.GetComponentsInChildren<MoveAction>())
            {
                if (xgrid == itemMovement.grid.x && zgrid == itemMovement.grid.z)
                    return itemMovement.gameObject;
            }
            foreach (var gimmickPosition in gimmicks.GetComponentsInChildren<ObjectPosition>())
            {
                if (xgrid == gimmickPosition.grid.x && zgrid == gimmickPosition.grid.z)
                    return gimmickPosition.gameObject;
            }
            foreach (var trapPosition in traps.GetComponentsInChildren<ObjectPosition>())
            {
                if (xgrid == trapPosition.grid.x && zgrid == trapPosition.grid.z)
                    return trapPosition.gameObject;
            }

            return null;
        }

        public bool IsCollidePutItem(int xgrid, int zgrid)
        {
            if (map.Get(xgrid, zgrid) != 0) return false;
            foreach (var itemMovement in items.GetComponentsInChildren<MoveAction>())
            {
                if (xgrid == itemMovement.grid.x && zgrid == itemMovement.grid.z)
                    return false;
            }
            foreach (var gimmickPosition in gimmicks.GetComponentsInChildren<ObjectPosition>())
            {
                if (xgrid == gimmickPosition.grid.x && zgrid == gimmickPosition.grid.z)
                    return false;
            }
            foreach (var trapPosition in traps.GetComponentsInChildren<ObjectPosition>())
            {
                if (xgrid == trapPosition.grid.x && zgrid == trapPosition.grid.z)
                    return false;
            }

            return true;
        }

        //現在座標(CurrentPos)と角度(R)を元に攻撃範囲にエネミーorプレイヤーがいるかを攻撃射程分(range)チェック
        //攻撃することが確定しているときの処理(当たる当たらないは考慮しない)
        public GameObject IsCollideHit(Pos2D CurrentPos, int R, int range)
        {
            Pos2D Pos = DirUtil.SetAttackPoint(R);
            int xgrid = CurrentPos.x;
            int zgrid = CurrentPos.z;

            for (int i = 1; i <= range; i++)
            {
                xgrid += Pos.x;
                zgrid += Pos.z;
                if (xgrid == playerMovement.grid.x && zgrid == playerMovement.grid.z)
                    return playerMovement.gameObject;
                foreach(var trapPosition in traps.GetComponentsInChildren<ObjectPosition>())
                {
                    if(xgrid == trapPosition.grid.x &&zgrid == trapPosition.grid.z)
                        trapPosition.gameObject.transform.GetChild(0).gameObject.SetActive(true);
                }
                foreach (var enemyMovement in enemies.GetComponentsInChildren<MoveAction>())
                {
                    if (xgrid == enemyMovement.grid.x && zgrid == enemyMovement.grid.z)
                        return enemyMovement.gameObject;
                }
            }
            return null;
        }

        //エネミーの行動決定処理。プレイヤーがエネミーの攻撃範囲に移動してこないかチェック
        public int IsPlayerHitCheckBeforeMoving(Pos2D CurrentPos, int range)
        {
            foreach (Dir d in System.Enum.GetValues(typeof(Dir)))
            {
                Vector3 Rota =DirUtil.SetNewPosRotation(d);
                Pos2D Pos = DirUtil.SetAttackPoint((int)Rota.y);
                int xgrid = CurrentPos.x;
                int zgrid = CurrentPos.z;

                for (int i = 1; i <= range; i++)
                {
                    xgrid += Pos.x;
                    zgrid += Pos.z;
                    if (xgrid == playerMovement.newGrid.x && zgrid == playerMovement.newGrid.z)
                        return (int)Rota.y;
                }
            }
            return 1;
        }

        //全体移動後プレイヤーに本当に攻撃が当たるか確認処理。当たらないなら攻撃しない
        public bool IsPlayerHitCheckAfterMoving(Pos2D CurrentPos, int R, int range)
        {
            Pos2D Pos = DirUtil.SetAttackPoint(R);
            int xgrid = CurrentPos.x;
            int zgrid = CurrentPos.z;

            for (int i = 1; i <= range; i++)
            {
                xgrid += Pos.x;
                zgrid += Pos.z;
                if (xgrid == playerMovement.grid.x && zgrid == playerMovement.grid.z)
                    return true;
            }
            return false;
        }

        public Pos2D ItemDropPointCheck(Pos2D pos)
        {
            Pos2D setPos = null;
            foreach (Dir d in System.Enum.GetValues(typeof(Dir)))
            {
                Pos2D newPos = DirUtil.GetNewGrid(pos, d);
                bool PutItem = IsCollidePutItem(newPos.x, newPos.z);
                if (PutItem == true)
                {
                    setPos = new Pos2D { x = newPos.x, z = newPos.z };
                    break;
                }
            }
            return setPos;
        }

        //渡された座標が渡された部屋内かどうか
        public bool IsInRoom(ObjectPosition room, int xgrid, int zgrid)
        {
            int leftx = room.grid.x + room.range.left;
            int rightx = leftx + room.range.right;
            int topz = room.grid.z + room.range.top;
            int bottomz = topz + room.range.bottom;
            return xgrid >= leftx && xgrid <= rightx && zgrid >= topz && zgrid <= bottomz;
        }

        //渡された座標が部屋内であれば部屋を返す。
        public ObjectPosition GetInRoom(int xgrid, int zgrid)
        {
            foreach (var room in rooms.GetComponentsInChildren<ObjectPosition>())
            {
                if (IsInRoom(room, xgrid, zgrid))
                {
                    return room;
                }
            }
            return null;
        }
    }
}