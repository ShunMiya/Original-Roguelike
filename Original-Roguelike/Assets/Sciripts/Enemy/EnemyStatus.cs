using UISystem;
using UnityEngine;

namespace Enemy
{
    public class EnemyStatus : MonoBehaviour
    {
        public delegate void EnemyDefeatedEventHandler();
        public event EnemyDefeatedEventHandler EnemyDefeated;
        public SystemText systemText;

        [SerializeField] private float currentHP;
        public int EnemyID;

        public void TakeDamage(float damage)
        {
            currentHP -= damage;
            systemText.TextSet(damage + "Damage! HP:" + currentHP);

            if (currentHP <= 0 && EnemyDefeated != null)
            {
                EnemyDefeated();
            }
        }
    }
}