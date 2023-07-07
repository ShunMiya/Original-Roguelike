/*using UnityEditor;
using UnityEngine;

namespace ItemSystem
{
    [CustomEditor(typeof(PlayerInventoryDataBase))]
    public class PlayerInventoryDataBaseEditor : Editor
    {
        private SerializedProperty inventoryProperty;

        private void OnEnable()
        {
            inventoryProperty = serializedObject.FindProperty(nameof(PlayerInventoryDataBase.inventory));
            if (inventoryProperty == null) Debug.Log("参照できてないよう");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            // NullReferenceExceptionの発生箇所より前に、serializedObjectの更新を行う

            EditorGUILayout.PropertyField(inventoryProperty, true);

            if (inventoryProperty.isExpanded)
            {
                EditorGUI.indentLevel++;

                for (int i = 0; i < inventoryProperty.arraySize; i++)
                {
                    SerializedProperty itemProperty = inventoryProperty.GetArrayElementAtIndex(i);
                    EditorGUILayout.PropertyField(itemProperty, true);
                }

                EditorGUI.indentLevel--;
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}*/