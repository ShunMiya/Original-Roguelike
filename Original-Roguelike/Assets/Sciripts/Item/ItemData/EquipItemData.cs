using ItemSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItemSystem
{
    [CreateAssetMenu(fileName = "NewEquipItemData", menuName = "ScriptableObject/ItemData/EquipItem")]
    public class EquipItemData : ItemData
    {
        [SerializeField] private float _AttackBonus;
        public new float AttackBonus { get { return _AttackBonus; } }

        [SerializeField] private float _WeaponRange;
        public new float WeaponRange { get { return _WeaponRange; } }

        [SerializeField] private float _WeaponDistance;
        public new float WeaponDistance { get { return _WeaponDistance; } }
    }
}
