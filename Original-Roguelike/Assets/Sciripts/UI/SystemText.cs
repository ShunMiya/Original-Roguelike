using System.Collections;
using TMPro;
using UnityEngine;

namespace UISystem
{
    public class SystemText : MonoBehaviour
    {
        public TextMeshProUGUI textMeshPro;
        private Coroutine displayCoroutine;

        public void TextSet(string text)
        {
            if (!gameObject.activeSelf) return;
            if (displayCoroutine != null)
            {
                StopCoroutine(displayCoroutine);
            }

            textMeshPro.text = text;
            displayCoroutine = StartCoroutine(DisplayTextForSeconds(1f));
        }

        private IEnumerator DisplayTextForSeconds(float duration)
        {
            yield return new WaitForSeconds(duration);
            textMeshPro.text = string.Empty;
            displayCoroutine = null;
        }
    }
}