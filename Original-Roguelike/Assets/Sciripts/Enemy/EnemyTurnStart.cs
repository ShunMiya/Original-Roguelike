using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

namespace Enemy
{
    public class EnemyTurnStart : MonoBehaviour
    {
        public List<GameObject> enemyObjects;

        private void Start()
        {
            enemyObjects.Clear();
            foreach (Transform child in transform)
            {
                if (child.CompareTag("Enemy")) enemyObjects.Add(child.gameObject);
            }
        }

        public void EnemyTurn()
        {
            foreach (GameObject enemyObject in enemyObjects)
            {
                enemyObject.GetComponent<EnemyActionDecision>().ActionDecision();
            }
        }

        public void AddEnemy(GameObject enemy)
        {
            if (!enemyObjects.Contains(enemy))
            {
                if (CompareTag("Enemy")) enemyObjects.Add(enemy);
            }
        }

        public void RemoveEnemy(GameObject enemy)
        {
            if (enemyObjects.Contains(enemy)) enemyObjects.Remove(enemy);
        }
    }
}