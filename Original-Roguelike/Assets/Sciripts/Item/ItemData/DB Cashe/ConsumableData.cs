using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItemSystemSQL
{
    public class ConsumableData : IItemDataInventory
    {
        public int Id { get; set; }
        public string PrefabName { get; set; }
        public string ItemName { get; set; }
        public int ItemType { get; set; }
        public string Description { get; set; }
        public int ItemStock { get; set; }
        public int MaxStock { get; set; }
        public float HealValue { get; set; }
    }
}