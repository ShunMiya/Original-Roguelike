using Field;
using MoveSystem;
using System.Collections;
using System.Collections.Generic;
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
            setPos = null;
            foreach (Dir d in System.Enum.GetValues(typeof(Dir)))
            {
                Pos2D newPos = DirUtil.GetNewGrid(pos, d);
                GameObject areaObj = field.IsCollideReturnAreaObj(newPos.x, newPos.z);
                if(areaObj == null)
                {
                    setPos = new Pos2D { x = newPos.x, z = newPos.z };
                    break;
                }
            }
            if(setPos != null)
            {
                GameObject spawnedItem = Instantiate(prefab, parent.transform);
                spawnedItem.GetComponent<MoveAction>().SetPosition(setPos.x, setPos.z);
                spawnedItem.GetComponent<SteppedOnEvent>().num = num;
            }
            if (systemText == null) systemText = FindObjectOfType<SystemTextV2>();
            Debug.Log(prefabName + "ÇÃNum" + num + "ÇéÃÇƒÇΩ");
        }

        public void RandomItemCreate(Vector3 position)
        {
            bool chooseEquipItem = Random.value < 0.3f;
            IItemDataV2 randomItem = ItemDataCacheV2.GetRandomItem(chooseEquipItem);

            string prefabName = randomItem.PrefabName;
            string prefabPath = "PrefabsV2/" + prefabName;

            GameObject prefab = Resources.Load<GameObject>(prefabPath);
            if (parent == null) parent = GameObject.Find("Items");
            GameObject spawnedItem = Instantiate(prefab, position, Quaternion.identity, parent.transform);

            int randomNum = NumSet();
            spawnedItem.GetComponent<SteppedOnEvent>().num = randomNum;
            if (systemText == null) systemText = FindObjectOfType<SystemTextV2>();
            Debug.Log(prefabName + "ÇÃNum" + randomNum + "Ç™ÉhÉçÉbÉv");
        }

        public int NumSet()
        {
            int randomNum = Random.Range(1, 101);
            int selectedNumber;
            Debug.Log(randomNum + "Ç™ê∂ê¨Ç≥ÇÍÇΩÇÊ");
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
