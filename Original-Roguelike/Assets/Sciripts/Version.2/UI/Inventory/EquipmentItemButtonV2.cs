using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using System;
using ItemSystemV2;

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
        private Transform itemArea;
        public Transform EquipArea;

        private void Awake()
        {
            buttonText = GetComponentInChildren<TextMeshProUGUI>();
        }
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
            if (row == null)
            {
                informationText.text = "";
                return;
            }
            int itemId = Convert.ToInt32(row["Id"]);
            EquipmentDataV2 equipmentItem = ItemDataCacheV2.GetEquipment(itemId);

            informationText.text = equipmentItem.ItemName;
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
            subMenu.gameObject.SetActive(true);
            subMenu.row = row;
            subMenu.informationText = informationText;
            subMenu.ItemButton = gameObject;
            subMenu.ButtonArea = itemArea;
            subMenu.MenuArea = menuArea;
            //　ItemAreaを無効化
            itemArea.GetComponent<CanvasGroup>().interactable = false;
            //　MenuAreaを無効化
            menuArea.GetComponent<CanvasGroup>().interactable = false;
            //　EquipAreaを無効化
            EquipArea.GetComponent<CanvasGroup>().interactable = false;
            subMenu.EquipArea = EquipArea;

            EventSystem.current.SetSelectedGameObject(subMenu.UseButton);
        }

        public void SelectReturnButton()
        {
            EventSystem.current.SetSelectedGameObject(returnButton);
        }
    }
}