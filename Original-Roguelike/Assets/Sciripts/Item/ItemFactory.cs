using UISystem;
using UnityEngine;

namespace ItemSystemSQL
{
    public class ItemFactory : MonoBehaviour
    {
        [SerializeField] private GameObject itemSQLDB;
        private SystemText systemText;

        private void Start()
        {
            systemText = FindObjectOfType<SystemText>();
        }

        public void ItemCreate(Vector3 position)
        {
            bool chooseEquipItem = Random.value < 0.3f;
            IItemData randomItem = ItemDataCache.GetRandomItem(chooseEquipItem);

            string prefabName = randomItem.PrefabName;
            string prefabPath = "Prefabs/" + prefabName;

            GameObject prefab = Resources.Load<GameObject>(prefabPath);
            GameObject spawnedItem = GameObject.Instantiate(prefab, position, Quaternion.identity);
            spawnedItem.transform.parent = itemSQLDB.transform;

            //randomItem��Id����Cashe�{������Item��񔻕ʂ���Num�̏���𔻒f����c�Ȃ�Ă��Ƃ��Ȃ�
            //�C���^�[�t�F�[�X��Num����������邩�������S�ẴA�C�e����Num�̏���𑵂���ق��������̂ł́H
            int randomNum = NumSet();
            spawnedItem.GetComponent<SQLDBGetItem>().num = randomNum;
            systemText.TextSet(randomItem.ItemName + " Drop!");
            Debug.Log(prefabName+"��Num"+ randomNum + "���h���b�v");
        }

        public int NumSet()
        {
            int randomNum = Random.Range(1, 101);
            int selectedNumber;
            Debug.Log(randomNum + "���������ꂽ��");
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