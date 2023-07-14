using UnityEngine;

namespace PlayerMovement
{
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] private float speed;
        float gridSize = GameRule.GridSize;
        private Vector3 move;
        private Vector3 targetPos;
        private bool ismoving = false;

        private void Start()
        {
            targetPos = transform.position;
        }

        public void MoveStance(float movex, float movez)
        {
            switch (Input.GetKey(KeyCode.C))
            {
                case true:
                    if (movex != 0.0f && movez != 0.0f)
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

            transform.LookAt(targetPos);

            if (targetPos == transform.position) return;
            switch(ismoving)
            {
                case true:
                    {
                        MoveAction();
                        break;
                    }
                case false:
                    {
                        if(MovePointChecker())
                        {
                            MoveAction();
                            break;
                        }
                        else
                        {
                            targetPos = transform.position;
                            break;
                        }
                    }
            }
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

        public bool MovePointChecker()
        {
            RaycastHit hit;
            Vector3 direction = targetPos - transform.position;
            float distance = direction.magnitude + 0.5f;

            if (Physics.Raycast(transform.position, direction.normalized, out hit, distance))
            {
                string tag = hit.collider.gameObject.tag;
                if (hit.collider.gameObject.CompareTag("Wall") || hit.collider.gameObject.CompareTag("Enemy"))
                {
                    return false;
                }
            }
            return true;
        }
    }
}