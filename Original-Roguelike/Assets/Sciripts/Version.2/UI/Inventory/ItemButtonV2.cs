using ItemSystemV2;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using Performances;
using UnityEditor;

namespace UISystemV2
{
    public class ItemButtonV2 : MonoBehaviour
    {
        public TextMeshProUGUI informationText;
        public GameObject returnButton;
        public DataRow row;
        public SubMenu subMenu;
        private Transform menuArea;
        private Transform itemArea;
        private Transform scrollView;
        public Transform EquipArea;
        private MenuSoundEffect menuSE;

        void Start()
        {
            menuArea = transform.parent.parent.parent.parent.Find("MenuArea");
            itemArea = transform.parent.parent.Find("Content");
            scrollView = transform.parent.parent.parent.parent.Find("ScrollView");
            menuSE = FindObjectOfType<MenuSoundEffect>();
        }

        public void OnSelected()
        {
            if (EventSystem.current.currentSelectedGameObject != gameObject)
            {
                EventSystem.current.SetSelectedGameObject(gameObject);
            }
            int itemId = Convert.ToInt32(row["Id"]);
            IItemDataV2 itemData = ItemDataCacheV2.GetIItemData(itemId);

            string textFromDatabase = Regex.Unescape(itemData.Description);

            informationText.text = textFromDatabase;
            RectTransform buttonRect = GetComponent<RectTransform>();
            scrollView.GetComponent<ScrollViewController>().ScrollToItem(buttonRect);

            menuSE.MenuOperationSE(0);
        }
        public void OnDeselected()
        {
            informationText.text = "";
        }

        public void Use()
        {
            menuSE.MenuOperationSE(1);

            subMenu.gameObject.SetActive(true);
            subMenu.row = row;
            subMenu.informationText = informationText;
            subMenu.ItemButton = gameObject;
            subMenu.ButtonArea = itemArea;
            subMenu.MenuArea = menuArea;
            //Å@ItemAreaÇñ≥å¯âª
            itemArea.GetComponent<CanvasGroup>().interactable = false;
            //Å@MenuAreaÇñ≥å¯âª
            menuArea.GetComponent<CanvasGroup>().interactable = false;
            //Å@EquipAreaÇñ≥å¯âª
            if (EquipArea != null)
            {
                EquipArea.GetComponent<CanvasGroup>().interactable = false;
                subMenu.EquipArea = EquipArea;
            }

            Button button = gameObject.GetComponent<Button>();
            if (button != null)
            {
                ColorBlock colors = button.colors;
                colors.disabledColor = Color.green;
                button.colors = colors;
            }

            EventSystem.current.SetSelectedGameObject(subMenu.UseButton);
        }

        public void SelectReturnButton()
        {
            menuSE.MenuOperationSE(2);

            EventSystem.current.SetSelectedGameObject(returnButton);
        }
    }
}