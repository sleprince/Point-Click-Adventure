using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ActivateActions))]
public class ActivateActionsEditor : Editor
{
    SerializedProperty customGOList; //because it is a serialize field.

    private void OnEnable()
    {
        customGOList = serializedObject.FindProperty("customGameObjects");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        if (GUILayout.Button("Add Entry")) //add button, add new element to list.
        {
            customGOList.InsertArrayElementAtIndex(customGOList.arraySize);
        }

        DrawCustomObjectFields(customGOList);

        serializedObject.ApplyModifiedProperties();
    }

    void DrawCustomObjectFields(SerializedProperty customList)
    {
        for (int i = 0; i < customList.arraySize; i++)
        {
            GUILayout.BeginHorizontal("box");
            
            //getting the elements from the list.
            EditorGUILayout.PropertyField(customList.GetArrayElementAtIndex(i).FindPropertyRelative("gO"), new GUIContent("GameObject:")); //is label.

            GUILayout.BeginVertical(GUILayout.Width(25f));
            EditorGUILayout.PropertyField(customList.GetArrayElementAtIndex(i).FindPropertyRelative("activeStatus"), GUIContent.none ,GUILayout.Width(25f));
            //GUIContent.none is so that no label is rendered.

            if (GUILayout.Button("x", GUILayout.Width(20f))) //delete button, delete element from list.
            {
                customList.DeleteArrayElementAtIndex(i);
            }
            GUILayout.EndVertical();

            GUILayout.EndHorizontal();
        }
    }
}
