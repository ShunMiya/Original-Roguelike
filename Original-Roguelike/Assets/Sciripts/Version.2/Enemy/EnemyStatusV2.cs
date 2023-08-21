using System.Collections;
using System.Collections.Generic;
using UISystemV2;
using UnityEngine;

namespace EnemySystem
{
    public class EnemyStatusV2 : MonoBehaviour
    {
        public delegate void EnemyDefeatedEventHandler();
        public event EnemyDefeatedEventHandler EnemyDefeated;
        private SystemTextV2 systemText;

        public float currentHP;
        public int EnemyID;

        private void Start()
        {
            systemText = FindObjectOfType<SystemTextV2>();
        }
        public void TakeDamage(float damage)
        {
            currentHP -= damage;
            systemText.TextSet("Enemy" + damage + "Damage! HP:" + currentHP);

            if (currentHP <= 0 && EnemyDefeated != null)
            {
                EnemyDefeated();
            }
        }

    }
}