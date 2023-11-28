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
                    if (!AS.isPlaying)  AS.PlayOneShot(Selection);
                    break;
                case 1:
                    AS.PlayOneShot(Confirm);
                    break;
                case 2:
                    AS.PlayOneShot(Cancel);
                    break;
                case 9:
                    AS.PlayOneShot(OpenMenu);
                    break;
            }
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