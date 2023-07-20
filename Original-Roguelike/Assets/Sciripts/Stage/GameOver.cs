using UnityEngine;

namespace GameEndSystem
{
    public class GameOver : MonoBehaviour
    {
        public bool isGameOver;
        [SerializeField] private GameObject GameOverUI;


        public void GameOverPerformance()
        {
            isGameOver = true;
            GameOverUI.SetActive(true);
        }

        public bool IsGameOver()
        { return isGameOver; }
    }
}