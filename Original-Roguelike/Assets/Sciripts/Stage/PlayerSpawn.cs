using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    void Awake()
    {
        string prefabPath = "Prefabs/Player";

        GameObject prefab = Resources.Load<GameObject>(prefabPath);
        Instantiate(prefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}