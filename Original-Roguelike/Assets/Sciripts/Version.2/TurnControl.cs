using System.Collections;
using UnityEngine;
using PlayerV2;
using MoveSystem;
using AttackSystem;
using EnemySystem;
using PlayerStatusSystemV2;

namespace TurnSystem
{
    public class TurnControl : MonoBehaviour
    {
        public PlayerControlV2 PC;
        public MoveObjects MO;
        public EnemyObjects EO;
        public AttackObjects AO;
        public PlayerHungryV2 PH;
        public PlayerAction PA;
        public PlayerEventAfterMove PEAM;

        void Start()
        {
            StartCoroutine(GameLoop());
        }

        private IEnumerator GameLoop()
        {
            while (true) // ゲームループを無限に続ける
            {
                yield return StartCoroutine(PlayerInputSet());　//プレイヤーの行動入力

                yield return StartCoroutine(PA.ActionStart());　//プレイヤーのアクション実施(攻撃、アイテム関連)

                EO.EnemiesActionSets();　//エネミーの行動決定

                yield return StartCoroutine(MO.MoveAllObjects());　//全オブジェクトの同時移動

                yield return StartCoroutine(PEAM.EventCheck()); //移動先での各種処理(アイテムゲット、罠発動、階層移動選択)

                yield return StartCoroutine(AO.AttackAllObject());　//エネミーのアクション実施(攻撃、特殊行動)

                PH.HungryDecrease();　//ターン回し(空腹値減少、HP回復、状態異常処理、ターン数記憶等)

                /*Debug.Log("NextTurn"); 
                yield return new WaitForSeconds(0.5f);*/ //DebugSystem 
            }
        }

        private IEnumerator PlayerInputSet()
        {
            bool TurnNext = false;

            while (true)
            {
                if (Time.timeScale != 1f)continue;

                if (PA.PlayerUseItemV2 != null)
                {
                    break;
                }

                TurnNext = PC.PlayerInput();

                if (TurnNext)
                {
                    break;
                }

                yield return null; // フレームの更新を待機
            }
        }
    }
}