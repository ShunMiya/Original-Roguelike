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
            // プレイヤーの位置と向きを取得
            Vector3 playerPosition = transform.position;
            Quaternion playerRotation = transform.rotation;

            // プレイヤーの正面にある領域を計算
            Vector3 playerFrontPosition = playerPosition + playerRotation * Vector3.forward * gridSize;
            Vector3 boxSize = new Vector3(gridSize / 4f, gridSize / 4f, gridSize / 4f);

            // プレイヤーの正面にオブジェクトが存在するか判定
            Collider[] colliders = Physics.OverlapBox(playerFrontPosition, boxSize, playerRotation);
            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag(EnemyTag))
                {
                    return true; // EnemyTagのオブジェクトが存在する
                }
            }

            return false; // EnemyTagのオブジェクトが存在しない
        }
    }

}
