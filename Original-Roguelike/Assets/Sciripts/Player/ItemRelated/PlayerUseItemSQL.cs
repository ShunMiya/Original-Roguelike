using UnityEngine;
using PlayerStatusList;
using ItemSystemSQL.Inventory;

namespace ItemSystemSQL
{
    public class PlayerUseItemSQL : MonoBehaviour
    {
        [SerializeField]private SQLInventoryRemove SQLinventoryremove;
        public PlayerStatusSQL playerStatusSQL;

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
                    playerStatusSQL.WeaponStatusPlus(); //UnknowError,BonusNotUpdated

                    break;
            }
            return remainingStock;
        }
    }
}