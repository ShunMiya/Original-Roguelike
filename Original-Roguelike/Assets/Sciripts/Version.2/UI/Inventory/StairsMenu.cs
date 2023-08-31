using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UISystemV2
{
    public class StairsMenu : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        public void DisableWindow()
        {
            Time.timeScale = 1f;
            gameObject.SetActive(false);
            Input.ResetInputAxes();
        }
    }
}