using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MovePointChecker;

namespace PlayerMovement
{
    public class PlayerMove : MonoBehaviour
    {
        private MovePointCheck movePointCheck;

        [SerializeField] private float speed = 5.0f;
        float gridSize = GameRule.GridSize;
        private Vector3 move;
        private Vector3 targetPos;

        private void Start()
        {
            movePointCheck = GetComponent<MovePointCheck>();
            targetPos = transform.position;
        }

        public void MoveStance(float movex , float movez)
        {
            if (!FlagControl.PlayerActionFlagControl.IsPlayerAction)
            {
                if (Input.GetKey(KeyCode.C) && movex != 0.0f && movez != 0.0f)
                {
                    move = new Vector3(movex * gridSize, 0, movez * gridSize);
                    if (targetPos == transform.position)
                    {
                        targetPos += move;
                    }
                }
                else if (!Input.GetKey(KeyCode.C))
                {
                    move = new Vector3(movex * gridSize, 0, movez * gridSize);
                    if (targetPos == transform.position)
                    {
                        targetPos += move;
                    }
                }

                transform.LookAt(targetPos);
                if (movePointCheck.MovePossible(targetPos, gridSize))
                {
                    MoveAction();
                }
                else
                {
                    targetPos = transform.position;
                }
            }
            else
            {
                MoveAction();
            }
        }

        public void MoveAction()
        {
            FlagControl.PlayerActionFlagControl.SetPlayerAction(true);

            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

            if(transform.position == targetPos)
            {
                FlagControl.PlayerActionFlagControl.SetPlayerAction(false);
            }
        }
    }
}
