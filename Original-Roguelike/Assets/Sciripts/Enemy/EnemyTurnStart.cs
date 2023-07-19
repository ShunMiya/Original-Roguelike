using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace Enemy
{
    public class EnemyTurnStart : MonoBehaviour
    {
        public List<GameObject> enemyObjects;

        private void Start()
        {
            StartCoroutine(EnemySet());
        }

        private IEnumerator EnemySet()
        {
            yield return new WaitForSeconds(0.5f);

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