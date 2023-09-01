using UnityEngine;

namespace GameEndSystemV2
{
    public class GameEndV2 : MonoBehaviour
    {
        [SerializeField] private GameObject GameOverUI;
        [SerializeField] private GameObject GameClearUI;

        public void GameOverPerformance()
        {
            GameOverUI.SetActive(true);
        }

        public void GameClearPerformance()
        {
            GameClearUI.SetActive(true);
        }
    }
}