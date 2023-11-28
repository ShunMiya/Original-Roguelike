using Performances;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UISystemV2
{
    public class PauseSystemV2 : MonoBehaviour
    {
        [SerializeField] private GameObject pauseUI;
        [SerializeField] private GameObject Systemtext;
        [SerializeField] private GameObject subMenu;
        [SerializeField] private GameObject stairsMenu;

        [SerializeField] private GameObject[] windowLists;

        [SerializeField] private SystemTextV2 systemText;

        [SerializeField] private MenuSoundEffect menuSoundEffect;

        public void PauseSwitching()
        {
            menuSoundEffect.MenuOperationSE(9);
            subMenu.SetActive(false);
            pauseUI.SetActive(true);
            systemText.NonActive();
            ChangeWindow(windowLists[0]);
            
            Time.timeScale = 0f;
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

        public IEnumerator StairsMenuOpen()
        {
            stairsMenu.gameObject.SetActive(true);
            EventSystem.current.SetSelectedGameObject(stairsMenu.gameObject.transform.GetChild(1).gameObject);
            
            // StairsMenu が非アクティブになるまで待機する
            while (stairsMenu.gameObject.activeSelf)
            {
                yield return null;
            }
        }
    }
}