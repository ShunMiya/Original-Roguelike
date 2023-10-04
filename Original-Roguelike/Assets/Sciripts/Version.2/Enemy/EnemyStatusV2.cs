using PlayerStatusSystemV2;
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
        private int Exp;

        private void Start()
        {
            systemText = FindObjectOfType<SystemTextV2>();
            EnemyDataV2 enemy = EnemyDataCacheV2.GetEnemyData(EnemyID);
            currentHP = enemy.MaxHP;
            Defense = enemy.Defense;
            Exp = enemy.EnemyExp;

        }
        public void TakeDamage(float damage,int R, float HitRate, GameObject attacker)
        {
            #region ñΩíÜó¶èàóù
            int HitCheck = Random.Range(1,101);
            if(HitCheck > HitRate)
            {
                systemText.TextSet("NoHit!");
                return;
            }
            #endregion

            #region âÒîó¶èàóù
            int EvasionCheck = Random.Range(1, 101);
            if (EvasionCheck < GameRule.EvasionRate)
            {
                systemText.TextSet("NoHit!");
                return;
            }
            #endregion

            #region É_ÉÅÅ[ÉWåàíËèàóù
            float damageModifier = Random.Range(0.85f, 1.0f);
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
                systemText.TextSet("EnemyDead!");
                if (attacker.CompareTag("Player"))
                {
                    FindFirstObjectByType<PlayerLevel>().PlayerGetExp(Exp);
                }
                EnemyDeath();
            }
        }
    }
}