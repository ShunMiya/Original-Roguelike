using MoveSystem;
using System.Collections;
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

        private Array2D map;
        private static float onetile = GameRule.GridSize;
        private static float floorSize = 10.0f / onetile;

        /**
        * �}�b�v�f�[�^�̐���
        */
        public void Create(Array2D mapdata)
        {
            map = mapdata;

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

        /**
        * ���������}�b�v�̃��Z�b�g
        */
        public void Reset()
        {
            Transform walls = floor.transform.GetChild(0);
            for (int i = 0; i < walls.childCount; i++)
            {
                Destroy(walls.GetChild(i).gameObject);
            }
            Transform effects = floor.transform.GetChild(1);
            for (int i = 0; i < effects.childCount; i++)
            {
                Destroy(effects.GetChild(i).gameObject);
            }
            for (int i = 0; i < enemies.transform.childCount; i++)
            {
                Destroy(enemies.transform.GetChild(i).gameObject);
            }
        }

        /**
         * * �}�b�v�f�[�^��Ԃ�
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
        * �w��̍��W���ړ��\���ǂ������`�F�b�N
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

        public GameObject IsCollideReturnObj(int xgrid, int zgrid)
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

        //���ݍ��W(CurrentPos)�Ɗp�x(R)�����ɍU���͈͂ɃG�l�~�[or�v���C���[�����邩���U���˒���(range)�`�F�b�N
        //�U�����邱�Ƃ��m�肵�Ă���Ƃ��̏���(�����铖����Ȃ��͍l�����Ȃ�)
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
                foreach (var enemyMovement in enemies.GetComponentsInChildren<MoveAction>())
                {
                    if (xgrid == enemyMovement.grid.x && zgrid == enemyMovement.grid.z)
                        return enemyMovement.gameObject;
                }
            }
            return null;
        }

        //�G�l�~�[�̍s�����菈���B�v���C���[���G�l�~�[�̍U���͈͂Ɉړ����Ă��Ȃ����`�F�b�N
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

        //�S�̈ړ���v���C���[�ɖ{���ɍU���������邩�m�F�����B������Ȃ��Ȃ�U�����Ȃ�
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
    }
}