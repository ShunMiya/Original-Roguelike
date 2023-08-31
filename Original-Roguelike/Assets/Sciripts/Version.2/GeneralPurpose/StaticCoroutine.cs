using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticCoroutine
{
    //‘ã“ü‚µ‚½Obj‚ªÁ‚¦‚é‚Ü‚Å‘Ò‹@
    public static IEnumerator ObjectActiveFalse(GameObject Obj)
    {
        while (Obj.activeSelf)
        {
            yield return null;
        }
    }
}
