using Field;
using ItemSystemV2;
using MoveSystem;
using UnityEngine;

namespace Minimap
{
    public class AutoMapping : MonoBehaviour
    {
        public GameObject roads;
        public GameObject roadImage;
        public GameObject roomImage;

        public GameObject enemies;
        public GameObject enemiesObj;
        public GameObject enemyIcon;

        public GameObject items;
        public GameObject itemsObj;
        public GameObject itemIcon;

        public GameObject gimmicks;
        public GameObject gimmicksObj;
        public GameObject gimmickIcon;
        public GameObject stairsIcon;

        public GameObject traps;
        public GameObject trapsObj;
        public GameObject trapIcon;

        public GameObject playerObj;
        public GameObject playerIcon;

        public Areamap field;

        private float pw, ph;
        private Array2D map;

        private void Awake()
        {
            RectTransform rect = roadImage.GetComponent<RectTransform>();
            pw = rect.sizeDelta.x;
            ph = rect.sizeDelta.y;
        }

        public void ObjMapping()
        {

            ShowPlayerObj();
            ShowMoveObj(enemyIcon, enemies, enemiesObj);
            ShowNoMoveObj(itemIcon, items, itemsObj);
            ShowGimmickObj();
            ShowTrapObj();
        }

        public void ShowPlayerObj()
        {
            if(playerIcon.activeSelf == false) playerIcon.SetActive(true);
            Pos2D tgrid = playerObj.GetComponent<MoveAction>().grid;
            playerIcon.GetComponent<RectTransform>().anchoredPosition = new Vector2(pw * tgrid.x, ph * tgrid.z);
        }

        public void ShowMoveObj(GameObject Icon, GameObject imgs, GameObject objs)
        {
            for (int i = imgs.transform.childCount; i < objs.transform.childCount; i++)
                Instantiate(Icon, imgs.transform);
            for (int i = objs.transform.childCount; i < imgs.transform.childCount; i++)
                Destroy(imgs.transform.GetChild(i).gameObject);

            for (int i = 0; i < objs.transform.childCount; i++)
            {
                Pos2D agrid = objs.transform.GetChild(i).GetComponent<MoveAction>().grid;
                Pos2D tgrid = playerObj.GetComponent<MoveAction>().grid;
                ObjectPosition room = field.GetInRoom(agrid.x, agrid.z);
                Transform img = imgs.transform.GetChild(i);

                if (room != null)
                {
                    if (field.IsInRoom(room, tgrid.x, tgrid.z))
                    {
                        img.GetComponent<RectTransform>().anchoredPosition = new Vector2(pw * agrid.x, ph * agrid.z);
                        img.gameObject.SetActive(true);
                        continue;
                    }
                }
                if (Mathf.Abs(agrid.x - tgrid.x) <= 1 && Mathf.Abs(agrid.z - tgrid.z) <= 1)
                {
                    img.GetComponent<RectTransform>().anchoredPosition = new Vector2(pw * agrid.x, ph * agrid.z);
                    img.gameObject.SetActive(true);
                }
                else img.gameObject.SetActive(false);
            }

        }

        public void ShowNoMoveObj(GameObject image, GameObject imgs, GameObject objs)
        {
            for (int i = imgs.transform.childCount; i < objs.transform.childCount; i++)
                Instantiate(image, imgs.transform);
            for (int i = objs.transform.childCount; i < imgs.transform.childCount; i++)
                Destroy(imgs.transform.GetChild(i).gameObject);
            for (int i = 0; i < objs.transform.childCount; i++)
            {
                Pos2D p = objs.transform.GetChild(i).GetComponent<MoveAction>().grid;
                Transform img = imgs.transform.GetChild(i);
                if (map.Get(p.x, p.z) == 1)
                {
                    img.GetComponent<RectTransform>().anchoredPosition = new Vector2(pw * p.x, ph * p.z);
                    img.gameObject.SetActive(true);
                }
                else img.gameObject.SetActive(false);
            }
        }

