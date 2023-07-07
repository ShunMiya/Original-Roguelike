using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

namespace UISystem
{
    public class InventorySceneButton : MonoBehaviour
    {
        [SerializeField] private string informationString;
        [SerializeField] private TextMeshProUGUI informationText;
        private GameObject returnButton;
        [SerializeField] private GameObject InventoryUI;
        private GameObject backgroundObject;


        void Start()
        {
            returnButton = transform.parent.Find("BackGameButton").gameObject;
            backgroundObject = InventoryUI.transform.Find("BackGround").gameObject;
        }

        public void OnSelected()
        {
            if (EventSystem.current.currentSelectedGameObject != gameObject)
            {
                EventSystem.current.SetSelectedGameObject(gameObject);
            }
            informationText.text = informationString;
        }
        public void OnDeselected()
        {
            informationText.text = "";
        }

        public void DisableWindow()
        {
            backgroundObject.SetActive(false);
//            Transform backgroundTransform = transform.parent.parent.parent.Find("BackGround");
//            GameObject backgroundObject = backgroundTransform.gameObject;
//            backgroundObject.SetActive(false);
        }

        public void WindowOnOff(GameObject window)
        {
            InventoryUI.GetComponent<PauseSystem>().ChangeWindow(window);
        }

        public void WindowOnOffSQL(GameObject window)
        {
            InventoryUI.GetComponent<PauseSystemSQL>().ChangeWindow(window);
        }

        public void SelectReturnButton()
        {
            EventSystem.current.SetSelectedGameObject(returnButton);
        }
    }
}
