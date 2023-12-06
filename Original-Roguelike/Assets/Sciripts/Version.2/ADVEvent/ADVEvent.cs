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
                case "ê‡ñæ0":
                    Debug.Log("ÇO");
                    break;
                case "ê‡ñæ1":
                    Debug.Log("ÇP");
                    break;
                case "ê‡ñæ2":
                    Debug.Log("ÇQ");
                    break;
            }
            yield return null;
        }
    }
}
