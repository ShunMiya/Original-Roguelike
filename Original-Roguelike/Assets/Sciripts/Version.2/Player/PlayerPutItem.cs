using ItemSystemV2.Inventory;
using MoveSystem;
using Performances;
using PlayerV2;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItemSystemV2
{
    public class PlayerPutItem : MonoBehaviour
    {
        private SQLInventoryRemoveV2 inventoryremove;
        private ItemFactoryV2 itemfactory;
        private MoveAction move;
        private FieldSoundEffect fieldSoundEffect;

        private DataRow row;

        private void Start()
        {
            inventoryremove = GetComponent<SQLInventoryRemoveV2>();
            move = GetComponent<MoveAction>();
            itemfactory = FindObjectOfType<ItemFactoryV2>();
            fieldSoundEffect = FindObjectOfType<FieldSoundEffect>();
        }

        public void SetData(DataRow date)
        {
            row = date;
            PlayerAction PA = GetComponent<PlayerAction>();
            PA.playerPutItem = this;
        }

        public IEnumerator PutItem()
        {
            fieldSoundEffect.GimmickSE(7);

            itemfactory.SpecifiedItemCreate(move.grid, Convert.ToInt32(row["Id"]), Convert.ToInt32(row["Num"]));

            inventoryremove.RemoveItem(row, 1);

            yield return new WaitForSeconds(0.2f);
        }
    }
}