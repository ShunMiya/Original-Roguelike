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

        public void ChangeSceneButtonClick(string SName)
        {
            ButtonTargetReset();
            fadeSystem.SceneJump(SName);
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
            ChangeSceneButtonClick("Dungeon");
        }

        public void NextStageButtonClick()
        {
            ButtonTargetReset();

            gameEnd.NextStagePerformance();
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