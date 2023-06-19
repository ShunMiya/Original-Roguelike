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
        //�@�C���t�H���[�V�����e�L�X�g
        public TextMeshProUGUI informationText;
        public ItemData itemData;
        //�@���g�̐e��CanvasGroup
        private CanvasGroup canvasGroup;
        public PlayerUseItem playerUseItem;
        private CreateItemButton createItemButton;


        void Start()
        {
            canvasGroup = GetComponentInParent<CanvasGroup>();
            createItemButton = GetComponentInParent<CreateItemButton>();

        }

        //�@�{�^���̏�Ƀ}�E�X�����������A�܂��̓L�[����ňړ����Ă�����
        public void OnSelected()
        {
            if (canvasGroup == null || canvasGroup.interactable)
            {
                //�@�C�x���g�V�X�e���̃t�H�[�J�X�����̃Q�[���I�u�W�F�N�g�ɂ��鎞���̃Q�[���I�u�W�F�N�g�Ƀt�H�[�J�X
                if (EventSystem.current.currentSelectedGameObject != gameObject)
                {
                    EventSystem.current.SetSelectedGameObject(gameObject);
                }
                string desciption = itemData.Desciption;
                informationText.text = desciption;
            }
        }
        //�@�{�^������ړ�����������폜
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
