using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;

    private Vector3 move;
    private Vector3 targetPos;

    private void Start()
    {
        targetPos = transform.position;
    }
    void Update()
    {
        move.x = Input.GetAxisRaw("Horizontal");
        move.z = Input.GetAxisRaw("Vertical");

        if(Input.GetKey(KeyCode.C))
        {
            if(move.x != 0 && move.z != 0)
            {
                if (move != Vector3.zero && transform.position == targetPos)
                {
                    targetPos += new Vector3(move.x, 0, move.z);
                }
            }
        }
        else
        {
            if (move != Vector3.zero && transform.position == targetPos)
            {
                targetPos += new Vector3(move.x, 0, move.z);
            }
        }

        transform.LookAt(targetPos);

        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

    }
}
