using ItemSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

namespace UISystem
{
    public class ItemButton : MonoBehaviour
    {
        public TextMeshProUGUI informationText;
        public ItemData itemData;
        private CanvasGroup canvasGroup;
        public PlayerUseItem playerUseItem;
        private CreateItemButton createItemButton;
        private EquipmentItem equipmentItem;


        void Start()
        {
            canvasGroup = GetComponentInParent<CanvasGroup>();
            createItemButton = GetComponentInParent<CreateItemButton>();
            equipmentItem = transform.parent.parent.GetComponentInChildren<EquipmentItem>();
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

        public void EquipItem()
        {
            equipmentItem.EquipItem(itemData);
        }
    }
}