using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItemSystem
{
    public enum ItemType
    {
        UseItem = 0,
        EquipItem = 1,
    }

    [CreateAssetMenu(menuName = "ScriptableObject/ItemData")]
    public class ItemData : ScriptableObject
    {
        [SerializeField] private string _id;
        public string Id { get { return _id; } }

        [SerializeField] private string _itemName;
        public string ItemName { get { return _itemName; } }

        [SerializeField] private ItemType _itemType;
        public ItemType itemType { get { return _itemType; } }

        [SerializeField] private string _MaxStack;
        public string maxStack { get { return _MaxStack; } }

    }
}
