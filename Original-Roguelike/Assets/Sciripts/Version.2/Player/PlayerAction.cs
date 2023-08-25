using AttackSystem;
using ItemSystemV2;
using System.Collections;
using System.Collections.Generic;
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

            // 行動が完了した後の処理
            PlayerToAttack = null;
            PlayerUseItemV2 = null;
        }
    }
}
