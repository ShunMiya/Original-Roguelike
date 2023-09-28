using Field;
using MoveSystem;
using UISystemV2;
using UnityEngine;

namespace ItemSystemV2
{
    public class ItemFactoryV2 : MonoBehaviour
    {
        private SystemTextV2 systemText;
        private GameObject parent;
        private Areamap field;
        private Pos2D setPos;

        private void Awake()
        {
            systemText = FindObjectOfType<SystemTextV2>();
            parent = GameObject.Find("Items");
            field = GetComponentInParent<Areamap>();

        }

        public void SpecifiedItemCreate(Pos2D pos, int Id, int num)
        {
            IItemDataV2 itemData = ItemDataCacheV2.GetIItemData(Id);
            string prefabName = itemData.PrefabName;
            string prefabPath = "PrefabsV2/" + prefabName;

            GameObject prefab = Resources.Load<GameObject>(prefabPath);
            if (parent == null) parent = GameObject.Find("Items");

            setPos = field.ItemDropPointCheck(pos);
            
            if (setPos != null)
            {
                GameObject spawnedItem = Instantiate(prefab, parent.transform);
                spawnedItem.GetComponent<MoveAction>().SetPosition(setPos.x, setPos.z);
                spawnedItem.GetComponent<SteppedOnEvent>().num = num;
            }
            if (systemText == null) systemText = FindObjectOfType<SystemTextV2>();
            systemText.TextSet(itemData.ItemName + " Num:" + num + " Put");
        }

        public GameObject ThrowItemCreate(Pos2D pos, int Id, int num)
        {
            IItemDataV2 itemData = ItemDataCacheV2.GetIItemData(Id);
            string prefabName = itemData.PrefabName;
            string prefabPath = "PrefabsV2/" + prefabName;

            GameObject prefab = Resources.Load<GameObject>(prefabPath);
            if (parent == null) parent = GameObject.Find("Items");

            GameObject spawnedItem = Instantiate(prefab, parent.transform);
            spawnedItem.GetComponent<MoveAction>().SetcomplementFrame();
            spawnedItem.GetComponent<MoveAction>().SetPosition(pos.x, pos.z);
            spawnedItem.GetComponent<SteppedOnEvent>().num = num;

            return spawnedItem;
        }

        public void RandomItemCreate(Pos2D pos)
        {
            bool chooseEquipItem = Random.value < 0.3f;
            IItemDataV2 randomItem = ItemDataCacheV2.GetRandomItem(chooseEquipItem);

            string prefabName = randomItem.PrefabName;
            string prefabPath = "PrefabsV2/" + prefabName;

            GameObject prefab = Resources.Load<GameObject>(prefabPath);
            if (parent == null) parent = GameObject.Find("Items");

            setPos = field.ItemDropPointCheck(pos);

            if (setPos != null)
            {
                GameObject spawnedItem = Instantiate(prefab, parent.transform);
                spawnedItem.GetComponent<MoveAction>().SetPosition(setPos.x, setPos.z);
                int randomNum = RandomNum.NumSetStock();
                spawnedItem.GetComponent<SteppedOnEvent>().num = randomNum;
                if (systemText == null) systemText = FindObjectOfType<SystemTextV2>();
                systemText.TextSet(randomItem.ItemName + " Num:" + randomNum + " Drop");
            }
        }
    }
}
