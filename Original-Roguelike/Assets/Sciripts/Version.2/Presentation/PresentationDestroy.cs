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
            // ParticleSystem �̍Đ����I�����Ă����� GameObject ��j��
            if (particleSystem != null && !particleSystem.isPlaying)
            {
                Destroy(gameObject);
            }
        }
    }
}