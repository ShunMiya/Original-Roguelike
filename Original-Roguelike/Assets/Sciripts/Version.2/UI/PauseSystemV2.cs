using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using PlayerV2;

namespace UISystemV2
{
    public class PauseSystemV2 : MonoBehaviour
    {
        [SerializeField] private GameObject pauseUI;
        [SerializeField] private GameObject Systemtext;
        [SerializeField] private GameObject subMenu;

        [SerializeField] private GameObject[] windowLists;

        private SystemTextV2 systemTextCompo;

        private void Start()
        {
            systemTextCompo = Systemtext.GetComponent<SystemTextV2>();
        }
        // Update is called once per frame
        public void PauseSwitching()
        {
            subMenu.SetActive(false);
            pauseUI.SetActive(true);
            systemTextCompo.TextSet("");
            systemTextCompo.TextSet("");
            systemTextCompo.TextSet("");
            ChangeWindow(windowLists[0]);

            if (pauseUI.activeSelf)
            {
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1f;
            }
        }

        public void ChangeWindow(GameObject window)
        {
            foreach (var item in windowLists)
            {
                if (item == window)
                {
                    item.SetActive(true);
                    EventSystem.current.SetSelectedGameObject(null);
                }
                else
                {
                    item.SetActive(false);
                }
                EventSystem.current.SetSelectedGameObject(window.transform.Find("MenuArea").GetChild(0).gameObject);
            }
        }
    }
}