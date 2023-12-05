using UnityEngine;
using MoveSystem;
using AttackSystem;
using UISystemV2;
using PlayerStatusSystemV2;

namespace PlayerV2
{
    public class PlayerControl : MonoBehaviour
    {
        [SerializeField] private MoveAction moveAction;
        [SerializeField] private AttackAction attackAction;
        [SerializeField] private PlayerCondition PCondition;
        [SerializeField] private PauseSystemV2 pauseSystem;
        [SerializeField] private GameObject DirectionSprite;

        private float WaitInput = 0.05f;
        private float WaitInputTimer = 0f;
        private bool firstInputProcessed = false;

        float movex;
        float movez;
        float aimx;
        float aimz;
        float oldx;
        float oldz;

        public bool TurnNext = false;
        public string Action;

        public void Update()
        {
            TurnNext = false;

            if (Input.GetButton("Submit"))
            {
                Action = "Attack";
                TurnNext = true;
                return;
            }

            if (Input.GetButton("ChangeDirection"))
            {
                Action = "ChangeDirection";

                aimx = Input.GetAxis("Horizontal");
                aimz = Input.GetAxis("Vertical");

                if (Mathf.Abs(aimx) > 0.3f) aimx = Mathf.Sign(aimx);
                else aimx = 0;

                if (Mathf.Abs(aimz) > 0.3f) aimz = Mathf.Sign(aimz);
                else aimz = 0;

                return;
            }

            if (Input.GetButton("OpenMenu"))
            {
                Action = "Inventory";
                return;
            }

            if (Input.GetButton("Cancel"))  GameRule.DashMove();
            else GameRule.WalkMove();

            movex = 0;
            movez = 0;

            Action = "";

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
                Action = "Move";

                movex = Input.GetAxis("Horizontal");
                movez = Input.GetAxis("Vertical");

                if (Mathf.Abs(movex) > 0.2f) movex = Mathf.Sign(movex);
                else movex = 0;

                if (Mathf.Abs(movez) > 0.2f) movez = Mathf.Sign(movez);
                else movez = 0;

                if (movex == 0 && movez == 0)
                {
                    WaitInputTimer = WaitInput;
                    firstInputProcessed = false;
                    return;
                }

                if (Mathf.Abs(oldx) == 1 && movex == 0) movez = 0;
                if (Mathf.Abs(oldz) == 1 && movez == 0) movex = 0;

                if (PCondition.ConfusionTurn != 0)
                {
                    Vector3 PosRota = PCondition.ConfusionEvent();
                    movex = PosRota.x; movez = PosRota.z;
                }

                TurnNext = moveAction.MoveReservation(movex, movez);
                oldx = movex;
                oldz = movez;
            }
        }

        public bool PlayerInput()
        {
            switch(Action)
            {
                case "Attack":
                    DirectionSprite.SetActive(false);

                    PlayerAction PA = GetComponent<PlayerAction>();
                    PA.PlayerToAttack = attackAction;

                    if (PCondition.ConfusionTurn != 0)
                    {
                        Vector3 PosRota = PCondition.ConfusionEvent();
                        transform.rotation = Quaternion.Euler(0, PosRota.y, 0);
                    }
                    return TurnNext;

                case "ChangeDirection":
                    DirectionSprite.SetActive(true);

                    if ((oldx != aimx && Mathf.Abs(aimx) == 1) || (oldz != aimz && Mathf.Abs(aimz) == 1))
                    {
                        moveAction.ChangeDirectionOnTheSpot(aimx, aimz);
                    }
                    oldx = aimx;
                    oldz = aimz;
                    return TurnNext;

                case "Inventory":
                    DirectionSprite.SetActive(false);
                    pauseSystem.PauseSwitching();
                    return TurnNext;

                case "Move":
                    DirectionSprite.SetActive(false);

                    if (!TurnNext)
                    {
                        moveAction.ChangeDirectionOnTheSpot(movex, movez);
                        return TurnNext;
                    }
                    if (PCondition.ConfusionTurn != 0) PCondition.ConfuParticle();
                    moveAction.MoveReservationStance();
                    return TurnNext;

                default:
                    DirectionSprite.SetActive(false);
                    return false;

            }
        }
    }
}