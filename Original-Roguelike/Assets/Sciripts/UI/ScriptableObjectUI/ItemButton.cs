using ItemSystem;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

namespace UISystem
{
    public class ItemButton : MonoBehaviour
    {
        public TextMeshProUGUI informationText;
        public IItemData itemData;
        public PlayerUseItem playerUseItem;
        private CreateItemButton createItemButton;
        private EquipmentItem equipmentItem;


        void Start()
        {
            createItemButton = GetComponentInParent<CreateItemButton>();
            equipmentItem = transform.parent.parent.GetComponentInChildren<EquipmentItem>();
        }

        public void OnSelected()
        {
            if (EventSystem.current.currentSelectedGameObject != gameObject)
            {
                EventSystem.current.SetSelectedGameObject(gameObject);
            }
            string desciption = itemData.Desciption;
            informationText.text = desciption;
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
                    Debug.Log("ID" + itemData.Id + "‚ÅStack" + ((Consumable)itemData).ItemStock + "‚ð‘I‘ð");
                    int remainingStack = playerUseItem.UseItem(itemData);
                    Debug.Log("ID" + itemData.Id + "‚ÅStack" + remainingStack + "‚É•Ï‰»");
                    
                    createItemButton.SetButtonAfterUseItem(itemData.Id, remainingStack);
  
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