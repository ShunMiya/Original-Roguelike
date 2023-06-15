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

    public abstract class ItemData : ScriptableObject
    {
        [SerializeField] private string _id;
        public string Id { get { return _id; } }

        [SerializeField] private GameObject prefab;
        public GameObject Prefab { get { return prefab; } }

        [SerializeField] private string _itemName;
        public string ItemName { get { return _itemName; } }

        [SerializeField] private ItemType _itemType;
        public ItemType ItemType { get { return _itemType; } }

        [TextArea(5, 20)]
        [SerializeField] private string _desciption;
        public string Desciption { get { return _desciption; } }

        public int ItemStack { get; set; }
        public int MaxStack { get; }
        public float HealValue { get; }


        public int AttackBonus { get; }

        public int WeaponRange { get; }

        public int WeaponDistance { get; }
    }
}