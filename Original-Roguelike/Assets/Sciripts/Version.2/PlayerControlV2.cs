using UnityEngine;

namespace PlayerV2
{
    public class PlayerControlV2 : MonoBehaviour
    {
        private PlayerMoveV2 playerMove;

        float movex;
        float movez;

        void Start()
        {
            playerMove = GetComponent<PlayerMoveV2>();
        }

        void Update()
        {
            movex = 0;
            movez = 0;
            if (Mathf.Approximately(Time.timeScale, 0f)) return;

            movex = Input.GetAxis("Horizontal");
            movez = Input.GetAxis("Vertical");

            if (Mathf.Abs(movex) > 0.3f) movex = Mathf.Sign(movex);
            else movex = 0;

            if (Mathf.Abs(movez) > 0.3f) movez = Mathf.Sign(movez);
            else movez = 0;

            if (Input.GetKeyDown(KeyCode.Z)/*||Input.GetButtonDown("Circle")*/)
            {
                //attackMotion.AttackStance();
            }
            playerMove.MoveStance(movex, movez);
        }
    }
}