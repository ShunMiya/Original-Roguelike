using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using ItemSystemV2;

namespace UISystemV2
{
    public class EquipmentItemButtonV2 : MonoBehaviour
    {
        public TextMeshProUGUI informationText;
        private TextMeshProUGUI buttonText;
        public DataRow row;
        private

        void Awake()
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
            row = null;
            informationText.text = "";
            if (buttonText != null) buttonText.text = "";

        }
    }
}