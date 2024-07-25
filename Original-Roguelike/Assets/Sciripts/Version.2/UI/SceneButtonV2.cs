using ItemSystemV2.Inventory;
using UnityEngine;
using Fade;
using UnityEngine.EventSystems;
using GameEndSystemV2;

namespace UISystemV2
{
    public class SceneButtonV2 : MonoBehaviour
    {
        private FadeSystem fadeSystem;
        private GameEndV2 gameEnd;
        [SerializeField] private string SceneName;

        private void Start()
        {
            fadeSystem = FindObjectOfType<FadeSystem>();
            gameEnd = FindObjectOfType<GameEndV2>();
        }

        public void OnSelected()
        {
            if (EventSystem.current.currentSelectedGameObject != gameObject)
            {
                EventSystem.current.SetSelectedGameObject(gameObject);
            }
        }

        public void ChangeSceneButtonClick()
        {
            ButtonTargetReset();
            fadeSystem.SceneJump(SceneName);
        }

        public void GameEndButtonClick()
        {
            ButtonTargetReset();
            fadeSystem.GameCloseButtonClick();
        }

        public void RetryButtonClick()
        {
            ButtonTargetReset();
            StatusReset();
            ChangeSceneButtonClick();

            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void NextStageButtonClick()
        {
            ButtonTargetReset();

            gameEnd.NextStagePerformance();
        }

        public void DisableWindow()
        {
            ButtonTargetReset();
            gameObject.SetActive(false);
            Input.ResetInputAxes();
        }

        public void ButtonTargetReset()
        {
            EventSystem.current.SetSelectedGameObject(null);
        }

        public void StatusReset()
        {
            SQLDBInitializationV2.PlayerStatusInitialization();
        }

        public void InventoryReset()
        {
            SQLDBInitializationV2.PlayerInventoryInitialization();
        }
    }
}