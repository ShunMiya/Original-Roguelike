using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItemSystem;

namespace ItemSystem
{
    [CreateAssetMenu(fileName = "NewUseItemData", menuName = "ScriptableObject/ItemData/UseItem")]
    public class UseItemData : ItemData
    {
        public new int ItemStack;

        [SerializeField] private int _MaxStack;
        public new int MaxStack { get { return _MaxStack; } }

        [SerializeField] private float _HealValue;
        public new float HealValue { get { return _HealValue; } }

    }
}