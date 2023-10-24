using UnityEngine;
using MoveSystem;
using AttackSystem;
using UISystemV2;
using PlayerStatusSystemV2;

namespace PlayerV2
{
    public class PlayerControlV2 : MonoBehaviour
    {
        private MoveAction moveAction;
        private AttackAction attackAction;
        private PlayerCondition PCondition;
        private PauseSystemV2 pauseSystem;

        private float WaitInput = 0.05f;
        private float WaitInputTimer = 0f;
        private bool firstInputProcessed = false;

        float movex;
        float movez;

        void Start()
        {
            moveAction = GetComponent<MoveAction>();
            attackAction = GetComponent<AttackAction>();
            PCondition = GetComponent<PlayerCondition>();
            pauseSystem = FindObjectOfType<PauseSystemV2>();
        }

        public bool PlayerInput()
        {
            if (Input.GetKeyDown("a")) pauseSystem.PauseSwitching();

            if (Input.GetKey(KeyCode.X)) GameRule.DashMove();
            else GameRule.WalkMove();

            bool TurnNext = false;
            movex = 0;
            movez = 0;

            if (Input.GetKey(KeyCode.Z)/*||Input.GetButtonDown("Circle")*/)
            {
                PlayerAction PA = GetComponent<PlayerAction>();
                PA.PlayerToAttack =attackAction;

                if (PCondition.ConfusionTurn != 0)
                {
                    Vector3 PosRota = PCondition.ConfusionEvent();
                    transform.rotation = Quaternion.Euler(0, PosRota.y, 0);
                }

                return true;
            }

            if(Input.GetKey(KeyCode.C))
            {
                movex = Input.GetAxis("Horizontal");
                movez = Input.GetAxis("Vertical");

                if (Mathf.Abs(movex) > 0.2f) movex = Mathf.Sign(movex);
                else movex = 0;

                if (Mathf.Abs(movez) > 0.2f) movez = Mathf.Sign(movez);
                else movez = 0;

                moveAction.ChangeDirectionOnTheSpot(movex, movez);

                return false;
            }

            if (!firstInputProcessed)
            {
                if (WaitInputTimer > 0)
                {
                    WaitInputTimer -= Time.deltaTime;
                }
                else
                {
                    movex = Input.GetAxis("Horizontal");
                    movez = Input.GetAxis("Vertical");

                    if (Mathf.Abs(movex) > 0.2f || Mathf.Abs(movez) > 0.2f)
                    {
                        firstInputProcessed = true;
                    }
                    else
                    {
                        WaitInputTimer = WaitInput;
                    }
                }
            }
            else
            {
                movex = Input.GetAxis("Horizontal");
                movez = Input.GetAxis("Vertical");

                if (Mathf.Abs(movex) > 0.2f) movex = Mathf.Sign(movex);
                else movex = 0;

                if (Mathf.Abs(movez) > 0.2f) movez = Mathf.Sign(movez);
                else movez = 0;

                if (movex == 0 && movez == 0)
                {
                    WaitInputTimer = 0f;
                    firstInputProcessed = false;
                    return false;
                }

                if (PCondition.ConfusionTurn != 0)
                {
                    Vector3 PosRota = PCondition.ConfusionEvent();
                    movex = PosRota.x; movez = PosRota.z;
                }

                TurnNext = moveAction.MoveStance(movex, movez);
            }
            return TurnNext;
        }
    }
}