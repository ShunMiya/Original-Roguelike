using MoveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AttackSystem;
using Field;
using System.Linq;
using UnityEditor;

namespace EnemySystem
{
    public class EnemyAction : MonoBehaviour
    {
        public MoveAction moveAction;
        public AttackAction attackAction;
        private EnemyStatusV2 enemyStatus;
        private EnemyDataV2 enemy;
        private Areamap areamap;
        public MoveAction target;

        public Pos2D nextConnectionPos;
        public int detectDistance = 4;
        private bool OldDetectTarget = false;
        public void Start()
        {
            enemyStatus = GetComponent<EnemyStatusV2>();
            enemy = EnemyDataCacheV2.GetEnemyData(enemyStatus.EnemyID);
            areamap = FindObjectOfType<Areamap>();
            nextConnectionPos = moveAction.grid;
        }
        public void EnemyActionSet()
        {
            //StatusのIDを元にCacheの行動タイプを確認しそれに応じて処理分岐。
            switch(enemy.AIType)
            {
                case 0: //基本移動
                    Patrol();
                    break;
                case 1: //逃走
                    Escape();
                    break;
                case 2: //不動
                    NoMove();
                    break;
                case 3: //気紛れ
                    Freaky();
                    break;
                default:
                    AllRandom();
                    break;
            }
        }

        private void Patrol()
        {
            bool NowDetectTarget = DetectTarget();

            if(OldDetectTarget && !NowDetectTarget)
            {
                nextConnectionPos = target.grid;
            }

            OldDetectTarget = NowDetectTarget;

            if(NowDetectTarget)
            {
                Tracking();
                return;
            }
            GoAroundAI();
        }

        private bool DetectTarget()
        {
            Areamap field = GetComponentInParent<Areamap>();
            Pos2D agrid = moveAction.grid;
            Pos2D tgrid = target.newGrid;
            ObjectPosition room = field.GetInRoom(agrid.x, agrid.z);

            if(room != null)
            {
                if (field.IsInRoom(room, tgrid.x, tgrid.z)) return true;
            }
            
            if (agrid.x == tgrid.x && Mathf.Abs(agrid.z - tgrid.z) <= detectDistance)
            {
                for (int z = Mathf.Min(agrid.z, tgrid.z), ez = Mathf.Max(agrid.z, tgrid.z); z < ez; z++)
                {
                    if (field.IsCollidediagonal(agrid.x, z)) return false;
                }
                return true;
            }
            if (agrid.z == tgrid.z && Mathf.Abs(agrid.x - tgrid.x) <= detectDistance)
            {
                for (int x = Mathf.Min(agrid.x, tgrid.x), ex = Mathf.Max(agrid.x, tgrid.x); x < ex; x++)
                {
                    if (field.IsCollidediagonal(x, agrid.z)) return false;
                }
                return true;
            }
            return false;
        }

        private void GoAroundAI()
        {
            Pos2D grid = moveAction.grid;
            int R = (int)transform.rotation.eulerAngles.y;
            if (R > 180) R -= 360;
            Dir dir = DirUtil.GetDirection(R);
            if (grid.x == nextConnectionPos.x && grid.z == nextConnectionPos.z)
            {
                SetNextConnection(grid.x, grid.z, dir);
                if (grid.x == nextConnectionPos.x && grid.z == nextConnectionPos.z)
                {
                    dir = DirUtil.GetDirection(DirUtil.ReverseDirection(R));
                    SetNextConnection(grid.x, grid.z, dir);
                }
            }
            Vector3 PosRota = DirUtil.SetNewPosRotation(DirUtil.GetNewPosRotation(grid, nextConnectionPos));
            transform.rotation = Quaternion.Euler(0, PosRota.y, 0);
            bool move = moveAction.MoveStance(PosRota.x, PosRota.z);
            if (!move)
            {
                dir = DirUtil.GetDirection(DirUtil.ReverseDirection(R));
                SetNextConnection(grid.x, grid.z, dir);
                PosRota = DirUtil.SetNewPosRotation(DirUtil.GetNewPosRotation(grid, nextConnectionPos));
                transform.rotation = Quaternion.Euler(0, PosRota.y, 0);
            }
        }

