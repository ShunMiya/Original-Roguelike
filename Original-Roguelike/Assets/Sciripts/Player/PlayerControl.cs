using UnityEngine;
using PlayerMovement;
using Combat.AttackMotion;
using PlayerStatusList;

public class PlayerControl : MonoBehaviour
{
    private PlayerMove playerMove;
    private AttackMotion attackMotion;
    private PlayerStatusSQL playerStatusSQL;

    float movex;
    float movez;

    void Start()
    {
        playerMove = GetComponent<PlayerMove>();
        attackMotion = GetComponent<AttackMotion>();
        playerStatusSQL = GetComponent<PlayerStatusSQL>();
    }

    void Update()
    {
        if (Mathf.Approximately(Time.timeScale, 0f)) return;
        if (!playerStatusSQL.IsPlayerActive())
        {
            movex = Input.GetAxisRaw("Horizontal");
            movez = Input.GetAxisRaw("Vertical");

            if (Input.GetKeyDown(KeyCode.Z))
            {
                attackMotion.AttackStance();
            }

        }

        playerMove.MoveStance(movex, movez);
    }
}