using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UISystemV2
{
    public class LevelUpTextAnimation : MonoBehaviour
    {
        public float moveDistance = 2f;
        public float moveDuration = 0.3f;
        public float waitDuration = 0.5f;

        private TextMeshProUGUI text;
        private RectTransform rectTransform;

        private void OnEnable()
        {
            rectTransform = GetComponent<RectTransform>();
            text = GetComponent<TextMeshProUGUI>();
            StartCoroutine(LevelUpAnimation());
        }

        private IEnumerator LevelUpAnimation()
        {
            float elapsedTime = 0f;

            while (elapsedTime < moveDuration)
            {
                float newY = Mathf.Lerp(0, moveDistance, elapsedTime / moveDuration);
                Vector2 newPosition = rectTransform.anchoredPosition + new Vector2(0, newY);
                rectTransform.anchoredPosition = newPosition;

                float alpha = Mathf.Lerp(0, 100, elapsedTime / moveDuration);
                text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            yield return new WaitForSeconds(waitDuration);

            elapsedTime = 0f;

            while (elapsedTime < moveDuration)
            {
                float newY = Mathf.Lerp(0, -moveDistance, elapsedTime / moveDuration);
                Vector2 newPosition = rectTransform.anchoredPosition + new Vector2(0, newY);
                rectTransform.anchoredPosition = newPosition;

                float alpha = Mathf.Lerp(100, 0, elapsedTime / moveDuration);
                text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);

                elapsedTime += Time.deltaTime;
                yield return null;
            }
            rectTransform.anchoredPosition = new Vector2(0, 0);
            gameObject.SetActive(false);
        }

    }
}