        public void SetNextConnection(int xgrid, int zgrid, Dir dir)
        {
            Areamap field = GetComponentInParent<Areamap>();
            ObjectPosition[] pos = field.connections.GetComponentsInChildren<ObjectPosition>();
            List<ObjectPosition> posList = new List<ObjectPosition>();
            posList.AddRange(pos);
            List<Pos2D> cGrids = new List<Pos2D>();
            foreach (var p in posList)
            {
                if (p.grid.x == xgrid && p.grid.z == zgrid) continue;
                if (dir == Dir.Left && p.grid.x > xgrid) continue;
                if (dir == Dir.Right && p.grid.x < xgrid) continue;
                if (dir == Dir.Up && p.grid.z < zgrid) continue;
                if (dir == Dir.Down && p.grid.z > zgrid) continue;
                if (dir == Dir.LeftUp && p.grid.x > xgrid && p.grid.z < zgrid) continue;
                if (dir == Dir.RightUp && p.grid.x < xgrid && p.grid.z < zgrid) continue;
                if (dir == Dir.LeftDown &&  p.grid.x > xgrid &&p.grid.z > zgrid) continue;
                if (dir == Dir.RightDown && p.grid.x < xgrid && p.grid.z > zgrid) continue;
                int minX = Mathf.Min(p.grid.x, xgrid);
                int maxX = Mathf.Max(p.grid.x, xgrid);
                int minZ = Mathf.Min(p.grid.z, zgrid);
                int maxZ = Mathf.Max(p.grid.z, zgrid);
                bool isEnd = false;
                for (int x = minX; x <= maxX; x++)
                {
                    for (int z = minZ; z <= maxZ; z++)
                    {
                        if (field.IsCollidediagonal(x, z))
                        {
                            isEnd = true;
                            break;
                        }
                    }
                    if (isEnd) break;
                }
                if (isEnd) continue;
                cGrids.Add(p.grid);
            }
            if (cGrids.Count < 1)
            {
                nextConnectionPos = moveAction.grid;
                return;
            }
            int idx = Random.Range(0, cGrids.Count);
            nextConnectionPos = cGrids[idx];
        }

        private void Tracking()
        {
            int Rota = areamap.IsPlayerHitCheckBeforeMoving(moveAction.grid, enemy.Range);
            if (Rota != 1)
            {
                attackAction.EnemyY = Rota;
                AttackObjects attackObjects = FindObjectOfType<AttackObjects>();
                attackObjects.objectsToAttack.Add(attackAction);
                return;
            }
            Dir d = AstarMovementAI();
            Vector3 PosRota = DirUtil.SetNewPosRotation(d);
            transform.rotation = Quaternion.Euler(0, PosRota.y, 0);
            moveAction.MoveStance(PosRota.x, PosRota.z);
        }

        private void Escape()
        {
            Dir d = AstarEscapeMovementAI();
            Vector3 PosRota = DirUtil.SetNewPosRotation(d);
            transform.rotation = Quaternion.Euler(0, PosRota.y, 0);
            moveAction.MoveStance(PosRota.x, PosRota.z);
        }

        private void NoMove()
        {
            if(DetectTarget())
            {
                int Rota = areamap.IsPlayerHitCheckBeforeMoving(moveAction.grid, enemy.Range);
                if (Rota == 1) return;
                attackAction.EnemyY = Rota;
                AttackObjects attackObjects = FindObjectOfType<AttackObjects>();
                attackObjects.objectsToAttack.Add(attackAction);
            }
        }

        private void Freaky()
        {
            if(Random.Range(0,2) > 0)
            {
                Tracking();
                return;
            }
            AllRandom();
        }

        private void AllRandom()
        {
            Vector3 PosRota = DirUtil.SetNewPosRotation(DirUtil.RandomDirection());
            if (Random.Range(0, 3) > 0)
            {
                transform.rotation = Quaternion.Euler(0, PosRota.y, 0);
                moveAction.MoveStance(PosRota.x, PosRota.z);
                return;
            }
            attackAction.EnemyY = (int)PosRota.y;
        }

        /**
        * A*アルゴリズムを用いた移動AI
        */
        private Dir AstarMovementAI()
        {
            Node node = new Node();
            return node.GetAstarNextDirection(moveAction.grid, target.newGrid, GetComponentInParent<Areamap>());
        }

        private Dir AstarEscapeMovementAI()
        {
            Node node = new Node();
            return node.GetAstarEscapeNextDirection(moveAction.grid, target.newGrid, GetComponentInParent<Areamap>());
        }


