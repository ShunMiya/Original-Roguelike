using System.Collections;
using UnityEngine;
using PlayerV2;
using MoveSystem;
using AttackSystem;
using EnemySystem;
using PlayerStatusSystemV2;
using Field;
using GameEndSystemV2;
using UISystemV2;

namespace TurnSystem
{
    public class TurnControl : MonoBehaviour
    {
        private int DungeonTurn = 0;
        private int AreaTurn = 0;
        [SerializeField] private PlayerControl PC;
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
        [SerializeField] private SystemTextV2 systemtext;
        [SerializeField] private DungeonFloorBar DFB;
        [SerializeField] private ADVTrigeerCheck ADVTC;

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
                StartCoroutine(field.MappingUpdate());

                if (ADVTC.TriggerCheck()) yield return StartCoroutine(ADVTC.EventStart());　//ADVパート処理

                yield return StartCoroutine(PlayerInputSet()); //プレイヤーの行動入力　１フレーム経過

                if(PA.ActionCheck()) yield return StartCoroutine(PA.ActionStart()); //プレイヤーのアクション実施(攻撃、アイテム関連)

                EO.EnemiesActionSets(); //エネミーの行動決定

                yield return StartCoroutine(MO.MoveAllObjects()); //全オブジェクトの同時移動

                StartCoroutine(field.MappingUpdate());

                if(PEAM.EventCheck()) yield return StartCoroutine(PEAM.EventStart()); //移動先での各種処理(アイテムゲット、罠発動、階層移動選択)

                if(AO.AttackObjCheck()) yield return StartCoroutine(AO.AttackAllObject()); //エネミーのアクション実施(攻撃、特殊行動)

                if (FadeImage.activeSelf)
                {
                    NextAreaProcess();
                    yield return StartCoroutine(StaticCoroutine.ObjectActiveFalse(FadeImage));
                    continue;
                }

                //ターン回し(毒ダメージ、空腹値減少、HP回復、状態異常ターン経過、ターン数記憶等)
                StartCoroutine(TurnProgression());
                AreaTurn++;

                if (AreaTurn == 500)
                {
                    systemtext.TextSet("突風が吹いてきた！　先へ進もう！");
                    yield return new WaitForSeconds(1.0f);

                    gameEnd.NextStagePerformance();
                    NextAreaProcess();
                    yield return StartCoroutine(StaticCoroutine.ObjectActiveFalse(FadeImage));
                    continue;
                }

                if(AreaTurn % 40 == 0) field.PopEnemy();
            }
        }

        private IEnumerator PlayerInputSet()
        {            
            while (true)
            {
                if (Time.timeScale != 1f)
                {
                    yield return null;
                    continue;
                }

                if (PCondition.StunTurn != 0)
                {
                    yield return StartCoroutine(PCondition.StunEvent());
                    break;
                }
                if (PA.playerUseItemV2 != null) yield break;
                if(PA.playerPutItem != null) yield break;
                if (PA.playerThrowItem != null) yield break;

                bool TurnNext = PC.PlayerInput();
                if (TurnNext) yield break;

                yield return null;
            }
        }

        private IEnumerator TurnProgression()
        {
            if (PCondition.PoisonTurn != 0) PCondition.PoisonEvent();
            PH.HungryDecrease();
            HP.TurnRecoveryHp();
            PCondition.ConditionTurn();
            yield return null;
        }

        public void NextAreaProcess()
        {
            AreaTurn = 0; DungeonTurn += AreaTurn;
            PCondition.ConditionClear();
            DFB.UpdateFloorBar();
        }
    }
}