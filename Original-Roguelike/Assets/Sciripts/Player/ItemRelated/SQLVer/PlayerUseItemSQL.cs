using UnityEngine;
using PlayerStatusList;
using ItemSystemSQL.Inventory;
using System;
using UISystem;

namespace ItemSystemSQL
{
    public class PlayerUseItemSQL : MonoBehaviour
    {
        [SerializeField]private SQLInventoryRemove SQLinventoryremove;
        [SerializeField]private EquipmentItemSQL equipmentItemSQL;

        public int UseItem(DataRow row,int ItemType)
        {
            int remainingStock = 0;
            switch (ItemType)
            {
                case 0:

                    //アイテムの効果処理。RemoveItemも内部に記載。

                    remainingStock = SQLinventoryremove.RemoveItem(row, 0);

                    break;
                case 1:

                    remainingStock = SQLinventoryremove.RemoveItem(row, 1);

                    break;
            }
            return remainingStock;
        }

    }
}