using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UISystem
{
    public class PauseSystemSQL : MonoBehaviour
    {

        [SerializeField] private GameObject pauseUI;
        [SerializeField] private GameObject[] windowLists;

        // Update is called once per frame
        public void Update()
        {
            if (Input.GetKeyDown("c"))
            {
                pauseUI.SetActive(!pauseUI.activeSelf);
                ChangeWindow(windowLists[0]);
            }

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