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
            switch (itemData.ItemType)
            {
                case 0:
                    Debug.Log("ID" + itemData.Id + "‚ÅStack" + ((UseItemData)itemData).ItemStack + "‚ð‘I‘ð");
                    int remainingStack = playerUseItem.UseItem(itemData);
                    Debug.Log("ID" + itemData.Id + "‚ÅStack" + remainingStack + "‚É•Ï‰»");
                    
                    createItemButton.SetButtonTextAfterUseItem(itemData.Id, remainingStack);
  
                    break;
                default:
                    playerUseItem.UseItem(itemData);

                    createItemButton.SelectButtonChangeForDestruction(this);
                    break;
            }
        }
    }
}