using UnityEngine;
using UnityEngine.UI;

namespace UISystemV2
{
    public class ScrollViewController : MonoBehaviour
    {
        public ScrollRect scrollRect;
        public GameObject content;
        private float movey;

        private void Start()
        {
            float celly = content.GetComponent<GridLayoutGroup>().cellSize.y;
            float spacey = content.GetComponent<GridLayoutGroup>().spacing.y;
            movey = celly + spacey;
        }

        public void ScrollToItem(RectTransform item)
        {
            if (item != null)
            {
                Vector3 itemPosition = scrollRect.transform.InverseTransformPoint(item.position);

                if (itemPosition.y > scrollRect.GetComponent<RectTransform>().rect.yMax)
                {
                    Vector2 contentPosition = content.GetComponent<RectTransform>().anchoredPosition;
                    contentPosition.y -= movey;
                    content.GetComponent<RectTransform>().anchoredPosition = contentPosition;
                }
                else if (itemPosition.y < scrollRect.GetComponent<RectTransform>().rect.yMin)
                {
                    Vector2 contentPosition = content.GetComponent<RectTransform>().anchoredPosition;
                    contentPosition.y += movey;
                    content.GetComponent<RectTransform>().anchoredPosition = contentPosition;
                }
            }
        }
    }
}