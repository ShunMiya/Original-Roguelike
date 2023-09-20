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
        private int Defense;

        private void Start()
        {
            systemText = FindObjectOfType<SystemTextV2>();
            EnemyDataV2 enemy = EnemyDataCacheV2.GetEnemyData(EnemyID);
            currentHP = enemy.MaxHP;
            Defense = enemy.Defense;

        }
        public void TakeDamage(float damage,int R, float HitRate)
        {
            #region –½’†—¦ˆ—
            int HitCheck = Random.Range(1,101);
            if(HitCheck > HitRate)
            {
                systemText.TextSet("NoHit!");
                return;
            }
            #endregion

            #region ‰ñ”ğ—¦ˆ—
            int EvasionCheck = UnityEngine.Random.Range(1, 101);
            if (EvasionCheck < GameRule.EvasionRate)
            {
                systemText.TextSet("NoHit!");
                return;
            }
            #endregion

            #region ƒ_ƒ[ƒWŒˆ’èˆ—
            float damageModifier = UnityEngine.Random.Range(0.85f, 1.0f);
            int ModifierDamage = Mathf.RoundToInt(damage * damageModifier);

            int reducedDamage = Mathf.CeilToInt(ModifierDamage * Mathf.Pow(GameRule.DamageIndexValue, Defense));
            #endregion

            currentHP -= reducedDamage;
            systemText.TextSet("Enemy" + damage + "Damage! HP:" + currentHP);
            if(R != 1)
            {
                int Rota = DirUtil.ReverseDirection(R);
                transform.rotation = Quaternion.Euler(0, Rota, 0);
            }

            if (currentHP <= 0 && EnemyDeath != null)
            {
                EnemyDeath();
            }
        }
    }
}