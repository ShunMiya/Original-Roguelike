using ItemSystemSQL;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System;

namespace UISystem
{
    public class ItemButtonSQL : MonoBehaviour
    {
        public TextMeshProUGUI informationText;
        public DataRow row;
        public PlayerUseItemSQL playerUseItemSQL;
        private CreateItemButtonSQL createItemButton;
        private EquipmentItemSQL equipmentItemSQL;


        void Start()
        {
            createItemButton = GetComponentInParent<CreateItemButtonSQL>();
            equipmentItemSQL = transform.parent.parent.GetComponentInChildren<EquipmentItemSQL>();
            playerUseItemSQL = FindObjectOfType<PlayerUseItemSQL>();
        }

        public void OnSelected()
        {
            if (EventSystem.current.currentSelectedGameObject != gameObject)
            {
                EventSystem.current.SetSelectedGameObject(gameObject);
            }
            int itemId = Convert.ToInt32(row["Id"]);
            IItemData itemData = ItemDataCache.GetIItemData(itemId);
            informationText.text = itemData.Description;
        }
        public void OnDeselected()
        {
            informationText.text = "";
        }

        public void UseItem()
        {
            int itemId = Convert.ToInt32(row["Id"]);
            int itemStock = Convert.ToInt32(row["Num"]);
            IItemData itemData = ItemDataCache.GetIItemData(itemId);

            switch (itemData.ItemType)
            {
                case 0:
                    Debug.Log("ID" + itemData.Id + "‚ÅStack" +itemStock+ "‚ð‘I‘ð");
                    int remainingStock = playerUseItemSQL.UseItem(row,0);
                    row["Num"] = remainingStock;

                    if (remainingStock == 0) createItemButton.SelectButtonChangeForDestruction(this);

                    createItemButton.SetButtonTextAfterUse(this);

                    break;
                case 1:
                    playerUseItemSQL.UseItem(row,1);

                    createItemButton.SelectButtonChangeForDestruction(this);

                    break;
            }
        }

        public void EquipItem()
        {
            equipmentItemSQL.EquipItem(row);
        }
    }
}