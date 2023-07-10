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
            Debug.Log(damage + "のダメージを与えた。敵残りHP" + currentHP);

            if (currentHP <= 0)
            {
                // HPが0以下になった場合、EnemyDefeatedイベントを発火させる
                if (EnemyDefeated != null)
                {
                    EnemyDefeated();
                }
            }
        }
    }
}