        public void ShowGimmickObj()
        {
            for (int i = gimmicks.transform.childCount; i < gimmicksObj.transform.childCount; i++)
            {
                switch (gimmicksObj.transform.GetChild(i).GetComponent<SteppedOnEvent>().ObjType)
                {
                    case 1:
                        Instantiate(gimmickIcon, gimmicks.transform);
                        break;
                    case 2:
                        Instantiate(stairsIcon, gimmicks.transform);
                        break;
                }
            }
            for (int i = gimmicksObj.transform.childCount; i < gimmicks.transform.childCount; i++)
                Destroy(gimmicks.transform.GetChild(i).gameObject);
            for (int i = 0; i < gimmicksObj.transform.childCount; i++)
            {
                Pos2D p = gimmicksObj.transform.GetChild(i).GetComponent<ObjectPosition>().grid;
                Transform img = gimmicks.transform.GetChild(i);
                if (map.Get(p.x, p.z) == 1)
                {
                    img.GetComponent<RectTransform>().anchoredPosition = new Vector2(pw * p.x, ph * p.z);
                    img.gameObject.SetActive(true);
                }
                else img.gameObject.SetActive(false);
            }
        }

        public void ShowTrapObj()
        {
            for (int i = traps.transform.childCount; i < trapsObj.transform.childCount; i++)
                Instantiate(trapIcon, traps.transform);
            for (int i = trapsObj.transform.childCount; i < traps.transform.childCount; i++)
                Destroy(traps.transform.GetChild(i).gameObject);
            for (int i = 0; i < trapsObj.transform.childCount; i++)
            {
                Pos2D p = trapsObj.transform.GetChild(i).GetComponent<ObjectPosition>().grid;
                Transform img = traps.transform.GetChild(i);

                if (trapsObj.transform.GetChild(i).GetChild(0).gameObject.activeSelf)
                {
                    img.GetComponent<RectTransform>().anchoredPosition = new Vector2(pw * p.x, ph * p.z);
                    img.gameObject.SetActive(true);
                }
                else img.gameObject.SetActive(false);
            }
        }

        public void MappingRoom(ObjectPosition room)
        {
            int sx = room.grid.x + room.range.left;
            int sy = room.grid.z + room.range.top;
            int ex = sx + room.range.width - 1;
            int ey = sy + room.range.height - 1;

            for (int ax = sx - 1; ax <= ex + 1; ax++)
            {
                for (int ay = sy - 1; ay <= ey + 1; ay++)
                {
                    Mapping(ax, ay, field.IsCollidediagonal(ax, ay) ? 0 : 1);
                }
            }

        }

        public void MappingRoads(int x, int y)
        {
            for (int ax = x - 1; ax <= x + 1; ax++)
            {
                for (int ay = y - 1; ay <= y + 1; ay++)
                {
                    Mapping(ax, ay, field.IsCollidediagonal(ax, ay) ? 0 : 1);
                }
            }
        }

        public void Mapping(int x, int y, int value)
        {
            if (map.Get(x, y) > 0) return;
            map.Set(x, y, value);
            if (value == 1)
            {
                if (field.GetInRoom(x, y) == null)
                {
                    GameObject road = Instantiate(roadImage, roads.transform);
                    road.GetComponent<RectTransform>().anchoredPosition = new Vector2(pw * x, ph * y);
                }
                else
                {
                    GameObject road = Instantiate(roomImage, roads.transform);
                    road.GetComponent<RectTransform>().anchoredPosition = new Vector2(pw * x, ph * y);
                }
            }
        }

        public void ResetMinimap(int width, int height)
        {
            playerIcon.SetActive(false);

            for (int i = 0; i < roads.transform.childCount; i++)
                Destroy(roads.transform.GetChild(i).gameObject);
            for (int i = 0; i < enemies.transform.childCount; i++)
                Destroy(enemies.transform.GetChild(i).gameObject);
            for (int i = 0; i < items.transform.childCount; i++)
                Destroy(items.transform.GetChild(i).gameObject);
            for (int i = 0; i < gimmicks.transform.childCount; i++)
                Destroy(gimmicks.transform.GetChild(i).gameObject);
            for (int i = 0; i < traps.transform.childCount; i++)
                Destroy(traps.transform.GetChild(i).gameObject);


            map = new Array2D(width, height);
            GetComponent<RectTransform>().sizeDelta = new Vector2(width * pw, height * ph);
        }
    }
}