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
            while (true) // �Q�[�����[�v�𖳌��ɑ�����
            {
                yield return StartCoroutine(PlayerInputSet());�@//�v���C���[�̍s������

                yield return StartCoroutine(PA.ActionStart());�@//�v���C���[�̃A�N�V�������{(�U���A�A�C�e���֘A)

                EO.EnemiesActionSets();�@//�G�l�~�[�̍s������

                yield return StartCoroutine(MO.MoveAllObjects());�@//�S�I�u�W�F�N�g�̓����ړ�

                yield return StartCoroutine(PEAM.EventCheck()); //�ړ���ł̊e�폈��(�A�C�e���Q�b�g�A㩔����A�K�w�ړ��I��)

                yield return StartCoroutine(AO.AttackAllObject());�@//�G�l�~�[�̃A�N�V�������{(�U���A����s��)

                PH.HungryDecrease();�@//�^�[����(�󕠒l�����AHP�񕜁A��Ԉُ폈���A�^�[�����L����)

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

                yield return null; // �t���[���̍X�V��ҋ@
            }
        }
    }
}