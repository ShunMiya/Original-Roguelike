using AttackSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerV2
{
    public class PlayerAction : MonoBehaviour
    {
        public AttackAction PlayerToAttack;

        public IEnumerator ActionStart()
        {
            if (PlayerToAttack != null)
            {
                Coroutine coroutine = StartCoroutine(PlayerToAttack.AttackPreparationPlayer());
                yield return coroutine;
            }

            // s“®‚ªŠ®—¹‚µ‚½Œã‚Ìˆ—
            PlayerToAttack = null;
        }
    }
}
