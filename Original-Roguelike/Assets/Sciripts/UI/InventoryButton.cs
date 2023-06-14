using ItemSystem;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ItemSystem
{
    public class InventoryButton : MonoBehaviour
    {
        public PlayerInventoryDataBase playerInventory;
        public Button buttonPrefab;
        public Transform buttonContainer;

        [SerializeField] private int totalTextLength = 20;

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.X)) //âºíuÇ´
            {
                SetButton();
            }
        }

        private void SetButton()
        {
            ClearButtons();

            var sortedInventory = playerInventory.inventory.OrderBy(x => x.Key);
            foreach (var item in sortedInventory)
            {
                string itemId = item.Key;
                int itemCount = item.Value;

                ItemData itemData = ItemManager.Instance.GetItemDataById(itemId);
                string itemName = itemData.ItemName;

                Button button = Instantiate(buttonPrefab, buttonContainer);

                TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();

                if (itemData.ItemType == ItemType.UseItem)
                {
                    buttonText.text = FormatItemText(itemName, itemCount);
                }
                else
                {
                    buttonText.text = itemName;
                }
            }
        }

        private void ClearButtons()
        {
            Button[] existingButtons = buttonContainer.GetComponentsInChildren<Button>();
            foreach (Button button in existingButtons)
            {
                Destroy(button.gameObject);
            }
        }

        private string FormatItemText(string itemName, int itemCount)
        {
            string itemText = $"{itemName}Å~{itemCount}";
            int spacesToAdd = totalTextLength - itemText.Length;

            if (spacesToAdd > 0)
            {
                string spaceText = new string(' ', spacesToAdd);
                itemText = $"{itemName}{spaceText}Å~{itemCount}";
            }

            return itemText;
        }
    }
}