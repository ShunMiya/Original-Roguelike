using Field;
using MoveSystem;
using System.Collections;
using UnityEngine;

public class MoveThrownItem : MonoBehaviour
{
    public MoveAction move;
    private Areamap field;

    private void Start()
    {
        field = FindObjectOfType<Areamap>();
    }

    public IEnumerator Throw(int R, GameObject AreaObj)
    {
        Pos2D Pos = DirUtil.SetAttackPoint(R);
        int xgrid = move.grid.x;
        int zgrid = move.grid.z;
        GameObject areaObj = AreaObj;
        GameObject NextPointAreaObj = null;
        xgrid += Pos.x;
        zgrid += Pos.z;

        for (int i = 1; i <= 4; i++)
        {
            NextPointAreaObj = field.IsCollideReturnAreaObj(xgrid, zgrid);


            IEnumerator Coroutine = move.ThrowMove(xgrid, zgrid);
            yield return StartCoroutine(Coroutine);

            bool Throw = (bool)Coroutine.Current;
            if (!Throw)
            {
                break;
            }

            areaObj = NextPointAreaObj;
            xgrid += Pos.x;
            zgrid += Pos.z;
        }

        Pos2D setPos = new Pos2D { x = move.grid.x, z = move.grid.z };
        if (areaObj != null)
        {
            setPos = field.ItemDropPointCheck(setPos);
        }

        if (setPos == null)
        {
            Destroy(gameObject);
            yield break;
        }
        move.SetPosition(setPos.x, setPos.z);
    }

    public IEnumerator ThrowAttackObj(int R, int range)
    {
        Pos2D Pos = DirUtil.SetAttackPoint(R);
        int xgrid = move.grid.x;
        int zgrid = move.grid.z;
        xgrid += Pos.x;
        zgrid += Pos.z;

        for (int i = 1; i <= range; i++)
        {
            IEnumerator Coroutine = move.ThrowMove(xgrid, zgrid);
            yield return StartCoroutine(Coroutine);

            bool Throw = (bool)Coroutine.Current;
            if (!Throw)
            {
                break;
            }

            xgrid += Pos.x;
            zgrid += Pos.z;
        }
        Destroy(gameObject);
    }
}