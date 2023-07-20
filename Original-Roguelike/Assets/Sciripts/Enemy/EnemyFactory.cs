using UnityEngine;

namespace Enemy
{
    public class EnemyFactory : MonoBehaviour
    {
        public void EnemyCreate(Vector3 position, Transform parent)
        {
            EnemyData randomEnemy = EnemyDataCache.GetRandomEnemy();

            string prefabName = randomEnemy.PrefabName;
            string prefabPath = "Prefabs/" + prefabName;

            GameObject prefab = Resources.Load<GameObject>(prefabPath);
            GameObject spawnedItem = Instantiate(prefab, position, Quaternion.identity, parent);

            spawnedItem.GetComponent<EnemyStatus>().currentHP = randomEnemy.MaxHP;
        }
    }
}