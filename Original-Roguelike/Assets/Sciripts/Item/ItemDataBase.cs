using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItemSystem
{
    [CreateAssetMenu(menuName = "ScriptableObject/ItemDataBase")]
    public class ItemDataBase : ScriptableObject
    {
        [SerializeField] private List<ItemData> itemLists = new List<ItemData>();

        public List<ItemData> GetItemLists()
        {
            return itemLists;
        }
    }
}
