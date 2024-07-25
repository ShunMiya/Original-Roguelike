using UnityEngine;
using UnityEngine.EventSystems;
using Performances;

namespace UISystemV2
{
    public class HomeMenuButton : MonoBehaviour
    {
        [SerializeField] private GameObject DungeonSelectMenu;
        [SerializeField] private GameObject ButtonList;
        private MenuSoundEffect menuSE;
        private bool FirstSelect;

        // Start is called before the first frame update
        void Start()
        {
            menuSE = FindObjectOfType<MenuSoundEffect>();
            FirstSelect = true;
        }

        public void OnSelected()
        {
            if (EventSystem.current.currentSelectedGameObject != gameObject)
            {
                EventSystem.current.SetSelectedGameObject(gameObject);
            }
            if(FirstSelect)
            {
                FirstSelect = false;
                return;
            }
            menuSE.MenuOperationSE(0);
        }

        public void WindowOnOff()
        {
            menuSE.MenuOperationSE(1);

            DungeonSelectMenu.SetActive(true);
        }

        public void EventSystemSet()
        {
            gameObject.transform.parent.GetComponent<CanvasGroup>().interactable = false;
            EventSystem.current.SetSelectedGameObject(ButtonList.transform.GetChild(0).gameObject);
        }

        public void BackHomeMenu()
        {
            gameObject.transform.parent.GetComponent<CanvasGroup>().interactable = true;
            DungeonSelectMenu.SetActive(false);
        }
    }
}