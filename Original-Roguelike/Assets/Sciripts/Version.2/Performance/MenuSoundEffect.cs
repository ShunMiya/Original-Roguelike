using System.Collections;
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
                    if (!AS.isPlaying) AS.PlayOneShot(Selection);
                    return;
                case 1:
                    AS.clip = Confirm;
                    AS.Play();
                    AS.time = 0.03f;
                    break;
                case 2:
                    AS.clip = Cancel;
                    AS.Play();
                    AS.time = 0.03f;
                    break;
                case 9:
                    AS.clip = OpenMenu;
                    AS.Play();
                    AS.time = 0.03f;
                    break;
            }
            StartCoroutine(StopSE(0.15f));
        }

        public IEnumerator StopSE(float delay)
        {
            yield return new WaitForSecondsRealtime(delay);
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