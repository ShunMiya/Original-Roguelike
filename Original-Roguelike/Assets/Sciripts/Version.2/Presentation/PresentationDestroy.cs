using UnityEngine;

namespace Presentation
{
    public class PresentationDestroy : MonoBehaviour
    {
        private new ParticleSystem particleSystem;

        void Start()
        {
            particleSystem = GetComponent<ParticleSystem>();
        }

        void Update()
        {
            // ParticleSystem ‚ÌÄ¶‚ªI—¹‚µ‚Ä‚¢‚½‚ç GameObject ‚ğ”jŠü
            if (particleSystem != null && !particleSystem.isPlaying)
            {
                Destroy(gameObject);
            }
        }
    }
}