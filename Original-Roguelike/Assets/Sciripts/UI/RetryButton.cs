using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameEndSystem
{
    public class RetryButton : MonoBehaviour
    {
        public void OnClick()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}