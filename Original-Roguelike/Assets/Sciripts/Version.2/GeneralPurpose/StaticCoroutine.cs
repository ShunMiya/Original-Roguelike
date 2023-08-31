using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticCoroutine
{
    //代入したObjが消えるまで待機
    public static IEnumerator ObjectActiveFalse(GameObject Obj)
    {
        while (Obj.activeSelf)
        {
            yield return null;
        }
    }
}
