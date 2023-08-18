using UnityEngine;
using System.Collections;
using MoveSystem;
using AttackSystem;
using UISystemV2;

namespace PlayerV2
{
    public class PlayerControlV2 : MonoBehaviour
    {
        private MoveAction moveAction;
        private AttackAction attackAction;
        private PauseSystemV2 pauseSystem;

        float movex;
        float movez;

        void Start()
        {
            moveAction = GetComponent<MoveAction>();
            attackAction = GetComponent<AttackAction>();
            pauseSystem = FindObjectOfType<PauseSystemV2>();
        }

        public bool PlayerInput()
        {
            if(Input.GetKeyDown("x")) pauseSystem.PauseSwitching();
            
            StartCoroutine(WaitForTimeScale());

            bool TurnNext = false;
            movex = 0;
            movez = 0;

            if (Input.GetKeyDown(KeyCode.Z)/*||Input.GetButtonDown("Circle")*/)
            {
               attackAction.AttackStance();
                return true;
            }

            movex = Input.GetAxis("Horizontal");
            movez = Input.GetAxis("Vertical");

            if (Mathf.Abs(movex) > 0.3f) movex = Mathf.Sign(movex);
            else movex = 0;

            if (Mathf.Abs(movez) > 0.3f) movez = Mathf.Sign(movez);
            else movez = 0;

            if (movex == 0 && movez == 0) return false;

            TurnNext = moveAction.MoveStance(movex, movez);

            return TurnNext;
        }

        private IEnumerator WaitForTimeScale()
        {
            while (!Mathf.Approximately(Time.timeScale, 1.0f))
            {
                yield return null; // フレームの更新を待機
            }
        }

    }
}