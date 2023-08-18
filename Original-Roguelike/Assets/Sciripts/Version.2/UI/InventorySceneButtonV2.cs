using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

namespace UISystemV2
{
    public class InventorySceneButtonV2 : MonoBehaviour
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
        }
        public void WindowOnOffSQL(GameObject window)
        {
            InventoryUI.GetComponent<PauseSystemV2>().ChangeWindow(window);
        }

        public void SelectReturnButton()
        {
            EventSystem.current.SetSelectedGameObject(returnButton);
        }
    }
}
