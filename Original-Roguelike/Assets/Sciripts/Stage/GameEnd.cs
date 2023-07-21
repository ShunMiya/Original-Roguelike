using UnityEngine;

namespace GameEndSystem
{
    public class GameEnd : MonoBehaviour
    {
        public bool isGameStop;
        [SerializeField] private GameObject GameOverUI;
        [SerializeField] private GameObject GameClearUI;
        [SerializeField] private GameObject StageClearUI;

        public void GameOverPerformance()
        {
            isGameStop = true;
            GameOverUI.SetActive(true);
        }

        public void GameClearPerformance()
        {
            isGameStop = true;
            GameClearUI.SetActive(true);
        }

        public void StageClearPerformance()
        {
            isGameStop = true;
            StageClearUI.SetActive(true);
        }

        public bool IsGameStop()
        { return isGameStop; }
    }
}