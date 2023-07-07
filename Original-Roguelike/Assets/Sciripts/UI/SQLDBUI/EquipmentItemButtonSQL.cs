using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItemSystem;
using TMPro;
using UnityEngine.EventSystems;

namespace UISystem
{
    public class EquipmentItemButtonSQL : MonoBehaviour
    {
        public TextMeshProUGUI informationText;
        private TextMeshProUGUI buttonText;
        public DataRow row;


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
            if (row == null)
            {
                informationText.text = "";
                return;
            }
            informationText.text = "UnEquip " + row["ItemName"].ToString();
        }
        public void OnDeselected()
        {
            informationText.text = "";
        }

        public void SetEquip(DataRow Row)
        {
            if (Row == null)
            {
                unequip();
                return;
            }
            row = Row;
            string itemName = row["ItemName"].ToString();
            buttonText.text = itemName;
        }

        public void unequip()
        {
            if (row != null) GetComponentInParent<EquipmentItemSQL>().UnequipItem(row);
            row = null;
            informationText.text = "";
            if (buttonText != null) buttonText.text = "";

        }
    }
}