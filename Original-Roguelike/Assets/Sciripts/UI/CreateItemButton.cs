using System.Linq;
using TMPro;
using ItemSystem;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.C)) //仮置き
            {
                SetButton();
            }
        }

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
                    buttonText.text = FormatItemText(itemName, ((UseItemData)itemData).ItemStack);
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
            string itemText = $"{itemName}×{itemStack}";
            int spacesToAdd = totalTextLength - itemText.Length;

            if (spacesToAdd > 0)
            {
                string spaceText = new string(' ', spacesToAdd);
                itemText = $"{itemName}{spaceText}×{itemStack}";
            }

            return itemText;
        }

        public void SetButtonTextAfterUseItem(string itemId ,int itemStack)
        {
            Debug.Log("ID" +itemId + "でStack" +itemStack + "を探す");

            ItemButton[] existingButtons = buttonContainer.GetComponentsInChildren<ItemButton>();
            foreach (ItemButton button in existingButtons)
            {
                ItemButton existingButton = button.GetComponent<ItemButton>();

                if (existingButton.itemData.Id == itemId && ((UseItemData)existingButton.itemData).ItemStack == itemStack)
                {
                    Debug.Log("発見");
                    string itemName = existingButton.itemData.ItemName;
                    TextMeshProUGUI buttonText = existingButton.GetComponentInChildren<TextMeshProUGUI>();
                    buttonText.text = FormatItemText(itemName,itemStack);
                    Debug.Log("書き換え完了");

                    EventSystem eventSystem = EventSystem.current;
                    eventSystem.SetSelectedGameObject(existingButton.gameObject);

                    if (itemStack == 0) SelectButtonChangeForDestruction(existingButton);
                }
            }
            Debug.Log("発見失敗");
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
    #region dead specification
/*public void SetButtonAfterUseItem(ItemData AfteritemData)
        {
            SetButton();
            ItemButton[] existingButtons = buttonContainer.GetComponentsInChildren<ItemButton>();
            Debug.Log("Aに到達");
            switch (AfteritemData.ItemType)
            {
                case 0:
                    {
                        UseItemData afteruseItemData = AfteritemData as UseItemData;

                        foreach (ItemButton button in existingButtons)
                        {
                            Debug.Log("B1に到達");
                            ItemButton itemButton = button.GetComponent<ItemButton>();
                            UseItemData buttonUseItemData = itemButton.itemData as UseItemData;
                            Debug.Log(buttonUseItemData.Id + "と" + afteruseItemData.Id);
                            Debug.Log(buttonUseItemData.ItemStack + "と" + afteruseItemData.ItemStack);
                            if (buttonUseItemData.Id == afteruseItemData.Id &&
                            buttonUseItemData.ItemStack == afteruseItemData.ItemStack)
                            {
                                Debug.Log("到達");
                                EventSystem.current.SetSelectedGameObject(itemButton.gameObject);
                                return;
                            }
                        }
                        Debug.Log("B2に到達");

                        foreach (ItemButton button in existingButtons)
                        {
                            ItemButton itemButton = button.GetComponent<ItemButton>();

                            if (itemButton.itemData == AfteritemData)
                            {
                                EventSystem.current.SetSelectedGameObject(itemButton.gameObject);
                                return;
                            }
                        }
                        Debug.Log("B3に到達");

                        ItemButton nextButton = null;
                        int nextId = int.MaxValue;
                        foreach (ItemButton button in existingButtons)
                        {
                            ItemButton itemButton = button.GetComponent<ItemButton>();

                            if (int.Parse(itemButton.itemData.Id) > int.Parse(AfteritemData.Id) && int.Parse(itemButton.itemData.Id) < nextId)
                            {
                                nextButton = itemButton;
                                nextId = int.Parse(itemButton.itemData.Id);
                            }
                        }
                        if (nextButton != null)
                        {
                            EventSystem.current.SetSelectedGameObject(nextButton.gameObject);
                            return;
                        }
                        Debug.Log("B4に到達");

                        ItemButton maxIdButton = null;
                        int maxId = int.MinValue;
                        foreach (ItemButton button in existingButtons)
                        {
                            ItemButton itemButton = button.GetComponent<ItemButton>();

                            if (int.Parse(itemButton.itemData.Id) > maxId)
                            {
                                maxIdButton = itemButton;
                                maxId = int.Parse(itemButton.itemData.Id);
                            }
                        }
                        if (maxIdButton != null)
                        {
                            EventSystem.current.SetSelectedGameObject(maxIdButton.gameObject);
                            return;
                        }
                        break;
                    }


                default:
                    {
                        Debug.Log("C1に到達");

                        foreach (ItemButton button in existingButtons)
                        {
                            ItemButton itemButton = button.GetComponent<ItemButton>();

                            if (itemButton.itemData.Id == AfteritemData.Id)
                            {
                                Debug.Log("到達");
                                EventSystem.current.SetSelectedGameObject(itemButton.gameObject);
                                return;
                            }
                            continue;
                        }
                        Debug.Log("C2に到達");

                        ItemButton nextButton = null;
                        int nextId = int.MaxValue;
                        foreach (ItemButton button in existingButtons)
                        {
                            ItemButton itemButton = button.GetComponent<ItemButton>();

                            if (int.Parse(itemButton.itemData.Id) > int.Parse(AfteritemData.Id) && int.Parse(itemButton.itemData.Id) < nextId)
                            {
                                nextButton = itemButton;
                                nextId = int.Parse(itemButton.itemData.Id);
                            }
                        }
                        if (nextButton != null)
                        {
                            EventSystem.current.SetSelectedGameObject(nextButton.gameObject);
                            return;
                        }
                        Debug.Log("C3に到達");

                        ItemButton maxIdButton = null;
                        int maxId = int.MinValue;
                        foreach (ItemButton button in existingButtons)
                        {
                            ItemButton itemButton = button.GetComponent<ItemButton>();

                            if (int.Parse(itemButton.itemData.Id) > maxId)
                            {
                                maxIdButton = itemButton;
                                maxId = int.Parse(itemButton.itemData.Id);
                            }
                        }
                        if (maxIdButton != null)
                        {
                            EventSystem.current.SetSelectedGameObject(maxIdButton.gameObject);
                            return;
                        }
                        break;
                    }
            }
        }*/
        #endregion
}