        private class Node
        {
            public Pos2D grid;
            public Dir direction;
            public int actualCost = 0;
            public int estimatedCost = 0;
            public Node parentNode = null;
            public int EscapeCount = 0;

            /**
            * A*アルゴリズムにて算出した方向を返す
            */
            public Dir GetAstarNextDirection(Pos2D pos, Pos2D target, Areamap field)
            {
                grid = new Pos2D();
                grid.x = pos.x;
                grid.z = pos.z;
                Array2D nodeMap = field.GetMapData();
                nodeMap.Set(grid.x, grid.z, 2);
                Node node = Astar(target, field, new List<Node>(), nodeMap);
                if (node.parentNode == null) return Dir.Pause;
                while (node.parentNode.parentNode != null) node = node.parentNode;
                return node.direction;
            }

            public Dir GetAstarEscapeNextDirection(Pos2D pos, Pos2D target, Areamap field)
            {
                grid = new Pos2D();
                grid.x = pos.x;
                grid.z = pos.z;
                Array2D nodeMap = field.GetMapData();
                nodeMap.Set(grid.x, grid.z, 2);
                Node node = AstarEscape(target, field, new List<Node>(), nodeMap);
                if (node.parentNode == null) return Dir.Pause;
                while (node.parentNode.parentNode != null) node = node.parentNode;
                return node.direction;
            }

            /**
            * 再起的にA*アルゴリズムを計算し、結果を返す
            */
            private Node Astar(Pos2D target, Areamap field, List<Node> openList, Array2D nodeMap)
            {
                foreach (Dir d in System.Enum.GetValues(typeof(Dir)))
                {
                    if (d == Dir.Pause) continue;
                    if (target.x == grid.x && target.z == grid.z) return this;
                    Pos2D newGrid = DirUtil.GetNewGrid(grid, d);
                    if (nodeMap.AStarGet(newGrid.x, newGrid.z, d) > 0) continue;
                    Node node = new Node();
                    node.grid = newGrid;
                    node.direction = d;
                    node.parentNode = this;
                    node.actualCost = node.parentNode.actualCost + 1;
                    node.actualCost += field.IsCollideReturnCharObj(node.grid.x, node.grid.z) == null ? 0 : field.enemies.transform.childCount * 2;
                    node.estimatedCost = Mathf.Abs(target.x - node.grid.x) + Mathf.Abs(target.z - node.grid.z);
                    if (openList.All(n => n.grid != node.grid))
                    {
                        openList.Add(node);
                        nodeMap.Set(node.grid.x, node.grid.z, 2);
                    }

                }
                if (openList.Count < 1) return this;
                openList = openList.OrderBy(n => (n.actualCost + n.estimatedCost)).ThenBy(n => n.actualCost).ToList();
                Node baseNode = openList[0];
                openList.RemoveAt(0);
                return baseNode.Astar(target, field, openList, nodeMap);
            }

            //移動コスト５の位置で最も遠いところに向かう処理にしたいが未完成
            private Node AstarEscape(Pos2D target, Areamap field, List<Node> openList, Array2D nodeMap) 
            {
                foreach (Dir d in System.Enum.GetValues(typeof(Dir)))
                {
                    if (d == Dir.Pause) continue;
                    if (actualCost == 5) return this;
                    Pos2D newGrid = DirUtil.GetNewGrid(grid, d);
                    if (nodeMap.AStarGet(newGrid.x, newGrid.z, d) > 0) continue;
                    Node node = new Node();
                    node.grid = newGrid;
                    node.direction = d;
                    node.parentNode = this;
                    node.actualCost = node.parentNode.actualCost + 1;
                    node.estimatedCost = Mathf.Abs(target.x - node.grid.x) + Mathf.Abs(target.z - node.grid.z);
                    if (openList.All(n => n.grid != node.grid))
                    {
                        openList.Add(node);
                        nodeMap.Set(node.grid.x, node.grid.z, 2);
                    }

                }
                if (openList.Count < 1) return this;
                openList = openList.OrderByDescending(n => (n.actualCost + n.estimatedCost)).ThenByDescending(n => n.actualCost).ToList();
                Node baseNode = openList[0];
                openList.RemoveAt(0);
                return baseNode.AstarEscape(target, field, openList, nodeMap);
            }
        }
    }
}