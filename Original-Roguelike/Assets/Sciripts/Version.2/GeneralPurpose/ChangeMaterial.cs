using UnityEngine;

public class ChangeMaterial : MonoBehaviour
{
    public static void ChangeMaterials(GameObject Obj, string MaterialName)
    {

        Obj.GetComponent<Renderer>().material = (Material)Resources.Load("Materials/" + MaterialName);

        ChangeMaterialsInChildren(Obj.transform, MaterialName);
    }

    public static void ChangeMaterialsInChildren(Transform parent, string MaterialName)
    {
        if (parent.GetComponentsInChildren<Renderer>() == null) return;
        foreach (Renderer childRenderer in parent.GetComponentsInChildren<Renderer>())
        {
            childRenderer.material = (Material)Resources.Load("Materials/" + MaterialName);
        }
    }
}