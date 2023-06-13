using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MovePointChecker
{
    public class MovePointCheck : MonoBehaviour
    {
        public bool MovePossible(Vector3 targetPos, float gridSize)
        {
            Vector3 boxSize = new Vector3(gridSize / 2f, gridSize / 2f, gridSize / 2f);

            Collider[] colliders = Physics.OverlapBox(targetPos, boxSize);

            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("Wall") || collider.CompareTag("Enemy"))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
