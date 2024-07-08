using System.Collections;
using UnityEngine;
using UISystemV2;

namespace ADVSystem
{
    public class ADVEvent : MonoBehaviour
    {
        public string Type;
        private ADVText aDVText;

        private void Start()
        {
            aDVText = FindFirstObjectByType<ADVText>();
        }


        public IEnumerator Event()
        {
            aDVText.TypeSet(Type);
            yield return StartCoroutine(aDVText.TextBoxSet());

            Destroy(gameObject);
        }
    }
}
