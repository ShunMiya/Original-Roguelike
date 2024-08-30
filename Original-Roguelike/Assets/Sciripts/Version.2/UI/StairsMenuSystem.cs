using UnityEngine;
using UnityEngine.EventSystems;
using Performances;
using GameEndSystemV2;

namespace StairsMenu
{
    public class StairsMenuSystem : MonoBehaviour
    {
        [SerializeField] private GameObject DungeonInterruptionMenu;
        [SerializeField] private GameObject DungeonInterruptionButton;
        private MenuSoundEffect menuSE;
        private GameEndV2 gameEnd;

        // Start is called before the first frame update
        void Awake()
        {
            menuSE = FindObjectOfType<MenuSoundEffect>();
            gameEnd = FindObjectOfType<GameEndV2>();
        }

        public void OnSelected()
        {
            menuSE.MenuOperationSE(0);
        }

        public void NextStageButtonClick()
        {
            menuSE.MenuOperationSE(1);

            ButtonTargetReset();
            gameEnd.NextStagePerformance();
        }
        public void WindowOnOff()
        {
            menuSE.MenuOperationSE(1);

            DungeonInterruptionMenu.SetActive(true);
        }

        public void EventSystemSet()
        {
            gameObject.GetComponent<CanvasGroup>().interactable = false;
            EventSystem.current.SetSelectedGameObject(DungeonInterruptionButton);
        }

        public void BackHomeMenu()
        {
            gameObject.GetComponent<CanvasGroup>().interactable = true;
            DungeonInterruptionMenu.SetActive(false);
        }

        public void DisableWindow()
        {
            menuSE.MenuOperationSE(2);

            ButtonTargetReset();
            gameObject.SetActive(false);
            Input.ResetInputAxes();
        }

        public void ButtonTargetReset()
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
    }
}