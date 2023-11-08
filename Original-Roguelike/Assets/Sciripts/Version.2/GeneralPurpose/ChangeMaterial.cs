using UnityEngine;

public class ChangeMaterial : MonoBehaviour
{
    public static void ChangeMaterials(GameObject Obj, string MaterialName)
    {
        if(Obj.GetComponentsInChildren<Renderer>() != null)
        Obj.GetComponent<Renderer>().material = (Material)Resources.Load("Materials/" + MaterialName);

        ChangeMaterialsInChildren(Obj.transform, MaterialName);
    }

    public static void ChangeMaterialsInChildren(Transform parent, string MaterialName)
    {
        if (parent.GetComponentsInChildren<Renderer>() == null) return;
        foreach (Transform child in parent)
        {
            if(child.CompareTag("Enemy"))
            child.GetComponent<Renderer>().material = (Material)Resources.Load("Materials/" + MaterialName);
        }
    }
}