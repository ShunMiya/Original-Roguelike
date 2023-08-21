using Enemy;
using System.Collections;
using System.Collections.Generic;
using UISystemV2;
using UnityEngine;

namespace EnemySystem
{
    public class EnemyStatusV2 : MonoBehaviour
    {
        public delegate void EnemyDeathEvent();
        public event EnemyDeathEvent EnemyDeath;
        private SystemTextV2 systemText;

        public float currentHP;
        public int EnemyID;

        private void Start()
        {
            systemText = FindObjectOfType<SystemTextV2>();
            EnemyDataV2 enemy = EnemyDataCacheV2.GetEnemyData(EnemyID);
            currentHP = enemy.MaxHP;

        }
        public void TakeDamage(float damage)
        {
            currentHP -= damage;
            systemText.TextSet("Enemy" + damage + "Damage! HP:" + currentHP);

            if (currentHP <= 0 && EnemyDeath != null)
            {
                EnemyDeath();
            }
        }

    }
}