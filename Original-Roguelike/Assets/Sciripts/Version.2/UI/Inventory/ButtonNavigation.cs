using UnityEngine;
using UnityEngine.UI;

namespace UISystemV2
{
    public class ButtonNavigation : MonoBehaviour
    {
        [SerializeField] private GameObject Content;

        private Button ItemButton0;
        private Button ItemButton1;

        [SerializeField] private Button MenuButton0;
        private Navigation Menu0Defaultnavi;
        [SerializeField] private Button MenuButton1;
        private Navigation Menu1Defaultnavi;
        [SerializeField] private Button MenuButton2;
        private Navigation Menu2Defaultnavi;

        public void Awake()
        {
            Menu0Defaultnavi = MenuButton0.navigation;
            Menu1Defaultnavi = MenuButton1.navigation;
            Menu2Defaultnavi = MenuButton2.navigation;
        }
        public void SetItemMenuButtonNavigation()
        {
            if (Content.transform.childCount == 1)
            {
                ItemButton0 = Content.transform.GetChild(0).gameObject.GetComponent<Button>();

                SetMenuButtonNavigation(MenuButton0, ItemButton0, 0);
                SetMenuButtonNavigation(MenuButton1, ItemButton0, 1);
                SetMenuButtonNavigation(MenuButton2, ItemButton0, 2);

                return;
            }

            ItemButton0 = Content.transform.GetChild(0).gameObject.GetComponent<Button>();
            ItemButton1 = Content.transform.GetChild(1).gameObject.GetComponent<Button>();

            SetMenuButtonNavigation(MenuButton0, ItemButton0, 0);
            SetMenuButtonNavigation(MenuButton1, ItemButton0, 1);
            SetMenuButtonNavigation(MenuButton2, ItemButton1, 2);

            SetItemButtonNavigation(ItemButton0, 0);
            SetItemButtonNavigation(ItemButton1, 1);
        }

        public void SetEquipMenuButtonNavigation()
        {
            ItemButton0 = Content.transform.GetChild(0).gameObject.GetComponent<Button>();

            SetMenuButtonNavigation(MenuButton2, ItemButton0, 2);

            SetEquipButtonNavigation();
        }

        public void SetMenuButtonNavigation(Button button, Button areabutton, int i)
        {
            Navigation newNavi = new Navigation();
            newNavi.mode = Navigation.Mode.Explicit;
            switch (i)
            {
                case 0:
                    newNavi.selectOnRight = MenuButton1.GetComponent<Selectable>();

                    newNavi.selectOnDown = areabutton.GetComponent<Selectable>();
                    break;
                case 1:
                    newNavi.selectOnLeft = MenuButton0.GetComponent<Selectable>();
                    newNavi.selectOnRight = MenuButton2.GetComponent<Selectable>();

                    newNavi.selectOnDown = areabutton.GetComponent<Selectable>();
                    break;
                case 2:
                    newNavi.selectOnLeft = MenuButton1.GetComponent<Selectable>();

                    newNavi.selectOnDown = areabutton.GetComponent<Selectable>();
                    break;
            }
            button.navigation = newNavi;
        }
        
        public void SetItemButtonNavigation(Button button, int i)
        {
            Navigation newNavi = new Navigation();
            newNavi.mode = Navigation.Mode.Explicit;
            switch (i)
            {
                case 0:
                    newNavi.selectOnUp = MenuButton0.GetComponent<Selectable>();
                    newNavi.selectOnRight = ItemButton1.GetComponent<Selectable>();

                    newNavi.selectOnDown = Content.transform.childCount > 2
                           ? Content.transform.GetChild(2).gameObject.GetComponent<Button>() : null;
                    //if(Content.transform.childCount > 2) newNavi.selectOnDown = Content.transform.GetChild(2).gameObject.GetComponent<Button>();
                    break;
                case 1:
                    newNavi.selectOnUp = MenuButton2.GetComponent<Selectable>();
                    newNavi.selectOnLeft = ItemButton0.GetComponent<Selectable>();

                    //if (Content.transform.childCount == 3) newNavi.selectOnDown = Content.transform.GetChild(2).gameObject.GetComponent<Button>();
                    //else if (Content.transform.childCount > 3) newNavi.selectOnDown = Content.transform.GetChild(3).gameObject.GetComponent<Button>();

                    newNavi.selectOnDown = Content.transform.childCount == 3
                        ? Content.transform.GetChild(2).gameObject.GetComponent<Button>()
                        : Content.transform.childCount > 3
                            ? Content.transform.GetChild(3).gameObject.GetComponent<Button>()
                            : null;
                    break;
            }
            button.navigation = newNavi;
        }

        public void SetEquipButtonNavigation()
        {
            for(int i=0; i < Content.transform.childCount; i++)
            {
                Button EquipButton = Content.transform.GetChild(i).gameObject.GetComponent<Button>();

                Navigation newNavi = new Navigation();
                newNavi.mode = Navigation.Mode.Explicit;
                
                
                newNavi.selectOnUp = i > 0
                    ? Content.transform.GetChild(i-1).gameObject.GetComponent<Selectable>()
                    : MenuButton2.GetComponent<Selectable>();
                
                newNavi.selectOnDown = Content.transform.childCount > i+1
                    ? Content.transform.GetChild(i+1).gameObject.GetComponent<Selectable>() : null;

                newNavi.selectOnLeft = MenuButton0.GetComponent<Selectable>();

                EquipButton.navigation = newNavi;
            }
        }

        public void ResetNavigation()
        {
            MenuButton0.navigation = Menu0Defaultnavi;
            MenuButton1.navigation = Menu1Defaultnavi;
            MenuButton2.navigation = Menu2Defaultnavi;

            ItemButton0 = null;
            ItemButton1 = null;
        }
    }
}