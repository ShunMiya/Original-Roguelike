using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerMovement;
using Combat.AttackMosion;

public class PlayerControl : MonoBehaviour
{
    private PlayerMove playerMove;
    private AttackMosion attackMosion;

    // Start is called before the first frame update
    void Start()
    {
        playerMove = GetComponent<PlayerMove>();
        attackMosion = GetComponent<AttackMosion>();
    }

    // Update is called once per frame
    void Update()
    {
        float movex = Input.GetAxisRaw("Horizontal");
        float movez = Input.GetAxisRaw("Vertical");

            playerMove.MoveStance(movex, movez);

        if(Input.GetKeyDown(KeyCode.Z))
        {
            attackMosion.AttackStance();
        }
    }
}
