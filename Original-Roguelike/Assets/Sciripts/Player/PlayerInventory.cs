using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItemSystem
{
    public class PlayerInventory : MonoBehaviour
    {
        private Dictionary<string, int> inventory = new Dictionary<string, int>();

        public void AddItem(ItemData itemData)
        {
            if (inventory.ContainsKey(itemData.Id))
            {
                inventory[itemData.Id]++;
            }
            else
            {
                inventory.Add(itemData.Id, 1);
            }
            Debug.Log(itemData.ItemName + "���擾");

            //�f�o�b�O�V�X�e��
            Debug.Log("�����A�C�e���ꗗ");
            foreach (KeyValuePair<string, int> item in inventory)
            {
                string itemName = ItemManager.Instance.GetItemNameById(item.Key);
                Debug.Log(itemName + ": " + item.Value);
            }
        }
    }
}
