using UnityEngine;
using PlayerStatusSystemV2;
using ItemSystemV2.Inventory;
using System;
using System.Collections;
using PlayerV2;
using AttackSystem;

namespace ItemSystemV2
{
    public class PlayerUseItemV2 : MonoBehaviour
    {
        [SerializeField] private SQLInventoryRemoveV2 inventoryremove;
        [SerializeField] private PlayerEquipmentChange equipmentchange;
        [SerializeField] private PlayerHPV2 playerHP;
        [SerializeField] private PlayerHungryV2 playerHungry;
        [SerializeField] private PlayerThrowItem playerThrowItem;
        bool ItemUse;
        private DataRow row;
        private int ItemType;

        public void SetData(DataRow date, int type)
        {
            row = date;
            ItemType = type;
            PlayerAction PA = GetComponent<PlayerAction>();
            PA.playerUseItemV2 = this;
        }

        public IEnumerator UseItem()
        {
            switch (ItemType)
            {
                case 0:
                    ItemUse = ConsumableUse(row);

                    if (ItemUse == false)
                    {
                        break;
                    }
                    inventoryremove.RemoveItem(row, 1);

                    break;
                case 1:
                    if (Convert.ToInt32(row["Equipped"]) == 1 || Convert.ToInt32(row["Equipped"]) == 2)
                    {
                        equipmentchange.UnequipItem(row);
                        break;
                    }
                    equipmentchange.EquipItem(row);

                    break;
                case 2:
                    yield return StartCoroutine(OffensiveUse(row));

                    break;
            }
            yield return new WaitForSeconds(0.2f);
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

        public IEnumerator OffensiveUse(DataRow row)
        {
            yield return StartCoroutine(playerThrowItem.ThrowOffensiveItem(row));

            inventoryremove.RemoveItem(row, 0);
        }
    }
}