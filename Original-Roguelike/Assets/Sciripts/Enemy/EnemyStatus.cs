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
        [SerializeField] private int EnemyID;

        public void TakeDamage(float damage)
        {
            currentHP -= damage;
            systemText.TextSet(damage + "Damage! currentHP:" + currentHP);

            if (currentHP <= 0 && EnemyDefeated != null)
            {
                EnemyDefeated();
            }
        }
    }
}