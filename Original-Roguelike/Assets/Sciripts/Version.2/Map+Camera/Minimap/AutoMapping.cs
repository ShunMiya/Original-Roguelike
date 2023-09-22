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

        public GameObject enemies;
        public GameObject enemiesObj;
        public GameObject enemyIcon;

        public GameObject items;
        public GameObject itemsObj;
        public GameObject itemIcon;

        public GameObject gimmicks;
        public GameObject gimmicksObj;
        public GameObject StairsIcon;
        public GameObject TrapIcon;

        private float pw, ph;
        private Array2D map;

        private void Awake()
        {
            RectTransform rect = roadImage.GetComponent<RectTransform>();
            pw = rect.sizeDelta.x;
            ph = rect.sizeDelta.y;
        }

        private void Update()
        {
            ShowObjectsAllTime(itemIcon, items, itemsObj);
            ShowGimmickObjects();
        }

        public void ShowObjectsAllTime(GameObject image, GameObject imgs, GameObject objs)
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

        public void ShowGimmickObjects()
        {
            for (int i = gimmicks.transform.childCount; i < gimmicksObj.transform.childCount; i++)
            {
                switch (gimmicksObj.transform.GetChild(i).GetComponent<SteppedOnEvent>().ObjType)
                {
                    case 1:
                        Instantiate(TrapIcon, gimmicks.transform);
                        break;
                    case 2:
                        Instantiate(StairsIcon, gimmicks.transform);
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

        public void Mapping(int x, int y, int value)
        {
            map.Set(x, y, value);
            if (value == 1)
            {
                GameObject road = Instantiate(roadImage, roads.transform);
                road.GetComponent<RectTransform>().anchoredPosition = new Vector2(pw * x, ph * y);
            }
        }

        public void ResetMinimap(int width, int height)
        {
            for (int i = 0; i < roads.transform.childCount; i++)
                Destroy(roads.transform.GetChild(i).gameObject);
            for (int i = 0; i < enemies.transform.childCount; i++)
                Destroy(enemies.transform.GetChild(i).gameObject);
            for (int i = 0; i < items.transform.childCount; i++)
                Destroy(items.transform.GetChild(i).gameObject);
            for (int i = 0; i < gimmicks.transform.childCount; i++)
                Destroy(gimmicks.transform.GetChild(i).gameObject);

            map = new Array2D(width, height);
            Debug.Log(width + "*" + pw + "," + height + "*" + ph);
            GetComponent<RectTransform>().sizeDelta = new Vector2(width * pw, height * ph);
        }
    }
}