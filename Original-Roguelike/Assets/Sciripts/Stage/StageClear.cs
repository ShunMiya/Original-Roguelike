using UnityEngine;

namespace GameEndSystem
{
    public class StageClear : MonoBehaviour
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
                gameEnd.StageClearPerformance();
            }
        }
    }
}