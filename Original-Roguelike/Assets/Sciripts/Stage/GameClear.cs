using UnityEngine;

namespace GameEndSystem
{
    public class GameClear : MonoBehaviour
    {
        private GameEnd gameEnd;

        private void Start()
        {
            gameEnd = FindObjectOfType<GameEnd>();
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                gameEnd.GameClearPerformance();
            }
        }
    }
}