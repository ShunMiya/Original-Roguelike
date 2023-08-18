using System.Collections;
using UnityEngine;
using PlayerV2;
using MoveSystem;
using EnemySystem;
using PlayerStatusSystemV2;

namespace TurnSystem
{
    public class TurnControl : MonoBehaviour
    {
        public PlayerControlV2 PC;
        public MoveObjects MO;
        public EnemyObjects EO;
        public PlayerHungryV2 PH;

        void Start()
        {
            StartCoroutine(GameLoop());
        }

        private IEnumerator GameLoop()
        {
            while (true) // ゲームループを無限に続ける
            {
                yield return StartCoroutine(PlayerInputSet());

                EO.EnemiesActionSets();

                yield return StartCoroutine(MO.MoveAllObjects());

                PH.HungryDecrease();
                /*yield return new WaitForSeconds(1.0f); 
                Debug.Log("NextTurn");*/ //DebugSystem 

            }
        }

        private IEnumerator PlayerInputSet()
        {
            bool TurnNext = false;

            while (true)
            {
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