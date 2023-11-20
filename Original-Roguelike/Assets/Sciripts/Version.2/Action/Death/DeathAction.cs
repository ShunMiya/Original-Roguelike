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
        private DeathObjects deathObjects;

        private GameObject Attacker;
        private int Exp;

        public float duration = 0.5f;  // フェードにかかる時間（秒）

        private void Start()
        {
            systemText = FindObjectOfType<SystemTextV2>();
            deathObjects = FindObjectOfType<DeathObjects>();
        }

        public void DeathSet(GameObject attacker, int exp)
        {
            Attacker = attacker;
            Exp = exp;
            
            deathObjects.objectsToDeath.Add(this);
        }


        public IEnumerator DeathEvent()
        {
            switch(gameObject.tag)
            {
                case "Player":

                    break;

                case "Enemy":

                    yield return new WaitForSeconds(0.2f);

                    systemText.TextSet("<color=red>" + name + "</color>は倒れた！");

                    yield return StartCoroutine(FadeOutCoroutine()); //死亡演出

                    deathObjects.GetExp(Attacker.CompareTag("Player") ? Exp : 0);

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
            yield return null;
        }
    }
}