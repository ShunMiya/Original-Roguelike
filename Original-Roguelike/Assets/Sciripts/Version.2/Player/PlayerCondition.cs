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
                    break;
                case 2:
                    ConfusionTurn = TurnNum;
                    break;
                case 3:
                    StunTurn = TurnNum;
                    break;
                case 4:
                    BlindTurn = TurnNum;
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

        public void StunEvent()
        {
            //‰‰o‚Æ‚©
        }

        public void BlindEvent()
        {
            //‰‰o‚Æ‚©
        }

        public void ConditionTurn()
        {
            PoisonTurn = (PoisonTurn > 0) ? PoisonTurn - 1 : 0;
            ConfusionTurn = (ConfusionTurn > 0) ? ConfusionTurn - 1 : 0;
            StunTurn = (StunTurn > 0) ? StunTurn - 1 : 0;
            BlindTurn = (BlindTurn > 0) ? BlindTurn - 1 : 0;
        }

        public void ConditionClear()
        {
            PoisonTurn = 0;
            ConfusionTurn = 0;
            StunTurn = 0;
            BlindTurn = 0;
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