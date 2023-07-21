using UnityEngine;
using PlayerStatusList;
using ItemSystemSQL.Inventory;
using System;

namespace ItemSystemSQL
{
    public class PlayerUseItemSQL : MonoBehaviour
    {
        [SerializeField]private SQLInventoryRemove SQLinventoryremove;
        public PlayerStatusSQL playerStatusSQL;
        [SerializeField]private PlayerHP playerHP;
        [SerializeField]private PlayerHungry playerHungry;
        bool ItemUse;

        public int UseItem(DataRow row,int ItemType)
        {
            int remainingStock = 0;
            switch (ItemType)
            {
                case 0:
                    ItemUse = ConsumableUse(row);

                    if(ItemUse == false)
                    {
                        int itemStock = Convert.ToInt32(row["Num"]);
                        remainingStock = itemStock;
                        break;
                    }
                    remainingStock = SQLinventoryremove.RemoveItem(row, 0);

                    break;
                case 1:

                    remainingStock = SQLinventoryremove.RemoveItem(row, 1);
                    playerStatusSQL.WeaponStatusPlus();

                    break;
            }
            return remainingStock;
        }

        public bool ConsumableUse(DataRow row)
        {
            int Id = Convert.ToInt32(row["Id"]);
            ConsumableData itemData = ItemDataCache.GetConsumable(Id);
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