using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticCoroutine
{
    //�������Obj��������܂őҋ@
    public static IEnumerator ObjectActiveFalse(GameObject Obj)
    {
        while (Obj.activeSelf)
        {
            yield return null;
        }
    }
}
