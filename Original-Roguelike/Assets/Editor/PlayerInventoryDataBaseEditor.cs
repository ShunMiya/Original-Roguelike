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
            if (inventoryProperty == null) Debug.Log("�Q�Ƃł��ĂȂ��悤");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            // NullReferenceException�̔����ӏ����O�ɁAserializedObject�̍X�V���s��

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