using UnityEngine;
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
        private GameObject SystemText;

        void Start()
        {
            returnButton = transform.parent.Find("BackGameButton").gameObject;
            backgroundObject = InventoryUI.transform.Find("BackGround").gameObject;
            SystemText = InventoryUI.transform.Find("SystemText").gameObject;
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
            SystemText.SetActive(true);
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
