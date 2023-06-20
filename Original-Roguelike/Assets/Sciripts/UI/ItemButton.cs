using ItemSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEditor.PackageManager.UI;
using UnityEngine.UIElements;

namespace UISystem
{
    public class ItemButton : MonoBehaviour
    {
        public TextMeshProUGUI informationText;
        public ItemData itemData;
        private CanvasGroup canvasGroup;
        public PlayerUseItem playerUseItem;
        private CreateItemButton createItemButton;


        void Start()
        {
            canvasGroup = GetComponentInParent<CanvasGroup>();
            createItemButton = GetComponentInParent<CreateItemButton>();

        }

        public void OnSelected()
        {
            if (canvasGroup == null || canvasGroup.interactable)
            {
                if (EventSystem.current.currentSelectedGameObject != gameObject)
                {
                    EventSystem.current.SetSelectedGameObject(gameObject);
                }
                string desciption = itemData.Desciption;
                informationText.text = desciption;
            }
        }
        public void OnDeselected()
        {
            informationText.text = "";
        }

        public void UseItem()
        {
            itemData = playerUseItem.UseItem(itemData);

            switch (itemData.ItemType)
            {
                case 0:
                    if(((UseItemData)itemData).ItemStack == 0) Destroy(this.gameObject);

                        createItemButton.SetButtonTextAfterUseItem(this.gameObject);
  
                    break;
                default:
                    Destroy(this.gameObject); 
                    break;
            }
        }
    }
}