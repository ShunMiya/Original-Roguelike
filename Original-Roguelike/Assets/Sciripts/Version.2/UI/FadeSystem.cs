using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;
using Field;
using AttackSystem;
using GameEndSystemV2;

namespace Fade
{
    public class FadeSystem : MonoBehaviour
    {
        public Image FadeImage;
        public float fadeOutDuration;
        public float fadeInDuration;
        public float waitTime;

        [SerializeField]private GameEndV2 gameEndV2;
        [SerializeField] private LoadFieldMap loadmap;
        [SerializeField]private AttackObjects attackObjects;
        [SerializeField] private GameObject StairsMenu;

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

        public IEnumerator NextStageFade()
        {
            yield return StartCoroutine(FadeOut());

            attackObjects.DeleteList();
            StairsMenu.SetActive(false);
            loadmap.Load();

            StartCoroutine(FadeIn());
        }

        public IEnumerator GameClearFade()
        {
            yield return StartCoroutine (FadeOut());
            gameEndV2.GameClearPerformance();
            StartCoroutine(FadeIn());

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
                if (t <= 0.5f) // 0.5 ‚æ‚è‘O‚Ìê‡
                {
                    FadeImage.color = Color.Lerp(startColor, middleColor, t / 0.5f); // 0`0.5 ‚Ì”ÍˆÍ‚Å Lerp
                }
                else // 0.5 ‚æ‚èŒã‚Ìê‡
                {
                    FadeImage.color = Color.Lerp(middleColor, endColor, (t - 0.5f) * 2); // 0.5`1 ‚Ì”ÍˆÍ‚Å Lerp
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

                if (t <= 0.5f) // 0.5 ‚æ‚è‘O‚Ìê‡
                {
                    FadeImage.color = Color.Lerp(startColor, middleColor, t / 0.5f); // 0`0.5 ‚Ì”ÍˆÍ‚Å Lerp
                }
                else // 0.5 ‚æ‚èŒã‚Ìê‡
                {
                    FadeImage.color = Color.Lerp(middleColor, endColor, (t - 0.5f) / 0.5f); // 0.5`1 ‚Ì”ÍˆÍ‚Å Lerp
                }

                yield return null;
            }
            Time.timeScale = 1f;
        }
    }
}