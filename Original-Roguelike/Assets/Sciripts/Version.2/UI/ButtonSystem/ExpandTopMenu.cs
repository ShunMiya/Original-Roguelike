using UnityEngine;
using UnityEngine.EventSystems;

namespace UISystemV2
{
    public class ExpandTopMenu : MonoBehaviour
    {
        [SerializeField] private GameObject TopMenu;
        [SerializeField] private GameObject ButtonList;
        [SerializeField] private GameObject returnButton;

        public void OpenMenu()
        {
            TopMenu.SetActive(true);
            gameObject.GetComponent<CanvasGroup>().interactable = false;
            EventSystem.current.SetSelectedGameObject(ButtonList.transform.GetChild(0).gameObject);
        }

        public void WindowOn()
        {
            TopMenu.SetActive(true);
        }

        public void EventSystemSet()
        {
            gameObject.GetComponent<CanvasGroup>().interactable = false;
            EventSystem.current.SetSelectedGameObject(ButtonList.transform.GetChild(0).gameObject);

        }

        public void CloseMenu()
        {
            gameObject.GetComponent<CanvasGroup>().interactable = true;
            EventSystem.current.SetSelectedGameObject(returnButton);
            TopMenu.SetActive(false);
        }

        public void DisableWindow()
        {
            EventSystem.current.SetSelectedGameObject(null);
            gameObject.SetActive(false);
            Input.ResetInputAxes();
        }
    }
}