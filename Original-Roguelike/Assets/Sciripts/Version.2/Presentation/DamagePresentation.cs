using Field;
using System.Collections;
using UnityEngine;

namespace Presentation
{
    public class DamagePresentation : MonoBehaviour
    {
        [SerializeField] private GameObject BlowEffectObj;
        [SerializeField]private GameObject SlashEffectObj;
        [SerializeField]private GameObject ThrustEffectObj;

        public IEnumerator DamagePresen(int AttackType, int gridx, int gridz)
        {

            switch(AttackType)
            {
                case 0:
                    GameObject effect = Instantiate(BlowEffectObj, gameObject.transform);
                    effect.GetComponent<PresentationPosition>().SetPosition(gridx, gridz);
                    effect.GetComponent<ParticleSystem>().Play();

                    break;
                case 1:
                    effect = Instantiate(SlashEffectObj, gameObject.transform);
                    effect.GetComponent<PresentationPosition>().SetPosition(gridx, gridz);
                    effect.GetComponent<ParticleSystem>().Play(); break;
                case 2:
                    effect = Instantiate(ThrustEffectObj, gameObject.transform);
                    effect.GetComponent<PresentationPosition>().SetPosition(gridx, gridz);
                    effect.GetComponent<ParticleSystem>().Play(); break;
            }

            yield return new WaitForEndOfFrame();
        }
    }
}