using ItemSystemV2;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System;
using UnityEngine.UI;

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
        public Transform EquipArea;


        void Start()
        {
            menuArea = transform.parent.parent.Find("MenuArea");
            itemArea = transform.parent.parent.Find("ItemArea");
        }

        public void OnSelected()
        {
            if (EventSystem.current.currentSelectedGameObject != gameObject)
            {
                EventSystem.current.SetSelectedGameObject(gameObject);
            }
            int itemId = Convert.ToInt32(row["Id"]);
            IItemDataV2 itemData = ItemDataCacheV2.GetIItemData(itemId);
            informationText.text = itemData.Description;
        }
        public void OnDeselected()
        {
            informationText.text = "";
        }

        public void Use()
        {
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
            EventSystem.current.SetSelectedGameObject(returnButton);
        }
    }
}