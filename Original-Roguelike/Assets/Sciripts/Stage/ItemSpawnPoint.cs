using UnityEngine;

namespace ItemSystemSQL
{
    public class ItemSpawnPoint : MonoBehaviour
    {
        [SerializeField] private ItemFactory itemFactory;

        private void Start()
        {
            itemFactory.ItemCreate(transform.position);
            Destroy(gameObject);
        }
    }
}