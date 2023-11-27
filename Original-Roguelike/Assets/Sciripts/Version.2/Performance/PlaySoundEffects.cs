using System.Collections;
using UnityEngine;

namespace Performances
{
    public class PlaySoundEffects : MonoBehaviour
    {
        [SerializeField] private AudioClip BlowAttack;
        [SerializeField] private AudioClip BlowHit;
        [SerializeField] private AudioClip SlashAttack;
        [SerializeField] private AudioClip SlashHit;
        [SerializeField] private AudioClip ThrustAttack;
        [SerializeField] private AudioClip ThrustHit;
        [SerializeField] private AudioClip ThrowAciton;
        [SerializeField] private AudioClip AttackMiss;
        [SerializeField] private AudioClip UseComsumableItem;

        public IEnumerator AttackSE(int AttackType, AudioSource AS)
        {
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

        public void DamageSE(int AttackType, AudioSource AS)
        {
            switch (AttackType)
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
    }
}