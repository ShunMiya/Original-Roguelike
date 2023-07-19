using UnityEngine;

namespace Enemy
{
    public class EnemyFactory : MonoBehaviour
    {
        public void EnemyCreate(Vector3 position, Transform parent)
        {
            EnemyData randomEnemy = EnemyDataCashe.GetRandomEnemy();

            string prefabName = randomEnemy.PrefabName;
            string prefabPath = "Prefabs/" + prefabName;

            GameObject prefab = Resources.Load<GameObject>(prefabPath);
            Instantiate(prefab, position, Quaternion.identity, parent);
        }
    }
}