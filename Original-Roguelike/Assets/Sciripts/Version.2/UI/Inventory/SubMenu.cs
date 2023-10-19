using ItemSystemV2;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UISystemV2
{
    public class SubMenu : MonoBehaviour
    {
        public DataRow row;
        public GameObject UseButton;
        public GameObject ItemButton;
        public TextMeshProUGUI informationText;
        private PlayerUseItemV2 playerUseItemV2;
        private PlayerPutItem playerPutItem;
        private PlayerThrowItem playerThrowItem;
        public Transform MenuArea;
        public Transform ButtonArea;
        public Transform EquipArea;
        [SerializeField]private GameObject backgroundObject;


        private void Start()
        {
            playerUseItemV2 = FindObjectOfType<PlayerUseItemV2>();
            playerPutItem = FindObjectOfType<PlayerPutItem>();
            playerThrowItem = FindObjectOfType<PlayerThrowItem>();
        }

        public void OnSelected()
        {
            int itemId = Convert.ToInt32(row["Id"]);
            IItemDataV2 itemData = ItemDataCacheV2.GetIItemData(itemId);
            switch (itemData.ItemType)
            {
                case 0:
                    UseButton.GetComponentInChildren<TextMeshProUGUI>().text = ("Žg‚¤");
                    break;
                case 1:
                    if (Convert.ToInt32(row["Equipped"]) == 1 || Convert.ToInt32(row["Equipped"]) == 2)
                    {
                        UseButton.GetComponentInChildren<TextMeshProUGUI>().text = ("ŠO‚·");
                        break;
                    }
                    UseButton.GetComponentInChildren<TextMeshProUGUI>().text = ("‘•”õ‚·‚é");
                    break;
                case 2:
                    UseButton.GetComponentInChildren<TextMeshProUGUI>().text = ("Žg‚¤");
                    break;
            }
            informationText.text = itemData.Description;
        }

        public void UseItem()
        {
            int itemId = Convert.ToInt32(row["Id"]);
            IItemDataV2 itemData = ItemDataCacheV2.GetIItemData(itemId);
            
            playerUseItemV2.SetData(row, itemData.ItemType);

            ButtonArea.GetComponent<CanvasGroup>().interactable = true;
            MenuArea.GetComponent<CanvasGroup>().interactable = true;
            if(EquipArea != null)
            {
                EquipArea.GetComponent<CanvasGroup>().interactable = true;
                EquipArea = null;
            }
            ChangeSelectColor();
            Input.ResetInputAxes();
            gameObject.SetActive(false);
        }

        public void ThrowItem()
        {
            playerThrowItem.SetData(row);

            ButtonArea.GetComponent<CanvasGroup>().interactable = true;
            MenuArea.GetComponent<CanvasGroup>().interactable = true;
            if (EquipArea != null)
            {
                EquipArea.GetComponent<CanvasGroup>().interactable = true;
                EquipArea = null;
            }
            ChangeSelectColor();
            Input.ResetInputAxes();
            gameObject.SetActive(false);
        }

        public void PutItem()
        {
            playerPutItem.SetData(row);

            ButtonArea.GetComponent<CanvasGroup>().interactable = true;
            MenuArea.GetComponent<CanvasGroup>().interactable = true;
            if (EquipArea != null)
            {
                EquipArea.GetComponent<CanvasGroup>().interactable = true;
                EquipArea = null;
            }

            ChangeSelectColor();
            Input.ResetInputAxes();
            gameObject.SetActive(false);
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

            ChangeSelectColor();

            EventSystem.current.SetSelectedGameObject(ItemButton);
            gameObject.SetActive(false);
        }

        public void ChangeSelectColor()
        {
            Button button = ItemButton.GetComponent<Button>();
            if (button != null)
            {
                ColorBlock colors = button.colors;
                colors.disabledColor = new Color(0, 0, 0, 128 / 255f);
                button.colors = colors;
            }
        }

        public void DisableWindow()
        {
            backgroundObject.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}