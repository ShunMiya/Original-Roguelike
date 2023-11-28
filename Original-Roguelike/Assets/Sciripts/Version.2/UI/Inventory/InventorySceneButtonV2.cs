using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using Performances;

namespace UISystemV2
{
    public class InventorySceneButtonV2 : MonoBehaviour
    {
        [SerializeField] private string informationString;
        [SerializeField] private TextMeshProUGUI informationText;
        private GameObject returnButton;
        [SerializeField] private GameObject InventoryUI;
        private GameObject backgroundObject;
        [SerializeField] private MenuSoundEffect menuSE;

        void Awake()
        {
            returnButton = transform.parent.Find("BackGameButton").gameObject;
            backgroundObject = InventoryUI.transform.Find("BackGround").gameObject;
            menuSE = FindObjectOfType<MenuSoundEffect>();
        }

        public void OnSelected()
        {
            if (EventSystem.current.currentSelectedGameObject != gameObject)
            {
                EventSystem.current.SetSelectedGameObject(gameObject);
            }
            informationText.text = informationString;

            menuSE.MenuOperationSE(0);
        }

        public void OnDeselected()
        {
            informationText.text = "";
        }

        public void DisableWindow()
        {
            menuSE.MenuOperationSE(2);

            Time.timeScale = 1f;
            Input.ResetInputAxes();
            backgroundObject.SetActive(false);
        }

        public void WindowOnOffSQL(GameObject window)
        {
            menuSE.MenuOperationSE(1);

            InventoryUI.GetComponent<PauseSystemV2>().ChangeWindow(window);
        }

        public void SelectReturnButton()
        {
            menuSE.MenuOperationSE(2);

            EventSystem.current.SetSelectedGameObject(returnButton);
        }
    }
}
