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
        public void TakeDamage(float damage,int R)
        {
            currentHP -= damage;
            systemText.TextSet("Enemy" + damage + "Damage! HP:" + currentHP);
            int Rota =DirUtil.ReverseDirection(R);
            transform.rotation = Quaternion.Euler(0, Rota, 0);

            if (currentHP <= 0 && EnemyDeath != null)
            {
                EnemyDeath();
            }
        }

    }
}