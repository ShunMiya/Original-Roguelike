using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MovePointChecker
{
    public class MovePointCheck : MonoBehaviour
    {
        public bool MovePossible(Vector3 targetPos, float gridSize)
        {
            // �ړ���͈̔͂��v�Z����
            Vector3 boxSize = new Vector3(gridSize / 2f, gridSize / 2f, gridSize / 2f);

            // �ړ���͈̔͂ɂ���Collider���擾
            Collider[] colliders = Physics.OverlapBox(targetPos, boxSize);

            // Wall�^�O�̃I�u�W�F�N�g�����邩�`�F�b�N����
            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("Wall") || collider.CompareTag("Enemy"))
                {
                    return false; // �ړ��s��
                }
            }

            return true; // �ړ��\
        }
    }
}