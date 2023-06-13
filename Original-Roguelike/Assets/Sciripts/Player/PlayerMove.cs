using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MovePointChecker;
using PlayerFrontChecker;

namespace PlayerMovement
{
    public class PlayerMove : MonoBehaviour
    {
        private MovePointCheck movePointCheck;
        private PlayerFrontCheck playerFrontCheck;

        [SerializeField] private float speed = 5.0f;
        float gridSize = GameRule.GridSize;
        private Vector3 move;
        private Vector3 targetPos;
        private bool ismoving = false;

        private void Start()
        {
            movePointCheck = GetComponent<MovePointCheck>();
            playerFrontCheck = GetComponentInChildren<PlayerFrontCheck>();
            targetPos = transform.position;
        }

        public void MoveStance(float movex, float movez)
        {
            switch (Input.GetKey(KeyCode.C))
            {
                case true:
                    if (movex != 0.0f && movez != Mathf.Epsilon)
                    {
                        move = new Vector3(movex * gridSize, 0, movez * gridSize);
                        if (targetPos == transform.position) //仮置き。PlayerActiveで行動停止付けれたらいらなくなる。
                        {
                            targetPos += move;
                        }

                    } break;
                case false:
                    move = new Vector3(movex * gridSize, 0, movez * gridSize);
                    if (targetPos == transform.position) //仮置き。PlayerActiveで行動停止付けれたらいらなくなる。
                    {
                        targetPos += move;
                    } break;
            }

 /*           if ((Input.GetKey(KeyCode.C) && movex != 0.0f && movez != Mathf.Epsilon) || !Input.GetKey(KeyCode.C))
            {
                move = new Vector3(movex * gridSize, 0, movez * gridSize);
                if (targetPos == transform.position) //仮置き。PlayerActiveで行動停止付けれたらいらなくなる。
                {
                    targetPos += move;
                }
            }*/

            transform.LookAt(targetPos);

//            if (movePointCheck.MovePossible(targetPos, gridSize))

            switch(ismoving)
            {
                case true:
                    {
                        MoveAction();
                        break;
                    }
                case false:
                    {
                        if(!playerFrontCheck.IsMoveFailCheck())
                        {
                            MoveAction();
                        }
                        else
                        {
                            targetPos = transform.position;
                        }
                        break;
                    }
            }

/*            if ((!playerFrontCheck.IsMoveFailCheck() && ismoving == false) || ismoving == true)
            {
                MoveAction();
            }
            else
            {
                targetPos = transform.position;
            }*/
        }

        public void MoveAction()
        {
            ismoving = true;
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

            if(transform.position == targetPos)
            {
                ismoving = false;
            }
        }

        public bool IsMoving()
        {
            return ismoving;
        }
    }
}
