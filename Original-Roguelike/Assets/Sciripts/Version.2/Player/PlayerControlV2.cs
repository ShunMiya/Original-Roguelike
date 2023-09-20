using UnityEngine;
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
            if (Input.GetKeyDown("a")) pauseSystem.PauseSwitching();

            bool TurnNext = false;
            movex = 0;
            movez = 0;

            if (Input.GetKey(KeyCode.Z)/*||Input.GetButtonDown("Circle")*/)
            {
                PlayerAction PA = GetComponent<PlayerAction>();
                PA.PlayerToAttack =attackAction;
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
    }
}