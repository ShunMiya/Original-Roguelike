using DeathSystem;
using MoveSystem;
using UISystemV2;
using UnityEngine;
using Performances;

namespace EnemySystem
{
    public class EnemyStatusV2 : MonoBehaviour
    {
        private SystemTextV2 systemText;
        private Performance performance;
        private ActionSoundEffects actionSoundEffects;
        private AudioSource audioSource;

        public float currentHP;
        public int EnemyID;
        private int Defense;
        private int Exp;

        private void Start()
        {
            performance = FindObjectOfType<Performance>();
            systemText = FindObjectOfType<SystemTextV2>();
            EnemyDataV2 enemy = EnemyDataCacheV2.GetEnemyData(EnemyID);
            name = enemy.EnemyName;
            currentHP = enemy.MaxHP;
            Defense = enemy.Defense;
            Exp = enemy.EnemyExp;
            actionSoundEffects = FindObjectOfType<ActionSoundEffects>();
            audioSource = GetComponent<AudioSource>();
        }
        public void TakeDamage(float damage,int R, float HitRate, GameObject attacker, int AttackType)
        {
            #region 命中率処理
            int HitCheck = Random.Range(1,101);
            if(HitCheck > HitRate)
            {
                actionSoundEffects.DamageSE(3, audioSource);
                return;
            }
            #endregion

            #region 回避率処理
            int EvasionCheck = Random.Range(1, 101);
            if (EvasionCheck < GameRule.EvasionRate)
            {
                actionSoundEffects.DamageSE(3, audioSource);
                return;
            }
            #endregion

            #region ダメージ決定処理
            float damageModifier = Random.Range(0.85f, 1.0f);
            float ModifierDamage = damage * damageModifier;
            int reducedDamage = Mathf.CeilToInt(ModifierDamage * Mathf.Pow(GameRule.DamageIndexValue, Defense));
            #endregion

            if (reducedDamage == 0) reducedDamage++;

            currentHP -= reducedDamage;

            systemText.TextSet("<color=red>" +name + "</color>は" + reducedDamage + "ダメージを受けた！");
            if(R != 1)
            {
                int Rota = DirUtil.ReverseDirection(R);
                transform.rotation = Quaternion.Euler(0, Rota, 0);
            }

            Pos2D grid = GetComponent<MoveAction>().grid;
            AudioSource AS = GetComponent<AudioSource>();
            StartCoroutine(performance.DamagePerformance(AttackType, grid.x, grid.z, AS));


            if (attacker.CompareTag("Player"))
            {
                GetComponent<EnemyAction>().EscapeCountPlus();
            }
            
            if (currentHP <= 0) GetComponent<DeathAction>().DeathSet(attacker, Exp);
        }

        public void DirectDamage(float damage, int R, float HitRate, GameObject attacker, int AttackType)
        {
            #region 命中率処理
            int HitCheck = Random.Range(1, 101);
            if (HitCheck > HitRate)
            {
                actionSoundEffects.DamageSE(3, audioSource);
                return;
            }
            #endregion

            #region 回避率処理
            int EvasionCheck = Random.Range(1, 101);
            if (EvasionCheck < GameRule.EvasionRate)
            {
                actionSoundEffects.DamageSE(3, audioSource);
                return;
            }
            #endregion

            currentHP -= damage;
            systemText.TextSet("<color=red>" + name + "</color>は" + damage + "ダメージを受けた！");
            if (R != 1)
            {
                int Rota = DirUtil.ReverseDirection(R);
                transform.rotation = Quaternion.Euler(0, Rota, 0);
            }

            Pos2D grid = GetComponent<MoveAction>().grid;
            AudioSource AS = GetComponent<AudioSource>();
            StartCoroutine(performance.DamagePerformance(AttackType, grid.x, grid.z, AS));

            if (currentHP <= 0) GetComponent<DeathAction>().DeathSet(attacker, Exp);
        }
    }
}