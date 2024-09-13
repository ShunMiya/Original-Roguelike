using Performances;
using UnityEngine;

namespace UISystemV2
{
    public class ButtonSESystem : MonoBehaviour
    {
        private MenuSoundEffect menuSE;
        private bool FirstSelect = true;

        void Awake()
        {
            menuSE = FindObjectOfType<MenuSoundEffect>();
        }
        public void OnSelected()
        {
            if (FirstSelect)
            {
                FirstSelect = false;
                return;
            }
            menuSE.MenuOperationSE(0);
        }

        public void YesSelected()
        {
            menuSE.MenuOperationSE(1);
        }

        public void NoSelected()
        {
            menuSE.MenuOperationSE(2);
        }
    }
}