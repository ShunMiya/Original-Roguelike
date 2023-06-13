using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerFrontChecker;


namespace Combat.AttackMotion
{
    public class AttackMotion : MonoBehaviour
    {
        private string EnemyTag = "Enemy";
        float gridSize = GameRule.GridSize;

        private PlayerFrontCheck playerFrontCheck;

        private void Start()
        {
            playerFrontCheck = GetComponentInChildren<PlayerFrontCheck>();
        }
        public void AttackStance()
        {
            Debug.Log("Attack!");
            if (playerFrontCheck.IsAttackHitCheck())
            {
                Debug.Log("EnemyDamage");
            }
/*            if (EnemyStay())
            {
                Debug.Log("EnemyDamage");
            }*/
        }

        public bool IsAttacking()
        {
            return false;
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
