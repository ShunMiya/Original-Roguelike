using Field;
using MoveSystem;
using System.Collections;
using System.Collections.Generic;
using UISystemV2;
using UnityEngine;
using static UnityEditor.PlayerSettings;

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
                int randomNum = NumSet();
                spawnedItem.GetComponent<SteppedOnEvent>().num = randomNum;
                if (systemText == null) systemText = FindObjectOfType<SystemTextV2>();
                systemText.TextSet(randomItem.ItemName + " Num:" + randomNum + " Drop");
            }
        }

        public int NumSet()
        {
            int randomNum = Random.Range(1, 101);
            int selectedNumber;
            if (randomNum <= 35)
                selectedNumber = 1;
            else if (randomNum <= 60)
                selectedNumber = 2;
            else if (randomNum <= 80)
                selectedNumber = 3;
            else if (randomNum <= 95)
                selectedNumber = 4;
            else
                selectedNumber = 5;
            return selectedNumber;
        }

    }
}
