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
            while (true) // �Q�[�����[�v�𖳌��ɑ�����
            {
                field.MappingUpdate();

                yield return StartCoroutine(PlayerInputSet());�@//�v���C���[�̍s������

                yield return StartCoroutine(PA.ActionStart());�@//�v���C���[�̃A�N�V�������{(�U���A�A�C�e���֘A)

                EO.EnemiesActionSets();�@//�G�l�~�[�̍s������

                yield return StartCoroutine(MO.MoveAllObjects()); //�S�I�u�W�F�N�g�̓����ړ�

                field.MappingUpdate();

                yield return StartCoroutine(PEAM.EventCheck()); //�ړ���ł̊e�폈��(�A�C�e���Q�b�g�A㩔����A�K�w�ړ��I��)

                field.MappingUpdate();

                yield return StartCoroutine(AO.AttackAllObject());�@//�G�l�~�[�̃A�N�V�������{(�U���A����s��)

                if(FadeImage.activeSelf)
                {
                    AreaTurn = 0; DungeonTurn+=AreaTurn;
                    PCondition.ConditionClear();
                    yield return StartCoroutine(StaticCoroutine.ObjectActiveFalse(FadeImage));
                    continue;
                }

                //�^�[����(�Ń_���[�W�A�󕠒l�����AHP�񕜁A��Ԉُ�^�[���o�߁A�^�[�����L����)
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