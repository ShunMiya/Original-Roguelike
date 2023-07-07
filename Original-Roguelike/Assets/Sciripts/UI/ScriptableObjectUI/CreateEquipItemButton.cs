using System.Linq;
using TMPro;
using ItemSystem;
using UnityEngine;

namespace UISystem
{
    public class CreateEquipItemButton : MonoBehaviour
    {
        public PlayerInventoryDataBase playerInventory;
        public ItemButton buttonPrefab;
        public Transform buttonContainer;
        [SerializeField] private TextMeshProUGUI informationText;
        [SerializeField] private PlayerUseItem playerUseItem;

        [SerializeField] private int totalTextLength;


        public void SetButton()
        {
            ClearButtons();

            var sortedInventory = playerInventory.inventory.OrderBy(x => x.Id);
            foreach (var itemData in sortedInventory)
            {
                if(itemData.ItemType != 0)
                {
                    string itemName = itemData.ItemName;

                    ItemButton button = Instantiate(buttonPrefab, buttonContainer);
                    ItemButton itemButton = button.GetComponent<ItemButton>();
                    itemButton.itemData = itemData;
                    itemButton.informationText = informationText;
                    itemButton.playerUseItem = playerUseItem;

                    TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();

                    buttonText.text = itemName;
                }
            }
        }

        private void ClearButtons()
        {
            ItemButton[] existingButtons = buttonContainer.GetComponentsInChildren<ItemButton>();
            foreach (ItemButton button in existingButtons)
            {
                Destroy(button.gameObject);
            }
        }
    }
}
