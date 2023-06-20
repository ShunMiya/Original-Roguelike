using ItemSystem;
using System.Linq;
using TMPro;
using UISystem;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace ItemSystem
{
    public class CreateItemButton : MonoBehaviour
    {
        public PlayerInventoryDataBase playerInventory;
        public ItemButton buttonPrefab;
        public Transform buttonContainer;
        [SerializeField] private TextMeshProUGUI informationText;
        [SerializeField] private PlayerUseItem playerUseItem;



        [SerializeField] private int totalTextLength = 20;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.C)) //���u��
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
            string itemText = $"{itemName}�~{itemStack}";
            int spacesToAdd = totalTextLength - itemText.Length;

            if (spacesToAdd > 0)
            {
                string spaceText = new string(' ', spacesToAdd);
                itemText = $"{itemName}{spaceText}�~{itemStack}";
            }

            return itemText;
        }

        public void SetButtonTextAfterUseItem(GameObject buttonObject)
        {
            ItemButton itemButton = buttonObject.GetComponent<ItemButton>();
            ItemData itemData = itemButton.itemData;
            string itemName = itemData.ItemName;
            TextMeshProUGUI buttonText = buttonObject.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = FormatItemText(itemName, ((UseItemData)itemData).ItemStack);
        }
        #region dead specification
        public void SetButtonAfterUseItem(ItemData AfteritemData)
        {
            SetButton();
            ItemButton[] existingButtons = buttonContainer.GetComponentsInChildren<ItemButton>();
            Debug.Log("A�ɓ��B");
            switch (AfteritemData.ItemType)
            {
                case 0:
                    {
                        UseItemData afteruseItemData = AfteritemData as UseItemData;

                        foreach (ItemButton button in existingButtons)
                        {
                            Debug.Log("B1�ɓ��B");
                            ItemButton itemButton = button.GetComponent<ItemButton>();
                            UseItemData buttonUseItemData = itemButton.itemData as UseItemData;
                            Debug.Log(buttonUseItemData.Id + "��" + afteruseItemData.Id);
                            Debug.Log(buttonUseItemData.ItemStack + "��" + afteruseItemData.ItemStack);
                            if (buttonUseItemData.Id == afteruseItemData.Id &&
                            buttonUseItemData.ItemStack == afteruseItemData.ItemStack)
                            {
                                Debug.Log("���B");
                                EventSystem.current.SetSelectedGameObject(itemButton.gameObject);
                                return;
                            }
                        }
                        Debug.Log("B2�ɓ��B");

                        foreach (ItemButton button in existingButtons)
                        {
                            ItemButton itemButton = button.GetComponent<ItemButton>();

                            if (itemButton.itemData == AfteritemData)
                            {
                                EventSystem.current.SetSelectedGameObject(itemButton.gameObject);
                                return;
                            }
                        }
                        Debug.Log("B3�ɓ��B");

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
                        Debug.Log("B4�ɓ��B");

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
                        Debug.Log("C1�ɓ��B");

                        foreach (ItemButton button in existingButtons)
                        {
                            ItemButton itemButton = button.GetComponent<ItemButton>();

                            if (itemButton.itemData.Id == AfteritemData.Id)
                            {
                                Debug.Log("���B");
                                EventSystem.current.SetSelectedGameObject(itemButton.gameObject);
                                return;
                            }
                            continue;
                        }
                        Debug.Log("C2�ɓ��B");

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
                        Debug.Log("C3�ɓ��B");

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
        }
        #endregion
    }
}