using PlayerStatusSystemV2;
using System.Collections;
using System.Collections.Generic;
using UISystemV2;
using UnityEngine;

namespace DeathSystem
{
    public class DeathObjects : MonoBehaviour
    {
        public List<DeathAction> objectsToDeath = new List<DeathAction>();
        [SerializeField]private SystemTextV2 systemText;
        private int Exp;

        public IEnumerator DeathAllObjects()
        {
            if (objectsToDeath.Count == 0) yield break;

            Exp = 0;

            List<Coroutine> DeathCoroutines = new List<Coroutine>();

            foreach (DeathAction DeathChar in objectsToDeath)
            {
                Coroutine coroutine = StartCoroutine(DeathChar.DeathEvent());

                DeathCoroutines.Add(coroutine);
            }

            foreach (Coroutine coroutine in DeathCoroutines)
            {
                yield return coroutine;
            }

            // ëSÇƒÇÃçsìÆÇ™äÆóπÇµÇΩå„ÇÃèàóù
            if (Exp != 0) FindFirstObjectByType<PlayerLevel>().PlayerGetExp(Exp);

            objectsToDeath.Clear();

            yield return new WaitForSeconds(0.2f);
        }

        public void GetExp(int exp)
        {
            Exp += exp;
        }
    }
}