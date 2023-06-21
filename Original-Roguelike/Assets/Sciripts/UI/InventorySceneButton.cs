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
        //�@�C���t�H���[�V�����e�L�X�g�ɕ\�����镶����
        [SerializeField] private string informationString;
        //�@�C���t�H���[�V�����e�L�X�g
        [SerializeField] private TextMeshProUGUI informationText;
        //�@���g�̐e��CanvasGroup
        private CanvasGroup canvasGroup;
        //�@�O�̉�ʂɖ߂�{�^��
        private GameObject returnButton;

        [SerializeField] private GameObject InventoryUI;

        void Start()
        {
            canvasGroup = GetComponentInParent<CanvasGroup>();
            returnButton = transform.parent.Find("BackGameButton").gameObject;
        }

        void OnEnable()
        {
            //�@�����A�C�e���I�𒆂ɃX�e�[�^�X��ʂ𔲂������Ƀ{�^���������������܂܂̏ꍇ������̂ŗ����グ���ɗL��������
            GetComponent<Button>().interactable = true;
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
                informationText.text = informationString;
            }
        }
        //�@�{�^������ړ�����������폜
        public void OnDeselected()
        {
            informationText.text = "";
        }

        //�@�X�e�[�^�X�E�C���h�E���A�N�e�B�u�ɂ���
        public void DisableWindow()
        {
            if (canvasGroup == null || canvasGroup.interactable)
            {
                //�@�E�C���h�E���A�N�e�B�u�ɂ���
                Transform backgroundTransform = transform.root.Find("BackGround");
                GameObject backgroundObject = backgroundTransform.gameObject;
                backgroundObject.SetActive(false);
            }
        }

        //�@���̉�ʂ�\������
        public void WindowOnOff(GameObject window)
        {
            if (canvasGroup == null || canvasGroup.interactable)
            {
                InventoryUI.GetComponent<PauseSystem>().ChangeWindow(window);
            }
        }
        //�@�O�̉�ʂɖ߂�{�^����I����Ԃɂ���
        public void SelectReturnButton()
        {
            EventSystem.current.SetSelectedGameObject(returnButton);
        }
    }
}
