using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerMovement;

namespace Combat.AttackMosion
{
    public class AttackMosion : MonoBehaviour
    {
        private string EnemyTag = "Enemy";
        float gridSize = GameRule.GridSize;

        public void AttackStance()
        {
            if(!FlagControl.PlayerActionFlagControl.IsPlayerAction)
            {
                FlagControl.PlayerActionFlagControl.SetPlayerAction(true);
                Debug.Log("Attack!");
                if (EnemyStay())
                {
                    Debug.Log("EnemyDamage");
                }
                FlagControl.PlayerActionFlagControl.SetPlayerAction(false);
            }
        }

        private bool EnemyStay()
        {
            // �v���C���[�̈ʒu�ƌ������擾
            Vector3 playerPosition = transform.position;
            Quaternion playerRotation = transform.rotation;

            // �v���C���[�̐��ʂɂ���̈���v�Z
            Vector3 playerFrontPosition = playerPosition + playerRotation * Vector3.forward * gridSize;
            Vector3 boxSize = new Vector3(gridSize / 4f, gridSize / 4f, gridSize / 4f);

            // �v���C���[�̐��ʂɃI�u�W�F�N�g�����݂��邩����
            Collider[] colliders = Physics.OverlapBox(playerFrontPosition, boxSize, playerRotation);
            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag(EnemyTag))
                {
                    return true; // EnemyTag�̃I�u�W�F�N�g�����݂���
                }
            }

            return false; // EnemyTag�̃I�u�W�F�N�g�����݂��Ȃ�
        }
    }

}
