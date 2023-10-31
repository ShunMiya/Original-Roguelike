using UnityEngine;
using UnityEngine.EventSystems;

public class SetFirstSelected : MonoBehaviour
{
    public GameObject initialSelectedObject;

    void Start()
    {
        EventSystem.current.SetSelectedGameObject(initialSelectedObject);
    }
}
