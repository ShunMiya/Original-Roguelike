using UnityEngine;

namespace Minimap
{
    public class AutoMapping : MonoBehaviour
    {
        public GameObject roads;
        public GameObject enemies;
        public GameObject items;
        public GameObject gimmicks;
        public GameObject roadImage;

        private float pw, ph;
        private Array2D map;

        private void Awake()
        {
            RectTransform rect = roadImage.GetComponent<RectTransform>();
            pw = rect.sizeDelta.x;
            ph = rect.sizeDelta.y;
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