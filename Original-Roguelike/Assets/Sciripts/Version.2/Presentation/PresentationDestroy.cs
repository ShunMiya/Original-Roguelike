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
            // ParticleSystem の再生が終了していたら GameObject を破棄
            if (particleSystem != null && !particleSystem.isPlaying)
            {
                Destroy(gameObject);
            }
        }
    }
}