using ItemSystemV2.Inventory;
using PlayerV2;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItemSystemV2
{
    public class PlayerThrowItem : MonoBehaviour
    {
        [SerializeField] private SQLInventoryRemoveV2 inventoryremove;
        private DataRow row;
        private int ItemType;

        public void SetData(DataRow date, int type)
        {
            row = date;
            ItemType = type;
            PlayerAction PA = GetComponent<PlayerAction>();
            //PA.PlayerUseItemV2 = this;
        }

        public IEnumerator ThrowItem()
        {
            yield break;
        }
    }
}