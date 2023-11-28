using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Performances
{
    public class Performance : MonoBehaviour
    {
        [SerializeField] private DamageEffects damageEffects;
        [SerializeField] private ActionSoundEffects actionSoundEffects;

        public IEnumerator DamagePerformance(int AttackType, int gridx, int gridz, AudioSource AS)
        {
            actionSoundEffects.DamageSE(AttackType, AS);
            damageEffects.DamageEffect(AttackType, gridx, gridz);

            yield return new WaitForEndOfFrame();
        }

    }

}