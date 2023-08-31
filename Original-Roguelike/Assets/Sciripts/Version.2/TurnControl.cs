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
        [SerializeField] private PlayerControlV2 PC;
        [SerializeField] private MoveObjects MO;
        [SerializeField] private EnemyObjects EO;
        [SerializeField] private AttackObjects AO;
        [SerializeField] private PlayerHungryV2 PH;
        [SerializeField] private PlayerHPV2 HP;
        [SerializeField] private PlayerAction PA;
        [SerializeField] private PlayerEventAfterMove PEAM;

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
                yield return StartCoroutine(PlayerInputSet());�@//�v���C���[�̍s������

                yield return StartCoroutine(PA.ActionStart());�@//�v���C���[�̃A�N�V�������{(�U���A�A�C�e���֘A)

                EO.EnemiesActionSets();�@//�G�l�~�[�̍s������

                yield return StartCoroutine(MO.MoveAllObjects());�@//�S�I�u�W�F�N�g�̓����ړ�

                yield return StartCoroutine(PEAM.EventCheck()); //�ړ���ł̊e�폈��(�A�C�e���Q�b�g�A㩔����A�K�w�ړ��I��)

                yield return StartCoroutine(AO.AttackAllObject());�@//�G�l�~�[�̃A�N�V�������{(�U���A����s��)

                PH.HungryDecrease();�@//�^�[����(�󕠒l�����AHP�񕜁A��Ԉُ폈���A�^�[�����L����)
                HP.TurnRecoveryHp();

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
                    yield return null; // �t���[���̍X�V��ҋ@
                    continue;
                }

                if (PA.playerUseItemV2 != null) break;
                if(PA.playerPutItem != null) break;
                if (PA.playerThrowItem != null) break;

                TurnNext = PC.PlayerInput();

                if (TurnNext)
                {
                    break;
                }

                yield return null; // �t���[���̍X�V��ҋ@
            }
        }
    }
}