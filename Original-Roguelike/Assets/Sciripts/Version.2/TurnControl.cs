using System.Collections;
using UnityEngine;
using PlayerV2;
using MoveSystem;
using AttackSystem;
using EnemySystem;
using PlayerStatusSystemV2;
using UISystemV2;
using Field;
using GameEndSystemV2;

namespace TurnSystem
{
    public class TurnControl : MonoBehaviour
    {
        private int DungeonTurn = 0;
        private int AreaTurn = 0;
        [SerializeField] private PlayerControlV2 PC;
        [SerializeField] private MoveObjects MO;
        [SerializeField] private EnemyObjects EO;
        [SerializeField] private AttackObjects AO;
        [SerializeField] private PlayerHungryV2 PH;
        [SerializeField] private PlayerHPV2 HP;
        [SerializeField] private PlayerAction PA;
        [SerializeField] private PlayerEventAfterMove PEAM;
        [SerializeField] private Areamap field;
        [SerializeField] private GameEndV2 gameEnd;
        [SerializeField] private PlayerCondition PCondition;

        [SerializeField] private GameObject FadeImage;

        void Start()
        {
            StartCoroutine(DungeonStart());
        }

        private IEnumerator DungeonStart()
        {
            yield return StartCoroutine(StaticCoroutine.ObjectActiveFalse(FadeImage));
            StartCoroutine(GameLoop());
        }
        private IEnumerator GameLoop()
        {
            while (true) // ゲームループを無限に続ける
            {
                field.MappingUpdate();

                yield return StartCoroutine(PlayerInputSet());　//プレイヤーの行動入力

                yield return StartCoroutine(PA.ActionStart());　//プレイヤーのアクション実施(攻撃、アイテム関連)

                EO.EnemiesActionSets();　//エネミーの行動決定

                yield return StartCoroutine(MO.MoveAllObjects()); //全オブジェクトの同時移動

                field.MappingUpdate();

                yield return StartCoroutine(PEAM.EventCheck()); //移動先での各種処理(アイテムゲット、罠発動、階層移動選択)

                field.MappingUpdate();

                yield return StartCoroutine(AO.AttackAllObject());　//エネミーのアクション実施(攻撃、特殊行動)

                if(FadeImage.activeSelf)
                {
                    AreaTurn = 0; DungeonTurn+=AreaTurn;
                    PCondition.ConditionClear();
                    yield return StartCoroutine(StaticCoroutine.ObjectActiveFalse(FadeImage));
                    continue;
                }

                //ターン回し(毒ダメージ、空腹値減少、HP回復、状態異常ターン経過、ターン数記憶等)
                if (PCondition.PoisonTurn != 0) PCondition.PoisonEvent();
                PH.HungryDecrease();
                HP.TurnRecoveryHp();
                PCondition.ConditionTurn();
                AreaTurn++;

                if (AreaTurn == 500)
                {
                    gameEnd.NextStagePerformance();
                    AreaTurn = 0; DungeonTurn += AreaTurn;
                    yield return StartCoroutine(StaticCoroutine.ObjectActiveFalse(FadeImage));
                    continue;
                }

                if (AreaTurn % 40 == 0)
                {
                    field.PopEnemy();
                }

                /*Debug.Log("NextTurn"); 
                yield return new WaitForSeconds(0.5f);*/ //DebugSystem 
            }
        }

        private IEnumerator PlayerInputSet()
        {
            bool TurnNext = false;
            
            while (true)
            {
                if (Time.timeScale != 1f)
                {
                    yield return null;
                    continue;
                }

                if (PCondition.StunTurn != 0)
                {
                    PCondition.StunEvent();
                    break;
                }
                if (PA.playerUseItemV2 != null) break;
                if(PA.playerPutItem != null) break;
                if (PA.playerThrowItem != null) break;

                TurnNext = PC.PlayerInput();

                if (TurnNext)
                {
                    break;
                }

                yield return null;
            }
        }
    }
}