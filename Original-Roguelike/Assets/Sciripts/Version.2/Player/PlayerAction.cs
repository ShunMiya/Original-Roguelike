using AttackSystem;
using ItemSystemV2;
using System.Collections;
using UnityEngine;

namespace PlayerV2
{
    public class PlayerAction : MonoBehaviour
    {
        public AttackAction PlayerToAttack;
        public PlayerUseItemV2 PlayerUseItemV2;

        public IEnumerator ActionStart()
        {
            if (PlayerToAttack != null)
            {
                Coroutine coroutine = StartCoroutine(PlayerToAttack.AttackPreparationPlayer());
                yield return coroutine;
            }
            if(PlayerUseItemV2 != null)
            {
                Coroutine coroutine = StartCoroutine(PlayerUseItemV2.UseItem());
                yield return coroutine;
            }

            // çsìÆÇ™äÆóπÇµÇΩå„ÇÃèàóù
            PlayerToAttack = null;
            PlayerUseItemV2 = null;
        }
    }
}
