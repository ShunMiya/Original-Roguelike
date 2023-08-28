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
        public GameObject UseButton;
        public GameObject ItemButton;
        public TextMeshProUGUI informationText;
        private PlayerUseItemV2 playerUseItemV2;
        public Transform menuArea;
        public Transform ButtonArea;
        [SerializeField]private GameObject backgroundObject;


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
            ButtonArea.GetComponent<CanvasGroup>().interactable = true;
            menuArea.GetComponent<CanvasGroup>().interactable = true;
            gameObject.SetActive(false);
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

        public void CancelUse()
        {
            ButtonArea.GetComponent<CanvasGroup>().interactable = true;
            menuArea.GetComponent<CanvasGroup>().interactable = true;
            EventSystem.current.SetSelectedGameObject(ItemButton);
            gameObject.SetActive(false);
        }

        public void DisableWindow()
        {
            backgroundObject.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}