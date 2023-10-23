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
        public PlayerThrowItem playerThrowItem;

        public IEnumerator ActionStart()
        {
            if (PlayerToAttack != null)
            {
                Coroutine coroutine = StartCoroutine(PlayerToAttack.AttackPreparationPlayer());
                yield return coroutine;
            }
            PlayerToAttack = null;

            if (playerUseItemV2 != null)
            {
                Coroutine coroutine = StartCoroutine(playerUseItemV2.UseItem());
                yield return coroutine;
            }
            playerUseItemV2 = null;

            if (playerPutItem != null)
            {
                Coroutine coroutine = StartCoroutine(playerPutItem.PutItem());
                yield return coroutine;
            }
            playerPutItem = null;

            if (playerThrowItem  != null)
            {
                Coroutine coroutine = StartCoroutine(playerThrowItem.ThrowItem());
                yield return coroutine;
            }
            playerThrowItem = null;
        }
    }
}
