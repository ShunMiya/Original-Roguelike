using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

namespace UISystem
{
    public class InventorySceneButton : MonoBehaviour
    {
        //　インフォメーションテキストに表示する文字列
        [SerializeField] private string informationString;
        //　インフォメーションテキスト
        [SerializeField] private TextMeshProUGUI informationText;
        //　自身の親のCanvasGroup
        private CanvasGroup canvasGroup;
        //　前の画面に戻るボタン
        private GameObject returnButton;

        [SerializeField] private GameObject InventoryUI;

        void Start()
        {
            canvasGroup = GetComponentInParent<CanvasGroup>();
            returnButton = transform.parent.Find("BackGameButton").gameObject;
        }

        void OnEnable()
        {
            //　装備アイテム選択中にステータス画面を抜けた時にボタンが無効化したままの場合もあるので立ち上げ時に有効化する
            GetComponent<Button>().interactable = true;
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
                informationText.text = informationString;
            }
        }
        //　ボタンから移動したら情報を削除
        public void OnDeselected()
        {
            informationText.text = "";
        }

        //　ステータスウインドウを非アクティブにする
        public void DisableWindow()
        {
            if (canvasGroup == null || canvasGroup.interactable)
            {
                //　ウインドウを非アクティブにする
                Transform backgroundTransform = transform.root.Find("BackGround");
                GameObject backgroundObject = backgroundTransform.gameObject;
                backgroundObject.SetActive(false);
            }
        }

        //　他の画面を表示する
        public void WindowOnOff(GameObject window)
        {
            if (canvasGroup == null || canvasGroup.interactable)
            {
                InventoryUI.GetComponent<PauseSystem>().ChangeWindow(window);
            }
        }
        //　前の画面に戻るボタンを選択状態にする
        public void SelectReturnButton()
        {
            EventSystem.current.SetSelectedGameObject(returnButton);
        }
    }
}
