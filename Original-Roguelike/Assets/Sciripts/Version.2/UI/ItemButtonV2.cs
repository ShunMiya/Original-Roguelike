using ItemSystemV2;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System;

namespace UISystemV2
{
    public class ItemButtonV2 : MonoBehaviour
    {
        public TextMeshProUGUI informationText;
        public DataRow row;
        public PlayerUseItemV2 playerUseItemSQL;
        private CreateItemButtonV2 createItemButton;
        private EquipmentItemV2 equipmentItemSQL;
        public SubMenu subMenu;
        private Transform menuArea;
        private Transform itemArea;


        void Start()
        {
            createItemButton = GetComponentInParent<CreateItemButtonV2>();
            equipmentItemSQL = transform.parent.parent.GetComponentInChildren<EquipmentItemV2>();
            playerUseItemSQL = FindObjectOfType<PlayerUseItemV2>();
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

        public void EquipItem()
        {
            equipmentItemSQL.EquipItem(row);
        }

        public void Use()
        {
            subMenu.gameObject.SetActive(true);
            subMenu.row = row;
            subMenu.informationText = informationText;
            subMenu.ItemButton = gameObject;
            subMenu.ButtonArea = itemArea;
            subMenu.menuArea = menuArea;
            //Å@EquipAreaÇñ≥å¯âª
            itemArea.GetComponent<CanvasGroup>().interactable = false;
            //Å@MenuAreaÇñ≥å¯âª
            menuArea.GetComponent<CanvasGroup>().interactable = false;

            EventSystem.current.SetSelectedGameObject(subMenu.UseButton);
        }
    }
}