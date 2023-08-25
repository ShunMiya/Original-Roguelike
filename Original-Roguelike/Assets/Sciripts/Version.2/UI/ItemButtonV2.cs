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


        void Start()
        {
            createItemButton = GetComponentInParent<CreateItemButtonV2>();
            equipmentItemSQL = transform.parent.parent.GetComponentInChildren<EquipmentItemV2>();
            playerUseItemSQL = FindObjectOfType<PlayerUseItemV2>();
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
            EventSystem.current.SetSelectedGameObject(subMenu.UseButton);
        }
    }
}