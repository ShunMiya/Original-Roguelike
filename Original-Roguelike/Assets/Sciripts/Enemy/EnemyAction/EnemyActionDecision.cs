using UnityEngine;

namespace Enemy
{
    public class EnemyActionDecision : MonoBehaviour
    {
        private EnemyPlayerAdjacentCheck playercheck;

        private void Start()
        {
            playercheck = GetComponentInChildren<EnemyPlayerAdjacentCheck>();
        }

        public void ActionDecision()
        {
            switch (playercheck.IsAttackHit())
            {
                case true:
                    playercheck.Attacked();
                    break;
                case false:
                    break;
            }
        }
    }
}