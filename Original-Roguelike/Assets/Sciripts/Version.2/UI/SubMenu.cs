using ItemSystemV2;
using System;
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
        public Transform MenuArea;
        public Transform ButtonArea;
        public Transform EquipArea;
        [SerializeField]private GameObject backgroundObject;


        private void Start()
        {
            playerUseItemV2 = FindObjectOfType<PlayerUseItemV2>();
        }

        public void OnSelected()
        {
            int itemId = Convert.ToInt32(row["Id"]);
            IItemDataV2 itemData = ItemDataCacheV2.GetIItemData(itemId);
            switch (itemData.ItemType)
            {
                case 0:
                    UseButton.GetComponentInChildren<TextMeshProUGUI>().text = ("Use");
                    break;
                case 1:
                    if (Convert.ToInt32(row["Equipped"]) == 1)
                    {
                        UseButton.GetComponentInChildren<TextMeshProUGUI>().text = ("Unequip");
                        break;
                    }
                    UseButton.GetComponentInChildren<TextMeshProUGUI>().text = ("Equip");
                    break;
            }
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
            MenuArea.GetComponent<CanvasGroup>().interactable = true;
            if(EquipArea != null)
            {
                EquipArea.GetComponent<CanvasGroup>().interactable = true;
                EquipArea = null;
            }
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
            MenuArea.GetComponent<CanvasGroup>().interactable = true;
            if (EquipArea != null)
            {
                EquipArea.GetComponent<CanvasGroup>().interactable = true;
                EquipArea = null;
            }
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