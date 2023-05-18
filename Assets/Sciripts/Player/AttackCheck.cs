using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCheck : MonoBehaviour
{
    private string EnemyTag = "Enemy";
    private bool isAttack = false;
    private bool isEnemyStay = false;

    public bool IsAttack()
    {
        if(isEnemyStay)
        {
            isAttack = true;
        }
        else
        {
            isAttack = false;
        }
        return isAttack;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == EnemyTag)
        {
            isEnemyStay = true;
        }
    }

 /*   private void OnTriggerStay(Collider collision)
    {
        if (collision.tag == EnemyTag)
        {
            isEnemyStay = true;
        }
        else
        {
            isEnemyStay = false;
        }
    }*/

    private void OnTriggerExit(Collider collision)
    {
        if (collision.tag == EnemyTag)
        {
            isEnemyStay = false;
        }

    }
}
