using UnityEngine;
using PlayerStatusSystemV2;
using ItemSystemV2.Inventory;
using System;
using System.Collections;
using AttackSystem;
using PlayerV2;

namespace ItemSystemV2
{
    public class PlayerUseItemV2 : MonoBehaviour
    {
        [SerializeField] private SQLInventoryRemoveV2 SQLinventoryremove;
        public PlayerStatusV2 playerStatusV2;
        [SerializeField] private PlayerHPV2 playerHP;
        [SerializeField] private PlayerHungryV2 playerHungry;
        bool ItemUse;
        private DataRow row;
        private int ItemType;

        public void SetData(DataRow date, int type)
        {
            row = date;
            ItemType = type;
            PlayerAction PA = GetComponent<PlayerAction>();
            PA.PlayerUseItemV2 = this;

        }

        public IEnumerator UseItem()
        {
            int remainingStock = 0;
            switch (ItemType)
            {
                case 0:
                    ItemUse = ConsumableUse(row);

                    if (ItemUse == false)
                    {
                        int itemStock = Convert.ToInt32(row["Num"]);
                        remainingStock = itemStock;
                        break;
                    }
                    remainingStock = SQLinventoryremove.RemoveItem(row, 0);

                    break;
                case 1:

                    remainingStock = SQLinventoryremove.RemoveItem(row, 1);
                    playerStatusV2.WeaponStatusPlus();

                    break;
            }
            yield return null;
        }

        public bool ConsumableUse(DataRow row)
        {
            int Id = Convert.ToInt32(row["Id"]);
            ConsumableDataV2 itemData = ItemDataCacheV2.GetConsumable(Id);
            switch (itemData.ConsumableType)
            {
                case 1:
                    ItemUse = playerHP.HealHP(itemData.HealValue);
                    break;
                case 2:
                    ItemUse = playerHungry.HealHungry(itemData.HealValue);
                    break;
            }
            return ItemUse;
        }
    }
}