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
            while (true) // �Q�[�����[�v�𖳌��ɑ�����
            {
                StartCoroutine(field.MappingUpdate());

                if (ADVTC.TriggerCheck()) yield return StartCoroutine(ADVTC.EventStart());�@//ADV�p�[�g����

                yield return StartCoroutine(PlayerInputSet()); //�v���C���[�̍s�����́@�P�t���[���o��

                if(PA.ActionCheck()) yield return StartCoroutine(PA.ActionStart()); //�v���C���[�̃A�N�V�������{(�U���A�A�C�e���֘A)

                EO.EnemiesActionSets(); //�G�l�~�[�̍s������

                yield return StartCoroutine(MO.MoveAllObjects()); //�S�I�u�W�F�N�g�̓����ړ�

                StartCoroutine(field.MappingUpdate());

                if(PEAM.EventCheck()) yield return StartCoroutine(PEAM.EventStart()); //�ړ���ł̊e�폈��(�A�C�e���Q�b�g�A㩔����A�K�w�ړ��I��)

                if(AO.AttackObjCheck()) yield return StartCoroutine(AO.AttackAllObject()); //�G�l�~�[�̃A�N�V�������{(�U���A����s��)

                if (FadeImage.activeSelf)
                {
                    NextAreaProcess();
                    yield return StartCoroutine(StaticCoroutine.ObjectActiveFalse(FadeImage));
                    continue;
                }

                //�^�[����(�Ń_���[�W�A�󕠒l�����AHP�񕜁A��Ԉُ�^�[���o�߁A�^�[�����L����)
                StartCoroutine(TurnProgression());
                AreaTurn++;

                if (AreaTurn == 500)
                {
                    systemtext.TextSet("�˕��������Ă����I�@��֐i�����I");
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