using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Performances
{
    public class FieldSoundEffect : MonoBehaviour
    {
        [SerializeField] private AudioSource AS;

        [SerializeField] private AudioClip Warp;
        [SerializeField] private AudioClip FallingRock;
        [SerializeField] private AudioClip Hungry;
        [SerializeField] private AudioClip Poison;
        [SerializeField] private AudioClip Confu;
        [SerializeField] private AudioClip Stun;
        [SerializeField] private AudioClip Blind;

        [SerializeField] private float GimmickVolume;
        [SerializeField] private float SESpeed;

        public void GimmickSE(int SEType)
        {
            AS.volume = GimmickVolume;
            AS.pitch = SESpeed;

            switch (SEType)
            {
                case 0:
                    AS.pitch = 1f;
                    AS.PlayOneShot(Warp);
                    break;
                case 1:
                    AS.PlayOneShot(FallingRock);
                    break;
                case 2:
                    AS.PlayOneShot(Hungry);
                    break;
                case 3:
                    AS.PlayOneShot(Poison);
                    break;
                case 4:
                    AS.PlayOneShot(Confu);
                    break;
                case 5:
                    AS.PlayOneShot(Stun);
                    break;
                case 6:
                    AS.PlayOneShot(Blind);
                    break;
            }
        }
    }
}