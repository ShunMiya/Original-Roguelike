using UnityEngine;
using ItemSystemSQL;

namespace Enemy
{
    public class EnemyStatus : MonoBehaviour
    {
        public delegate void EnemyDefeatedEventHandler();
        public event EnemyDefeatedEventHandler EnemyDefeated;

        [SerializeField]private int currentHP;

        public void TakeDamage(int damage)
        {
            currentHP -= damage;
            Debug.Log(damage + "�̃_���[�W��^�����B�G�c��HP" + currentHP);

            if (currentHP <= 0 && EnemyDefeated != null)
            {
                EnemyDefeated();
            }
        }
    }
}