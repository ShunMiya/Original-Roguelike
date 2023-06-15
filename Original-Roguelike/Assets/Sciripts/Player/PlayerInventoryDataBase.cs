using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItemSystem
{

    [CreateAssetMenu(menuName = "ScriptableObject/PlayerInventoryDB")]
    public class PlayerInventoryDataBase : ScriptableObject
    {
        public List<ItemData> inventory = new List<ItemData>();

        public void AddItem(string itemId)
        {
            ItemData itemData = ItemManager.Instance.GetItemDataById(itemId);

            ItemData existingItem = inventory.Find(item => item.Id == itemId);

            if (existingItem != null)
            {
                existingItem.ItemStack += itemData.ItemStack;
            }
            else
            {
                inventory.Add(itemData);
            }

            Debug.Log(itemData.ItemName + "���擾");

            //�f�o�b�O�V�X�e��
            Debug.Log("�����A�C�e���ꗗ");
            foreach (ItemData item in inventory)
            {
                Debug.Log(itemData.ItemName + "�~" + itemData.ItemStack);
            }
        }
    }
}