using UnityEngine;

namespace Performances
{
    public class MenuSoundEffect : MonoBehaviour
    {
        [SerializeField] private AudioSource AS;

        [SerializeField] private AudioClip OpenMenu;
        [SerializeField] private AudioClip ComsumableItem;
        [SerializeField] private AudioClip EquipItem;
        [SerializeField] private AudioClip Confirm;
        [SerializeField] private AudioClip Cancel;
        [SerializeField] private AudioClip Selection;

        [SerializeField] private float MenuVolume = 0.2f;
        [SerializeField] private float MenuActionVolume = 0.5f;

        public void MenuOperationSE(int SEType)
        {
            AS.volume = MenuVolume;

            switch (SEType)
            {
                case 0:
                    if (!AS.isPlaying || AS.clip == Selection) AS.PlayOneShot(Selection);
                    break;
                case 1:
                    AS.PlayOneShot(Confirm);
                    AS.time = 0.1f;
                    break;
                case 2:
                    AS.PlayOneShot(Cancel);
                    AS.time = 0.1f;
                    break;
                case 9:
                    AS.PlayOneShot(OpenMenu);
                    AS.time = 0.1f;
                    break;
            }
            Invoke("StopSound", 0.3f);
        }

        void StopSound()
        {
            AS.Stop();
        }

        public void MenuActionSE(int SEType)
        {
            AS.volume = MenuActionVolume;

            switch (SEType)
            {
                case 0:
                    AS.PlayOneShot(ComsumableItem);
                    break;
                case 1:
                    AS.PlayOneShot(EquipItem);
                    break;
            }
        }
    }
}