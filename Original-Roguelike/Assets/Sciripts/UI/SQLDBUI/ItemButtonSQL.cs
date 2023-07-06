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
        private EquipmentItem equipmentItem;


        void Start()
        {
            createItemButton = GetComponentInParent<CreateItemButtonSQL>();
            equipmentItem = transform.parent.parent.GetComponentInChildren<EquipmentItem>();
        }

        public void OnSelected()
        {
            if (EventSystem.current.currentSelectedGameObject != gameObject)
            {
                EventSystem.current.SetSelectedGameObject(gameObject);
            }
            int itemId = Convert.ToInt32(row["Id"]);
            IItemDataInventory itemData = ItemDataCache.GetIItemData(itemId);
            string description = itemData.Description;
            informationText.text = description;
        }
        public void OnDeselected()
        {
            informationText.text = "";
        }

        public void UseItem()
        {
            int itemId = Convert.ToInt32(row["Id"]);
            int itemStock = Convert.ToInt32(row["Num"]);
            IItemDataInventory itemData = ItemDataCache.GetIItemData(itemId);

            switch (itemData.ItemType)
            {
                case 0:
                    Debug.Log("ID" + itemData.Id + "‚ÅStack" +itemStock+ "‚ð‘I‘ð");
                    int remainingStock = playerUseItemSQL.UseItem(row,0);
                    Debug.Log("ID" + itemData.Id + "‚ÅStack" + remainingStock + "‚É•Ï‰»");
                    row["Num"] = remainingStock;

                    createItemButton.SetButtonTextAfterUse(this);

                    break;
                default:
                    playerUseItemSQL.UseItem(row,1);

                    createItemButton.SelectButtonChangeForDestruction(this);

                    break;
            }
        }

        public void EquipItem()
        {
//            equipmentItem.EquipItem(itemData);
        }
    }
}