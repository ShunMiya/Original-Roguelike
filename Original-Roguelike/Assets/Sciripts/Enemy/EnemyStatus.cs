using UISystem;
using UnityEngine;

namespace Enemy
{
    public class EnemyStatus : MonoBehaviour
    {
        public delegate void EnemyDefeatedEventHandler();
        public event EnemyDefeatedEventHandler EnemyDefeated;
        private SystemText systemText;

        public float currentHP;
        public int EnemyID;

        private void Start()
        {
            systemText = FindObjectOfType<SystemText>();
        }
        public void TakeDamage(float damage)
        {
            currentHP -= damage;
            systemText.TextSet("Enemy"+damage+"Damage! HP:" + currentHP);

            if (currentHP <= 0 && EnemyDefeated != null)
            {
                EnemyDefeated();
            }
        }
    }
}