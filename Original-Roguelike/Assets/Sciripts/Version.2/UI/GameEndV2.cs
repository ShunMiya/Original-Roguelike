using UISystemV2;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameEndSystemV2
{
    public class GameEndV2 : MonoBehaviour
    {
        [SerializeField] private GameObject GameOverUI;
        [SerializeField] private GameObject GameClearUI;

        public void GameOverPerformance()
        {
            GameOverUI.SetActive(true);
            EventSystem.current.SetSelectedGameObject(GameOverUI.transform.GetChild(1).gameObject);

        }

        public void GameClearPerformance()
        {
            GameClearUI.SetActive(true);
            EventSystem.current.SetSelectedGameObject(GameClearUI.transform.GetChild(1).gameObject);

        }
    }
}