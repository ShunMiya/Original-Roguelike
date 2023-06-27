/*using ItemSystem;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayerInventoryDataBase))]
public class PlayerInventoryDataBaseEditor : Editor
{
    private SerializedProperty inventoryProperty;

    private void OnEnable()
    {
        inventoryProperty = serializedObject.FindProperty("inventory");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(inventoryProperty, true);

        serializedObject.ApplyModifiedProperties();
    }
}*/