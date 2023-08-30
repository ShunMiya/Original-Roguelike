using Field;
using ItemSystemV2.Inventory;
using MoveSystem;
using PlayerV2;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ItemSystemV2
{
    public class PlayerThrowItem : MonoBehaviour
    {
        private SQLInventoryRemoveV2 inventoryremove;
        private ItemFactoryV2 itemfactory;
        private MoveAction move;
        private Areamap field;

        private DataRow row;

        private void Start()
        {
            inventoryremove = GetComponent<SQLInventoryRemoveV2>();
            move = GetComponent<MoveAction>();
            itemfactory = FindObjectOfType<ItemFactoryV2>();
            field = FindObjectOfType<Areamap>();
        }

        public void SetData(DataRow date)
        {
            row = date;
            PlayerAction PA = GetComponent<PlayerAction>();
            PA.playerThrowItem = this;
        }

        public IEnumerator ThrowItem()
        {
            GameObject AreaObj = field.IsCollideReturnAreaObj(move.grid.x, move.grid.z);
            GameObject ItemObj = itemfactory.ThrowItemCreate(move.grid, Convert.ToInt32(row["Id"]), Convert.ToInt32(row["Num"]));
            yield return new WaitForEndOfFrame();

            int R = (int)transform.rotation.eulerAngles.y;
            if (R > 180) R -= 360;
            yield return StartCoroutine(ItemObj.GetComponent<MoveThrownItem>().Throw(R, AreaObj));

            inventoryremove.RemoveItem(row, 2);

            yield return null;
        }
    }
}