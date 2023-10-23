using UnityEngine;
public class SetScreenSize
{
    [RuntimeInitializeOnLoadMethod]
    static void OnRuntimeMethodLoad()
    {
        Screen.SetResolution(1280, 720, false);
    }
}