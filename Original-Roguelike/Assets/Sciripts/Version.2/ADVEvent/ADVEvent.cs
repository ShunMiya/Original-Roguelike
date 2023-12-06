using System.Collections;
using UnityEngine;

namespace ADVSystem
{
    public class ADVEvent : MonoBehaviour
    {
        public string Type;

        public IEnumerator Event()
        {
            switch (Type)
            {
                case "����0":
                    Debug.Log("�O");
                    break;
                case "����1":
                    Debug.Log("�P");
                    break;
                case "����2":
                    Debug.Log("�Q");
                    break;
            }
            yield return null;
        }
    }
}
