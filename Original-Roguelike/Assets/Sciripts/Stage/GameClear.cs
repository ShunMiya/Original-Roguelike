using UnityEngine;

namespace GameEndSystem
{
    public class GameClear : MonoBehaviour
    {
        public bool isGameClear;
        [SerializeField]private GameObject GameClearUI;

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                isGameClear = true;
                GameClearPerformance();
            }
        }

        private void GameClearPerformance()
        {
            GameClearUI.SetActive(true);
        }

        public bool IsGameClear()
        { return isGameClear; }
    }
}