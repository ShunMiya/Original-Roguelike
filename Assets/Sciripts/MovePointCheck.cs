using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MovePointChecker
{
    public class MovePointCheck : MonoBehaviour
    {
        public bool MovePossible(Vector3 targetPos, float gridSize)
        {
            // 移動先の範囲を計算する
            Vector3 boxSize = new Vector3(gridSize / 2f, gridSize / 2f, gridSize / 2f);

            // 移動先の範囲にあるColliderを取得
            Collider[] colliders = Physics.OverlapBox(targetPos, boxSize);

            // Wallタグのオブジェクトがあるかチェックする
            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("Wall") || collider.CompareTag("Enemy"))
                {
                    return false; // 移動不可
                }
            }

            return true; // 移動可能
        }
    }
}