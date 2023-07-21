using UnityEngine;
using UnityEngine.SceneManagement;

namespace UISystem
{
    public class ScemeButton : MonoBehaviour
    {
        public void BackTitleButtonClick()
        {
            SceneManager.LoadScene("Title");
        }

        public void GameStartButtonClick()
        {
            SceneManager.LoadScene("Main");
        }

        public void GameEndButtonClick()
        {
            Application.Quit();
        }

        public void RetryButtonClick()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}