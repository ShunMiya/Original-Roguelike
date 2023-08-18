using UnityEngine;

namespace GameEndSystemV2
{
    public class GameEndV2 : MonoBehaviour
    {
        [SerializeField] private GameObject GameOverUI;
        [SerializeField] private GameObject GameClearUI;
        [SerializeField] private GameObject StageClearUI;

        public void GameOverPerformance()
        {
            GameOverUI.SetActive(true);
        }

        public void GameClearPerformance()
        {
            GameClearUI.SetActive(true);
        }

        public void StageClearPerformance()
        {
            StageClearUI.SetActive(true);
        }
    }
}