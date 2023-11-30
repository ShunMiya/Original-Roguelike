using System.Collections;
using UnityEngine;

namespace PlayerStatusSystemV2
{
    public class PlayerCondition : MonoBehaviour
    {
        public int PoisonTurn = 0;
        public int ConfusionTurn = 0;
        public int StunTurn = 0;
        public int BlindTurn = 0;

        private PlayerHPV2 playerHP;
        [SerializeField] private ParticleSystem Poisonparticle;
        [SerializeField] private ParticleSystem Confuparticle;
        [SerializeField] private ParticleSystem Stunparticle;
        [SerializeField] private ParticleSystem Blindparticle;


        private void Start()
        {
            playerHP = GetComponent<PlayerHPV2>();
        }

            public void SetCondition(int ConditionNum, int TurnNum)
        {
            switch(ConditionNum)
            {
                case 1:
                    PoisonTurn = TurnNum;
                    Poisonparticle.Play();
                    break;
                case 2:
                    ConfusionTurn = TurnNum;
                    break;
                case 3:
                    StunTurn = TurnNum;
                    break;
                case 4:
                    BlindTurn = TurnNum;
                    Blindparticle.Play();
                    break;
                default:
                    break;
            }
        }

        public void PoisonEvent()
        {
            playerHP.DirectDamage(1);
        }

        public Vector3 ConfusionEvent()
        {
            return DirUtil.SetNewPosRotation(DirUtil.RandomDirection());
        }

        public void ConfuParticle()
        {
            Confuparticle.Play();
        }

        public IEnumerator StunEvent()
        {
            Stunparticle.Play();

            while (Stunparticle.isPlaying)
            {
                yield return null; // パーティクルが再生中の間、待機
            }
        }

        public void BlindEvent()
        {
            //演出とか
        }

        public void ConditionTurn()
        {
            PoisonTurn = (PoisonTurn > 0) ? PoisonTurn - 1 : 0;
            if(PoisonTurn == 0) Poisonparticle.Stop();
            ConfusionTurn = (ConfusionTurn > 0) ? ConfusionTurn - 1 : 0;
            StunTurn = (StunTurn > 0) ? StunTurn - 1 : 0;
            BlindTurn = (BlindTurn > 0) ? BlindTurn - 1 : 0;
            if(BlindTurn == 0) Blindparticle.Stop();
        }

        public void ConditionClear()
        {
            PoisonTurn = 0;
            Poisonparticle.Stop();
            ConfusionTurn = 0;
            StunTurn = 0;
            BlindTurn = 0;
            Blindparticle.Stop();
        }

        public int GetConditionTurn(int ConditionNum)
        {
            switch (ConditionNum)
            {
                case 1:
                    return PoisonTurn;
                case 2:
                    return ConfusionTurn;
                case 3:
                    return StunTurn;
                case 4:
                    return BlindTurn;
                default:
                    return 0;
            }
        }
    }
}