using ItemSystemV2;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UISystemV2
{
    public class SubMenu : MonoBehaviour
    {
        public DataRow row;
        private GameObject returnButton;
        public GameObject UseButton;
        public TextMeshProUGUI informationText;
        private PlayerUseItemV2 playerUseItemV2;

        private void Start()
        {
            playerUseItemV2 = FindObjectOfType<PlayerUseItemV2>();
        }

        public void OnSelected()
        {
            int itemId = Convert.ToInt32(row["Id"]);
            IItemDataV2 itemData = ItemDataCacheV2.GetIItemData(itemId);
            informationText.text = itemData.Description;
        }

        public void UseItem()
        {
            int itemId = Convert.ToInt32(row["Id"]);
            IItemDataV2 itemData = ItemDataCacheV2.GetIItemData(itemId);

            switch (itemData.ItemType)
            {
                case 0:
                    playerUseItemV2.SetData(row, 0);

                    break;
                case 1:
                    playerUseItemV2.SetData(row, 1);

                    break;
            }
            
        }

        public void ThrowItem()
        {
            int itemId = Convert.ToInt32(row["Id"]);
            IItemDataV2 itemData = ItemDataCacheV2.GetIItemData(itemId);

        }

        public void PutItem()
        {
            int itemId = Convert.ToInt32(row["Id"]);
            IItemDataV2 itemData = ItemDataCacheV2.GetIItemData(itemId);

        }

        public void SelectReturnButton()
        {
            EventSystem.current.SetSelectedGameObject(returnButton);
        }
    }
}