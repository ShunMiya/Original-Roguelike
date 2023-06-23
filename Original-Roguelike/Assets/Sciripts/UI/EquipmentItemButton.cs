using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItemSystem;
using TMPro;
using UnityEngine.EventSystems;

namespace UISystem
{
    public class EquipmentItemButton : MonoBehaviour
    {
        public PlayerInventoryDataBase playerInventory;
        public TextMeshProUGUI informationText;
        public ItemData itemData;
        private TextMeshProUGUI buttonText;


        // Start is called before the first frame update
        void Start()
        {
            buttonText = GetComponentInChildren<TextMeshProUGUI>();

        }

        public void OnSelected()
        {
            if (EventSystem.current.currentSelectedGameObject != gameObject)
            {
                EventSystem.current.SetSelectedGameObject(gameObject);
            }
            if (itemData == null)
            {
                informationText.text = "";
                return;
            }
            informationText.text = "UnEquip " + itemData.ItemName;
        }
        public void OnDeselected()
        {
            informationText.text = "";
        }

        public void SetEquip(ItemData iitemData)
        {
            if (iitemData == null)
            {
                unequip();
                return;
            }
            itemData = iitemData;
            string itemName = itemData.ItemName;
            buttonText.text = itemName;
        }

        public void unequip()
        {
            if(itemData != null)GetComponentInParent<EquipmentItem>().UnequipItem(itemData);
            itemData = null;
            informationText.text = "";
            buttonText.text = "";

        }
    }
}