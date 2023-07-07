using UnityEngine;
using PlayerStatusList;

namespace ItemSystem
{
    public class PlayerUseItem : MonoBehaviour
    {
        public PlayerInventoryDataBase playerInventory;
        public PlayerStatus playerStatus;

        private void Start()
        {
            playerStatus = GetComponent<PlayerStatus>();
        }

        public int UseItem(IItemData itemData)
        {
            int itemStock = 0;
            switch (itemData.ItemType)
            {
                case 0:
                    if (itemData is Consumable consumable)
                    {
                        int itemId = consumable.Id;
                        itemStock = consumable.ItemStock;

                        //�A�C�e���̌��ʏ����BRemoveItem�������ɋL�ځB

                        itemStock =playerInventory.RemoveItem(itemId, itemStock);
                    }
                    break;
                default:
                    int ItemId = itemData.Id;

                    //������������
                    if (((Equipment)itemData).Equipped == true) playerStatus.UnequipItem(itemData);

                    playerInventory.RemoveItem(ItemId, 0);

                    break;
            }
            return itemStock;
        }

    }
}