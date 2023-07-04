using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItemSystem
{
    [CreateAssetMenu(fileName = "NewConsumable", menuName = "ScriptableObject/ItemData/Consumable")]
    public class Consumable : ScriptableObject,IItemData
    {
        [SerializeField] private int _id;
        public int Id { get { return _id; } }

        [SerializeField] private GameObject _prefab;
        public GameObject Prefab { get { return _prefab; } }

        [SerializeField] private string _itemName;
        public string ItemName { get { return _itemName; } }

        [SerializeField] private ItemType _itemType;
        public ItemType ItemType { get { return _itemType; } }

        [TextArea(5, 20)]
        [SerializeField] private string _desciption;
        public string Desciption { get { return _desciption; } }

        public int ItemStock;

        [SerializeField] private int _maxStock;
        public int MaxStock { get { return _maxStock; } }

        [SerializeField] private float _healValue;
        public float HealValue { get { return _healValue; } }

    }
}