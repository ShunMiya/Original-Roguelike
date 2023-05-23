using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlagControl
{
    public class PlayerActionFlagControl : MonoBehaviour
    {
        private static bool PlayerAction = false;

        public static bool IsPlayerAction
        {
            get { return PlayerAction; }
        }

        public static void SetPlayerAction(bool playerAction)
        {
            PlayerAction = playerAction;
        }
    }

}
