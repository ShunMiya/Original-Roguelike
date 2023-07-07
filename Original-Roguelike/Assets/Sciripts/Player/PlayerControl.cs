using UnityEngine;
using PlayerMovement;
using Combat.AttackMotion;
using PlayerStatusList;

public class PlayerControl : MonoBehaviour
{
    private PlayerMove playerMove;
    private AttackMotion attackMotion;

    float movex;
    float movez;

    // Start is called before the first frame update
    void Start()
    {
        playerMove = GetComponent<PlayerMove>();
        attackMotion = GetComponent<AttackMotion>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Approximately(Time.timeScale, 0f)) return;
        
        movex = Input.GetAxisRaw("Horizontal");
        movez = Input.GetAxisRaw("Vertical");

        playerMove.MoveStance(movex, movez);

        if (Input.GetKeyDown(KeyCode.Z))
        {
            attackMotion.AttackStance();
        }

    }
}
