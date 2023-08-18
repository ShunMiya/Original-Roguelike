using System.Collections;
using TMPro;
using UnityEngine;

namespace UISystemV2
{
    public class SystemTextV2 : MonoBehaviour
    {
        public TextMeshProUGUI textMeshPro1;
        public TextMeshProUGUI textMeshPro2;
        public TextMeshProUGUI textMeshPro3;
        private Coroutine displayCoroutine;

        public void TextSet(string text)
        {
            if (!gameObject.activeSelf) return;
            if (displayCoroutine != null) StopCoroutine(displayCoroutine);

            textMeshPro3.text = textMeshPro2.text;
            textMeshPro2.text = textMeshPro1.text;
            textMeshPro1.text = text;


            displayCoroutine = StartCoroutine(DisplayTextForSeconds(2f));
        }

        private IEnumerator DisplayTextForSeconds(float duration)
        {
            yield return new WaitForSeconds(duration);
            textMeshPro1.text = "";
            textMeshPro2.text = "";
            textMeshPro3.text = "";
            displayCoroutine = null;
        }
    }
}