using System.Collections;
using UnityEngine;

namespace Performances
{
    public class ActionSoundEffects : MonoBehaviour
    {
        [SerializeField] private AudioClip BlowAttack;
        [SerializeField] private AudioClip BlowHit;
        [SerializeField] private AudioClip SlashAttack;
        [SerializeField] private AudioClip SlashHit;
        [SerializeField] private AudioClip ThrustAttack;
        [SerializeField] private AudioClip ThrustHit;
        [SerializeField] private AudioClip ThrowAciton;
        [SerializeField] private AudioClip AttackMiss;
        [SerializeField] private AudioClip EnemyDeath;
        [SerializeField] private AudioClip LevelUp;

        [SerializeField] private float AttackVolume;
        [SerializeField] private float HitVolume;

        public IEnumerator AttackSE(int AttackType, AudioSource AS)
        {
            AS.volume = AttackVolume;
            switch (AttackType)
            {
                case 0:
                    AS.PlayOneShot(BlowAttack);
                    break;
                case 1:
                    AS.PlayOneShot(SlashAttack);
                    break;
                case 2:
                    AS.PlayOneShot(ThrustAttack);
                    break;
                case 3:
                    AS.PlayOneShot(ThrowAciton);
                    break;
            }

            yield return new WaitForSeconds(0.1f);
        }

        public void DamageSE(int SEType, AudioSource AS)
        {
            AS.volume = HitVolume;

            switch (SEType)
            {
                case 0:
                    AS.PlayOneShot(BlowHit);
                    break;
                case 1:
                    AS.PlayOneShot(SlashHit);
                    break;
                case 2:
                    AS.PlayOneShot(ThrustHit);
                    break;
                case 3:
                    AS.PlayOneShot(AttackMiss);
                    break;
            }
        }

        public void EndBattleSE(int SEType, AudioSource AS)
        {
            switch (SEType)
            {
                case 0:
                    AS.volume = 0.3f;
                    AS.PlayOneShot(EnemyDeath);
                    break;
                case 1:
                    AS.volume = 1f;
                    AS.PlayOneShot(LevelUp);
                    break;
            }
        }
    }
}