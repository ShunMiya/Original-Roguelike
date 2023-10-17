using UnityEngine;
public class SetScreenSize
{
    [RuntimeInitializeOnLoadMethod]
    static void OnRuntimeMethodLoad()
    {
        Debug.Log("After Scene is loaded and game is running");
        Screen.SetResolution(1280, 720, false);
    }
}