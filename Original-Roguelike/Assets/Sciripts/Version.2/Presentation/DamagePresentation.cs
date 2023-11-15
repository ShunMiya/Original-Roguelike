using Field;
using System.Collections;
using UnityEngine;

namespace Presentation
{
    public class DamagePresentation : MonoBehaviour
    {
        [SerializeField]private ParticleSystem BlowEffect;
        [SerializeField]private ParticleSystem SlashEffect;
        [SerializeField]private ParticleSystem ThrustEffect;

        private ParticleSystem NowEffect;

        [SerializeField] private PresentationPosition presenPosition;

        public IEnumerator DamagePresen(int AttackType, int gridx, int gridz)
        {
            presenPosition.SetPosition(gridx, gridz);

            switch(AttackType)
            {
                case 0:
                    NowEffect =BlowEffect;
                    break;
                case 1:
                    NowEffect = SlashEffect;
                    break;
                case 2:
                    NowEffect = ThrustEffect;
                    break;
            }

            NowEffect.Play();
            yield return new WaitForEndOfFrame();
        }

        public IEnumerator WaitDamagePresen()
        {
            while(NowEffect.isPlaying)
            {
                yield return null;
            }
        }
    }
}