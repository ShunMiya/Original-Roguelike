using UnityEngine;
using PlayerStatusList;
using ItemSystemSQL.Inventory;
using System;
using static UnityEditor.Progress;

namespace ItemSystemSQL
{
    public class PlayerUseItemSQL : MonoBehaviour
    {
        [SerializeField]private SQLInventoryRemove SQLinventoryremove;
        public PlayerStatusSQL playerStatusSQL;
        [SerializeField]private PlayerHP playerHP;

        public int UseItem(DataRow row,int ItemType)
        {
            int remainingStock = 0;
            switch (ItemType)
            {
                case 0:
                    int Id = Convert.ToInt32(row["Id"]);
                    ConsumableData itemData = ItemDataCache.GetConsumable(Id);
                    bool ItemUse = playerHP.HealHP(itemData.HealValue);

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
    }
}