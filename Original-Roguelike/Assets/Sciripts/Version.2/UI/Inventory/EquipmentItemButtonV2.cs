using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using System;
using ItemSystemV2;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using UnityEditor;
using Performances;

namespace UISystemV2
{
    public class EquipmentItemButtonV2 : MonoBehaviour
    {
        public TextMeshProUGUI informationText;
        private TextMeshProUGUI buttonText;
        public DataRow row;
        public GameObject returnButton;
        public SubMenu subMenu;
        private Transform menuArea;
        [SerializeField] private Transform itemArea;
        public Transform EquipArea;
        private MenuSoundEffect menuSE;

        private void Awake()
        {
            buttonText = GetComponentInChildren<TextMeshProUGUI>();
        }
        void Start()
        {
            menuArea = transform.parent.parent.Find("MenuArea");
            menuSE = FindObjectOfType<MenuSoundEffect>();
        }

        public void OnSelected()
        {
            menuSE.MenuOperationSE(0);

            if (EventSystem.current.currentSelectedGameObject != gameObject)
            {
                EventSystem.current.SetSelectedGameObject(gameObject);
            }
            if (row == null)
            {
                informationText.text = "";
                return;
            }
            int itemId = Convert.ToInt32(row["Id"]);
            EquipmentDataV2 equipmentItem = ItemDataCacheV2.GetEquipment(itemId);

            string textFromDatabase = Regex.Unescape(equipmentItem.Description);

            informationText.text = textFromDatabase;
        }
        public void OnDeselected()
        {
            informationText.text = "";
        }

        public void SetEquip(DataRow Row)
        {
            if (Row == null)
            {
                row = null;
                buttonText.text = "";
                return;
            }
            row = Row;

            int itemId = Convert.ToInt32(row["Id"]);
            EquipmentDataV2 equipmentItem = ItemDataCacheV2.GetEquipment(itemId);
            buttonText.text = equipmentItem.ItemName;
        }

        public void Use()
        {
            if (row == null) return;
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
            EquipArea.GetComponent<CanvasGroup>().interactable = false;
            subMenu.EquipArea = EquipArea;

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