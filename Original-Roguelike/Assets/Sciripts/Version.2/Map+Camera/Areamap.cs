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

        //���ݍ��W(CurrentPos)�Ɗp�x(R)�����ɃG�l�~�[or�v���C���[�����邩���U���˒���(range)�`�F�b�N
        public GameObject IsCollideHit(Pos2D CurrentPos,int R, int range)
        {
            Pos2D Pos =DirUtil.SetAttackPoint(R);
            int xgrid = CurrentPos.x;
            int zgrid = CurrentPos.z;

            for (int i=1; i < range; i++)
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
    }
}