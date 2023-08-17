using System.Collections;
using UnityEngine;
using PlayerV2;
using MoveSystem;

namespace TurnSystem
{
    public class TurnControl : MonoBehaviour
    {
        public PlayerControlV2 PC;
        public MoveObjects MO;

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(GameLoop());
        }

        private IEnumerator GameLoop()
        {
            while (true) // �Q�[�����[�v�𖳌��ɑ�����
            {
                yield return StartCoroutine(PlayerInputSet());

                yield return StartCoroutine(MO.MoveAllObjects());

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

                // ����̓��͂����邩�ǂ������m�F���鏈��
                if (TurnNext)
                {
                    break;
                }

                yield return null; // �t���[���̍X�V��ҋ@
            }
        }
    }
}