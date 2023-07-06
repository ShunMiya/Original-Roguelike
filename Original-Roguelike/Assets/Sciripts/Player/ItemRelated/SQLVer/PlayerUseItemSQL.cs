using UnityEngine;
using PlayerStatusList;
using ItemSystemSQL.Inventory;

namespace ItemSystemSQL
{
    public class PlayerUseItemSQL : MonoBehaviour
    {
        private PlayerStatus playerStatus;
        private SQLInventoryRemove SQLinventoryremove;

        private void Start()
        {
            playerStatus = GetComponent<PlayerStatus>();
            SQLinventoryremove = GetComponent<SQLInventoryRemove>();
        }

        public int UseItem(DataRow row,int ItemType)
        {
            int remainingStock = 0;
            switch (ItemType)
            {
                case 0:

                        //アイテムの効果処理。RemoveItemも内部に記載。

                    remainingStock = SQLinventoryremove.RemoveItem(row, 0);

                    break;
                default:

                    //装備解除処理
//                    if (((Equipment)itemData).Equipped == true) playerStatus.UnequipItem(itemData);

                    remainingStock = SQLinventoryremove.RemoveItem(row, 1);

                    break;
            }
            return remainingStock;
        }

    }
}