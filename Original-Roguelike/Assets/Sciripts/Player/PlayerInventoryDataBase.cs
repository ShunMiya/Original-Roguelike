using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItemSystem
{

    [CreateAssetMenu(menuName = "ScriptableObject/PlayerInventoryDB")]
    public class PlayerInventoryDataBase : ScriptableObject
    {
        public Dictionary<string, int> inventory = new Dictionary<string, int>();

        public void AddItem(string itemId)
        {
            ItemData itemData = ItemManager.Instance.GetItemDataById(itemId);

            if (/*itemData.ItemType == ItemType.UseItem &&*/ inventory.ContainsKey(itemData.Id))
            {
                inventory[itemData.Id]++;
            }
            else
            {
                inventory.Add(itemData.Id, 1);
            }
            Debug.Log(itemData.ItemName + "���擾");

            /*�f�o�b�O�V�X�e��
            Debug.Log("�����A�C�e���ꗗ");
            foreach (KeyValuePair<string, int> item in inventory)
            {
                string itemName = ItemManager.Instance.GetItemNameById(item.Key);
                Debug.Log(itemName + ": " + item.Value);
            }*/
        }
    }
}