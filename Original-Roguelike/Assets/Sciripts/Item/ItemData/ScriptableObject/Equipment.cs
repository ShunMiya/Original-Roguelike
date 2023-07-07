using UnityEngine;

namespace ItemSystem
{
    public interface IItemData
    {
        int Id { get; }
        GameObject Prefab { get; }
        string ItemName { get; }
        ItemType ItemType { get; }
        string Desciption { get; }
    }

    public enum ItemType
    {
        UseItem = 0,
        EquipItem = 1,
    }

    public enum EquipType
    {
        OneHandedWeapon = 0,
        TwoHandedWeapon = 1,
        Armor = 2,
    }

    [CreateAssetMenu(fileName = "NewEquipment", menuName = "ScriptableObject/ItemData/Equipment")]
    public class Equipment : ScriptableObject,IItemData
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

        [SerializeField] private EquipType _equipType;
        public EquipType EquipType { get { return _equipType; } }

        public bool Equipped;

        [SerializeField] private float _attackBonus;
        public float AttackBonus { get { return _attackBonus; } }

        [SerializeField] private float _defenseBonus;
        public float DefenseBonus { get { return _defenseBonus; } }

        [SerializeField] private float _weaponRange;
        public float WeaponRange { get { return _weaponRange; } }

        [SerializeField] private float _weaponDistance;
        public float WeaponDistance { get { return _weaponDistance; } }
    }
}
