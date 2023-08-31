using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

namespace Fade
{
    public class FadeSystem : MonoBehaviour
    {
        public Image FadeImage;
        public float fadeOutDuration;
        public float fadeInDuration;
        public float waitTime;

        private void Awake()
        {
            FadeImage.gameObject.SetActive(true);
            StartCoroutine(FadeIn());
        }

        public void SceneJump(string SceneName)
        {
            StartCoroutine(TransitionSeq(SceneName));
        }

        public void GameCloseButtonClick()
        {
            StartCoroutine(GameClose());
        }

        private IEnumerator TransitionSeq(string SceneName)
        {
            yield return StartCoroutine(FadeOut());
            SceneManager.LoadScene(SceneName);
        }

        private IEnumerator GameClose()
        {
            yield return StartCoroutine(FadeOut());

            EditorApplication.isPlaying = false; // UnityEditorClose NotAplicationClose
            Application.Quit();
        }

        private IEnumerator FadeIn()
        {
            float st = 0f;
            Color startColor = Color.black;
            Color middleColor = new Color(0f, 0f, 0f, 0.7f);
            Color endColor = Color.clear;

            while (st < 1f)
            {
                st += Time.unscaledDeltaTime / waitTime;
                yield return null;
            }

            float t = 0f;
            while (t < 1f)
            {
                t += Time.unscaledDeltaTime / fadeInDuration;
                if (t <= 0.5f) // 0.5 ���O�̏ꍇ
                {
                    FadeImage.color = Color.Lerp(startColor, middleColor, t / 0.5f); // 0�`0.5 �͈̔͂� Lerp
                }
                else // 0.5 ����̏ꍇ
                {
                    FadeImage.color = Color.Lerp(middleColor, endColor, (t - 0.5f) * 2); // 0.5�`1 �͈̔͂� Lerp
                }

                yield return null;
            }
            FadeImage.gameObject.SetActive(false);
        }

        private IEnumerator FadeOut()
        {
            FadeImage.gameObject.SetActive(true);

            float t = 0f;
            Color startColor = Color.clear;
            Color middleColor = new Color(0f, 0f, 0f, 0.3f);
            Color endColor = Color.black;

            while (t < 1f)
            {
                t += Time.unscaledDeltaTime / fadeOutDuration;

                if (t <= 0.5f) // 0.5 ���O�̏ꍇ
                {
                    FadeImage.color = Color.Lerp(startColor, middleColor, t / 0.5f); // 0�`0.5 �͈̔͂� Lerp
                }
                else // 0.5 ����̏ꍇ
                {
                    FadeImage.color = Color.Lerp(middleColor, endColor, (t - 0.5f) / 0.5f); // 0.5�`1 �͈̔͂� Lerp
                }

                yield return null;
            }
        }
    }
}