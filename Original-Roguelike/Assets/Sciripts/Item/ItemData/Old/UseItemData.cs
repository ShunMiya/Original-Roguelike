using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItemSystem;

namespace ItemSystem
{
    [CreateAssetMenu(fileName = "NewUseItemData", menuName = "ScriptableObject/ItemData/UseItem")]
    public class UseItemData : ItemData
    {
        public int ItemStack;

        [SerializeField] private int _MaxStack;
        public int MaxStack { get { return _MaxStack; } }

        [SerializeField] private float _HealValue;
        public float HealValue { get { return _HealValue; } }

    }
}