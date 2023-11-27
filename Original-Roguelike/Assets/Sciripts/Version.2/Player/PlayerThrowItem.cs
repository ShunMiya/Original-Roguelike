using Field;
using ItemSystemV2.Inventory;
using MoveSystem;
using Performances;
using PlayerV2;
using System;
using System.Collections;
using UnityEngine;

namespace ItemSystemV2
{
    public class PlayerThrowItem : MonoBehaviour
    {
        private SQLInventoryRemoveV2 inventoryremove;
        private ItemFactoryV2 itemfactory;
        private MoveAction move;
        private Areamap field;

        private PlaySoundEffects playSoundEffects;
        private AudioSource audioSource;

        private DataRow row;

        private void Start()
        {
            inventoryremove = GetComponent<SQLInventoryRemoveV2>();
            move = GetComponent<MoveAction>();
            itemfactory = FindObjectOfType<ItemFactoryV2>();
            field = FindObjectOfType<Areamap>();
            playSoundEffects = FindObjectOfType<PlaySoundEffects>();
            audioSource = GetComponent<AudioSource>();

        }

        public void SetData(DataRow date)
        {
            row = date;
            PlayerAction PA = GetComponent<PlayerAction>();
            PA.playerThrowItem = this;
        }

        public IEnumerator ThrowItem()
        {
            GameObject AreaObj = field.IsCollideReturnAreaObj(move.grid.x, move.grid.z);
            GameObject ItemObj = itemfactory.ThrowItemCreate(move.grid, Convert.ToInt32(row["Id"]), Convert.ToInt32(row["Num"]));
            yield return new WaitForEndOfFrame();

            int R = (int)transform.rotation.eulerAngles.y;
            if (R > 180) R -= 360;

            yield return StartCoroutine(playSoundEffects.AttackSE(3, audioSource));
            yield return StartCoroutine(ItemObj.GetComponent<MoveThrownItem>().Throw(R, AreaObj));

            inventoryremove.RemoveItem(row, 1);

            yield return null;
        }

        public IEnumerator ThrowOffensiveItem(DataRow Row)
        {
            int Id = Convert.ToInt32(Row["Id"]);
            OffensiveDataV2 itemData = ItemDataCacheV2.GetOffensive(Id);
            GameObject itemObj = (GameObject)Resources.Load("PrefabsV2/ThrowingAttackObj");
            GameObject Obj = Instantiate(itemObj, transform);
            Obj.GetComponent<MeshRenderer>().material = (Material)Resources.Load("Materials/ThrowingKnife");
            Obj.GetComponent<MoveAction>().SetcomplementFrame();
            Obj.GetComponent<MoveAction>().SetPosition(move.grid.x, move.grid.z);
            Obj.GetComponent<ThrowObjData>().Id = Id;
            Obj.GetComponent<ThrowObjData>().Num = 1;
            Obj.GetComponent<ThrowObjData>().DamageNum = itemData.DamageNum;

            yield return new WaitForEndOfFrame();

            int R = (int)transform.rotation.eulerAngles.y;
            if (R > 180) R -= 360;

            yield return StartCoroutine(playSoundEffects.AttackSE(3, audioSource));
            yield return StartCoroutine(Obj.GetComponent<MoveThrownItem>().ThrowAttackObj(R, itemData.Range));
        }
    }
}