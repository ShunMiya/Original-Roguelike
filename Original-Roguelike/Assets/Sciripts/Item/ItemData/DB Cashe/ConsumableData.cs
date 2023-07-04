using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItemSystem
{
    public class ConsumableData
    {
        public int Id { get; set; }
        public string PrefabName { get; set; }
        public string ItemName { get; set; }
        public string ItemType { get; set; }
        public string Description { get; set; }
        public int ItemStock { get; set; }
        public int MaxStock { get; set; }
        public float HealValue { get; set; }
    }
}