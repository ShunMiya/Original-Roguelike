using System.Linq;
using TMPro;
using ItemSystem;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UISystem
{
    public class CreateItemButton : MonoBehaviour
    {
        public PlayerInventoryDataBase playerInventory;
        public ItemButton buttonPrefab;
        public Transform buttonContainer;
        [SerializeField] private TextMeshProUGUI informationText;
        [SerializeField] private PlayerUseItem playerUseItem;
        [SerializeField] private GameObject MenuButton;

        [SerializeField] private int totalTextLength;

        public void SetButton()
        {
            ClearButtons();

            var sortedInventory = playerInventory.inventory.OrderBy(x => x.Id);
            foreach (var itemData in sortedInventory)
            {
                string itemName = itemData.ItemName;

                ItemButton button = Instantiate(buttonPrefab, buttonContainer);
                ItemButton itemButton = button.GetComponent<ItemButton>();
                itemButton.itemData = itemData;
                itemButton.informationText = informationText;
                itemButton.playerUseItem = playerUseItem;

                TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();

                if (itemData.ItemType == ItemType.UseItem)
                {
                    buttonText.text = FormatItemText(itemName, ((Consumable)itemData).ItemStock);
                }
                else
                {
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

        private string FormatItemText(string itemName, int itemStack)
        {
            string itemText = $"{itemName}Å~{itemStack}";
            int spacesToAdd = totalTextLength - itemText.Length;

            if (spacesToAdd > 0)
            {
                string spaceText = new string(' ', spacesToAdd);
                itemText = $"{itemName}{spaceText}Å~{itemStack}";
            }

            return itemText;
        }


        public void SetButtonAfterUseItem(int itemId ,int itemStock)
        {
            Debug.Log("ID" +itemId + "Ç≈Stack" +itemStock + "ÇíTÇ∑");

            ItemButton[] existingButtons = buttonContainer.GetComponentsInChildren<ItemButton>();
            foreach (ItemButton button in existingButtons)
            {
                ItemButton existingButton = button.GetComponent<ItemButton>();

                if (existingButton.itemData.Id == itemId && ((Consumable)existingButton.itemData).ItemStock == itemStock)
                {
                    Debug.Log("î≠å©");
                    string itemName = existingButton.itemData.ItemName;
                    TextMeshProUGUI buttonText = existingButton.GetComponentInChildren<TextMeshProUGUI>();
                    buttonText.text = FormatItemText(itemName,itemStock);
                    Debug.Log("èëÇ´ä∑Ç¶äÆóπ");

                    EventSystem eventSystem = EventSystem.current;
                    eventSystem.SetSelectedGameObject(existingButton.gameObject);

                    if (itemStock == 0) SelectButtonChangeForDestruction(existingButton);
                }
            }
        }

        public void SelectButtonChangeForDestruction(ItemButton button)
        {
            ItemButton[] existingButtons = buttonContainer.GetComponentsInChildren<ItemButton>();
            EventSystem eventSystem = EventSystem.current;

            if (existingButtons.Length > 1)
            {
                GameObject currentSelectedObject = eventSystem.currentSelectedGameObject;

                GameObject nextSelectedObject = null;
                for (int i = 0; i < existingButtons.Length; i++)
                {
                    if (existingButtons[i].gameObject == currentSelectedObject)
                    {
                        if (i < existingButtons.Length - 1)
                        {
                            nextSelectedObject = existingButtons[i + 1].gameObject;
                        }
                        else
                        {
                            nextSelectedObject = existingButtons[i - 1].gameObject;
                        }
                        break;
                    }
                }

                if (nextSelectedObject == null)
                {
                    nextSelectedObject = existingButtons[existingButtons.Length - 1].gameObject;
                }
                eventSystem.SetSelectedGameObject(nextSelectedObject);
            }
            else
            {
                EventSystem.current.SetSelectedGameObject(MenuButton);
            }
            Destroy(button.gameObject);
            return;
        }
    }
}