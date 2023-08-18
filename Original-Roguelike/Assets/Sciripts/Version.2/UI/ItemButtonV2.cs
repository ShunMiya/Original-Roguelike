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

        public void UseItem()
        {
            int itemId = Convert.ToInt32(row["Id"]);
            int itemStock = Convert.ToInt32(row["Num"]);
            IItemDataV2 itemData = ItemDataCacheV2.GetIItemData(itemId);

            switch (itemData.ItemType)
            {
                case 0:
                    Debug.Log("ID" + itemData.Id + "‚ÅStack" + itemStock + "‚ð‘I‘ð");
                    int remainingStock = playerUseItemSQL.UseItem(row, 0);
                    row["Num"] = remainingStock;

                    if (remainingStock == 0) createItemButton.SelectButtonChangeForDestruction(this);

                    createItemButton.SetButtonTextAfterUse(this);

                    break;
                case 1:
                    playerUseItemSQL.UseItem(row, 1);

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