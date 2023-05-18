using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;
    private float distance = 1.0f;

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
        if (move != Vector3.zero && transform.position == targetPos)
        {
            targetPos += new Vector3(move.x, 0, move.z) * distance;
        }

        transform.LookAt(targetPos);

        Move(targetPos);
    }
    private void Move(Vector3 targetPos)
    {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
    }
}
