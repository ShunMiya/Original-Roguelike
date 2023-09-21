using PlayerStatusList;
using System.Collections;
using System.Collections.Generic;
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
            if(PoisonTurn > 0) PoisonTurn--;
            if(ConfusionTurn > 0) ConfusionTurn--;
            if(StunTurn > 0) StunTurn--;
            if(BlindTurn > 0) BlindTurn--;
        }

        public void ConditionClear()
        {
            PoisonTurn = 0;
            ConfusionTurn = 0;
            StunTurn = 0;
            BlindTurn = 0;
        }
    }
}