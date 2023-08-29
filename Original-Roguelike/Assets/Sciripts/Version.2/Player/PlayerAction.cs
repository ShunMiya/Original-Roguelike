using AttackSystem;
using ItemSystemV2;
using System.Collections;
using UnityEngine;

namespace PlayerV2
{
    public class PlayerAction : MonoBehaviour
    {
        public AttackAction PlayerToAttack;
        public PlayerUseItemV2 playerUseItemV2;
        public PlayerPutItem playerPutItem;

        public IEnumerator ActionStart()
        {
            if (PlayerToAttack != null)
            {
                Coroutine coroutine = StartCoroutine(PlayerToAttack.AttackPreparationPlayer());
                yield return coroutine;
            }
            if(playerUseItemV2 != null)
            {
                Coroutine coroutine = StartCoroutine(playerUseItemV2.UseItem());
                yield return coroutine;
            }
            if (playerPutItem != null)
            {
                Coroutine coroutine = StartCoroutine(playerPutItem.PutItem());
                yield return coroutine;
            }

            // çsìÆÇ™äÆóπÇµÇΩå„ÇÃèàóù
            PlayerToAttack = null;
            playerUseItemV2 = null;
            playerPutItem = null;
        }
    }
}
