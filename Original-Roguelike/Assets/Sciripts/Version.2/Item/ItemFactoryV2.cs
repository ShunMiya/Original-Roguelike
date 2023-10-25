using Field;
using ItemSystemV2.Inventory;
using MoveSystem;
using System.Collections.Generic;
using System;
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
            GameObject itemObj = (GameObject)Resources.Load("PrefabsV2/Item");
            if (parent == null) parent = GameObject.Find("Items");

            setPos = field.ItemDropPointCheck(pos);

            if (setPos != null)
            {
                GameObject item = Instantiate(itemObj, parent.transform);
                item.GetComponent<MeshRenderer>().material = (Material)Resources.Load("Materials/" + itemData.PrefabName);
                item.GetComponent<MoveAction>().SetPositionItem(setPos.x, setPos.z);

                //switch(randomItem.ItemType)

                item.GetComponent<SteppedOnEvent>().ObjType = 0;
                item.GetComponent<SteppedOnEvent>().Id = Id;
                item.GetComponent<SteppedOnEvent>().num = num;
            }
            if (systemText == null) systemText = FindObjectOfType<SystemTextV2>();
            systemText.TextSet(itemData.ItemName + "‚ð’u‚¢‚½");
        }

        public GameObject ThrowItemCreate(Pos2D pos, int Id, int num)
        {
            IItemDataV2 itemData = ItemDataCacheV2.GetIItemData(Id);
            GameObject itemObj = (GameObject)Resources.Load("PrefabsV2/Item");
            if (parent == null) parent = GameObject.Find("Items");

            GameObject item = Instantiate(itemObj, parent.transform);
            item.GetComponent<MeshRenderer>().material = (Material)Resources.Load("Materials/" + itemData.PrefabName);

            item.GetComponent<MoveAction>().SetcomplementFrame();
            item.GetComponent<MoveAction>().SetPosition(pos.x, pos.z);
            item.GetComponent<SteppedOnEvent>().ObjType = 0;
            item.GetComponent<ThrowObjData>().Id = Id;
            item.GetComponent<ThrowObjData>().Num = num;
            item.GetComponent<SteppedOnEvent>().Id = Id;
            item.GetComponent<SteppedOnEvent>().num = num;

            if (systemText == null) systemText = FindObjectOfType<SystemTextV2>();
            systemText.TextSet(itemData.ItemName + "‚ð“Š‚°‚½");

            return item;
        }

        public void RandomItemCreate(Pos2D pos) //EnemyDropItemSystem
        {
            int itemId = 0;
            string databasePath = SQLDBInitializationV2.GetDatabasePath();
            SqliteDatabase sqlDB = new SqliteDatabase(databasePath);
            string query = "SELECT FloorLevel FROM PlayerStatus WHERE PlayerID = 1;";
            DataTable Data = sqlDB.ExecuteQuery(query);
            int FloorLevel = Convert.ToInt32(Data[0]["FloorLevel"]);
            List<ItemAppearData> ItemList = DungeonDataCache.GetItemsAppearInFloor(FloorLevel);
            int MaxRate = 0;
            foreach (ItemAppearData itemAppear in ItemList) MaxRate += itemAppear.GenerationRate;
            int p = UnityEngine.Random.Range(1, MaxRate + 1);
            foreach (ItemAppearData itemAppear in ItemList)
            {
                p -= itemAppear.GenerationRate;
                if (p <= 0)
                {
                    itemId = itemAppear.ItemId;
                    break;
                }
            }
            IItemDataV2 randomItem = ItemDataCacheV2.GetIItemData(itemId);

            if (randomItem == null) return;

            GameObject itemObj = (GameObject)Resources.Load("PrefabsV2/Item");

            if (parent == null) parent = GameObject.Find("Items");

            setPos = field.ItemDropPointCheck(pos);

            if (setPos != null)
            {
                GameObject item = Instantiate(itemObj, parent.transform);
                item.GetComponent<MeshRenderer>().material = (Material)Resources.Load("Materials/" + randomItem.PrefabName);
                item.GetComponent<MoveAction>().SetPositionItem(setPos.x, setPos.z);

                //switch(randomItem.ItemType)

                int randomnum = RandomNum.NumSetStock();
                item.GetComponent<SteppedOnEvent>().ObjType = 0;
                item.GetComponent<SteppedOnEvent>().Id = itemId;
                item.GetComponent<SteppedOnEvent>().num = randomnum;
            }
        }
    }
}
