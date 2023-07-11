using UnityEngine;

namespace ItemSystemSQL
{
    public class ItemFactory : MonoBehaviour
    {
        [SerializeField] private GameObject itemSQLDB;
        public void ItemCreate(Vector3 position)
        {
            // ランダムにどちらのキャッシュを選択するかを決定
            bool chooseItemType = UnityEngine.Random.value < 0.3f;
            IItemData randomItem = ItemDataCache.GetRandomItem(chooseItemType);

            string prefabName = randomItem.PrefabName;
            string prefabPath = "Prefabs/" + prefabName;

            GameObject prefab = Resources.Load<GameObject>(prefabPath);
            GameObject spawnedItem = GameObject.Instantiate(prefab, position, Quaternion.identity);
            spawnedItem.transform.parent = itemSQLDB.transform;

            int randomNum = UnityEngine.Random.Range(1, 6);
            spawnedItem.GetComponent<SQLDBGetItem>().num = randomNum;
            Debug.Log(prefabName+"のNum"+ randomNum + "がドロップ");
        }
    }
}