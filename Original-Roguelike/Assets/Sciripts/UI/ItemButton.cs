using ItemSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

namespace UISystem
{
    public class ItemButton : MonoBehaviour
    {
        //　インフォメーションテキスト
        public TextMeshProUGUI informationText;
        public ItemData itemData;
        //　自身の親のCanvasGroup
        private CanvasGroup canvasGroup;
        public PlayerUseItem playerUseItem;
        private CreateItemButton createItemButton;


        void Start()
        {
            canvasGroup = GetComponentInParent<CanvasGroup>();
            createItemButton = GetComponentInParent<CreateItemButton>();

        }

        //　ボタンの上にマウスが入った時、またはキー操作で移動してきた時
        public void OnSelected()
        {
            if (canvasGroup == null || canvasGroup.interactable)
            {
                //　イベントシステムのフォーカスが他のゲームオブジェクトにある時このゲームオブジェクトにフォーカス
                if (EventSystem.current.currentSelectedGameObject != gameObject)
                {
                    EventSystem.current.SetSelectedGameObject(gameObject);
                }
                string desciption = itemData.Desciption;
                informationText.text = desciption;
            }
        }
        //　ボタンから移動したら情報を削除
        public void OnDeselected()
        {
            informationText.text = "";
        }

        public void UseItem()
        {
            playerUseItem.UseItem(itemData);
            createItemButton.SetButton();
        }
    }
}
