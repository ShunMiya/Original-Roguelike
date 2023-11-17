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

        public IEnumerator DeathAllObjects()
        {
            if (objectsToDeath.Count == 0) yield break;

            int Exp = 0;

            List<IEnumerator> DeathCoroutines = new List<IEnumerator>();

            foreach (DeathAction DeathChar in objectsToDeath)
            {
                IEnumerator Coroutine = DeathChar.DeathEvent();
                StartCoroutine(Coroutine);

                DeathCoroutines.Add(Coroutine);
            }

            foreach (IEnumerator Coroutine in DeathCoroutines)
            {
                yield return Coroutine;
                Exp += (int)Coroutine.Current;
            }

            // ëSÇƒÇÃçsìÆÇ™äÆóπÇµÇΩå„ÇÃèàóù
            if (Exp != 0) FindFirstObjectByType<PlayerLevel>().PlayerGetExp(Exp);

            objectsToDeath.Clear();

            yield return new WaitForSeconds(0.2f);
        }
    }
}