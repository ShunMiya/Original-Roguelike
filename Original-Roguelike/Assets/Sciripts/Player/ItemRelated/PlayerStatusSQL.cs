using UnityEngine;
using PlayerMovement;
using Combat.AttackMotion;
using ItemSystemSQL.Inventory;

namespace PlayerStatusList
{
    public class PlayerStatusSQL : MonoBehaviour
    {
        private PlayerMove playerMove;
        private AttackMotion attackMotion;

        public int inventorySize;

        [SerializeField] private SQLInventoryAdd SQLInventory;

        public bool PlayerActive = false; //移動、攻撃、アイテムの使用(装備の着脱含)

        private void Start()
        {
            playerMove = GetComponent<PlayerMove>();
            attackMotion = GetComponent<AttackMotion>();

            inventorySizeUpDate();
        }

        public void inventorySizeUpDate()
        {
            SQLInventory.InitializeFromPlayerStatus(inventorySize);
        }

        public bool IsPlayerActive()
        {
            return playerMove.IsMoving() || attackMotion.IsAttacking();
        }

    }
}