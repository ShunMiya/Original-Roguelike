using UnityEngine;

namespace GameEndSystem
{
    public class GameEnd : MonoBehaviour
    {
        public bool isGameOver;
        public bool isGameClear;
        [SerializeField] private GameObject GameOverUI;
        [SerializeField] private GameObject GameClearUI;

        public void GameOverPerformance()
        {
            isGameOver = true;
            GameOverUI.SetActive(true);
        }

        public void GameClearPerformance()
        {
            isGameClear = true;
            GameClearUI.SetActive(true);
        }

        public bool IsGameOver()
        { return isGameOver; }
        public bool IsGameClear()
        { return isGameClear; }
    }
}