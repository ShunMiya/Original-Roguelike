using System.Collections;
using UnityEngine;

namespace Performances
{
    public class Performance : MonoBehaviour
    {
        [SerializeField] private AudioSource PlayerAS;

        [SerializeField] private DamageEffects damageEffects;
        [SerializeField] private ActionSoundEffects actionSoundEffects;
        [SerializeField] private FieldSoundEffect fieldSoundEffects;

        public IEnumerator DamagePerformance(int AttackType, int gridx, int gridz, AudioSource AS)
        {
            actionSoundEffects.DamageSE(AttackType, AS);
            damageEffects.DamageEffect(AttackType, gridx, gridz);

            yield return new WaitForEndOfFrame();
        }

        public IEnumerator GimmickPerformance(int GimmickType, int gridx, int gridz)
        {
            fieldSoundEffects.GimmickSE(GimmickType);

            //�G�t�F�N�g��SE���I���܂őҋ@����K�v����
            while (PlayerAS.isPlaying)
            {
                yield return null;
            }
        }
    }

}