using System.Collections;
using System.Linq;
using UISystemV2;
using UnityEngine;

namespace DeathSystem
{
    public class DeathAction : MonoBehaviour
    {
        public delegate void EnemyDeathEvent();
        public event EnemyDeathEvent EnemyDeath;
        private SystemTextV2 systemText;

        private GameObject Attacker;
        private int Exp;

        public float duration = 1f;  // フェードにかかる時間（秒）

        private void Start()
        {
            systemText = FindObjectOfType<SystemTextV2>();
        }

        public void DeathSet(GameObject attacker, int exp)
        {
            Attacker = attacker;
            Exp = exp;

            DeathObjects deathObjects = FindObjectOfType<DeathObjects>();
            if (deathObjects != null)
            {
                deathObjects.objectsToDeath.Add(this);
            }
        }


        public IEnumerator DeathEvent()
        {
            int exp = 0;

            switch(gameObject.tag)
            {
                case "Player":

                    break;

                case "Enemy":

                    systemText.TextSet("<color=red>" + name + "</color>は倒れた！");

                    //yield return StartCoroutine(FadeOutCoroutine());　死亡演出

                    if (Attacker.CompareTag("Player"))
                    {
                        exp = Exp;
                    }
                    yield return exp;

                    Debug.Log("死亡処理終了");
                    EnemyDeath();

                    break;
            }
        }

        public IEnumerator FadeOutCoroutine()
        {
            Renderer[] renderers = GetComponentsInChildren<Renderer>();

            float timer = 0.0f;

            while (timer < duration)
            {
                float alpha = Mathf.Lerp(1.0f, 0.0f, timer / duration);

                foreach (Renderer renderer in renderers)
                {
                    foreach (Material material in renderer.materials)
                    {
                        Color color = material.color;
                        color.a = alpha;
                        material.color = color;
                    }
                }

                timer += Time.deltaTime;
                yield return null;
            }
            Debug.Log("透明化完了");
        }
    }
}