using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UISystem
{
    public class PauseSystem : MonoBehaviour
    {

        [SerializeField] private GameObject pauseUI;

        // Update is called once per frame
        public void Update()
        {
            if (Input.GetKeyDown("x")) pauseUI.SetActive(!pauseUI.activeSelf);

            if (pauseUI.activeSelf)
            {
//                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1f;
            }
        }
    }
}
