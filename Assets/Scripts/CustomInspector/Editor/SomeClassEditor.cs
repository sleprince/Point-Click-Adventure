using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor; //so that we can make custom editor.

[CustomEditor(typeof(SomeClass))]
public class SomeClassEditor : Editor
{
    SomeClass source;
    SerializedProperty playerName, speed, playerPosition, playerPrefabs; //required for the serialized private fields.

    private void OnEnable()
    {
        source = (SomeClass)target; //getting the public fields from the class

        playerName = serializedObject.FindProperty("s_playerName"); //finding each of the serialized private fields.
        speed = serializedObject.FindProperty("s_speed");
        playerPosition = serializedObject.FindProperty("s_playerPosition");
        playerPrefabs = serializedObject.FindProperty("s_playerPrefabs");
    }

    public override void OnInspectorGUI() //overrides the standard inspector so that it doesn't appear.
    {
        //base.OnInspectorGUI(); //this commented out code would load the standard inspector.

        GUILayout.BeginVertical("box"); //defining the custom inspector GUI group. Box argument makes a box around the group.
        source.playerName = EditorGUILayout.TextField("Player Name: ",source.playerName);
        source.speed = EditorGUILayout.FloatField(source.speed);
        source.playerPosition = EditorGUILayout.Vector3Field("Player Position: ",source.playerPosition); //can have it like transform field.
        source.playerPrefabs = (GameObject)EditorGUILayout.ObjectField(source.playerPrefabs, typeof(GameObject), true); //only works with (GameObject) at
        //start. True is allowSceneObjects, so you can use objects in the scene as well as those in asset folder.
        GUILayout.EndVertical();

        GUILayout.BeginVertical("box");
        EditorGUILayout.PropertyField(playerName, new GUIContent("Player Name: ")); //new GUIContent is to put custom labels otherwise it puts the variable
                                                                                    ////name.
        EditorGUILayout.PropertyField(speed, new GUIContent("Player Speed: "));
        EditorGUILayout.PropertyField(playerPosition, new GUIContent("Player Position: "));
        EditorGUILayout.PropertyField(playerPrefabs, new GUIContent("Player Prefabs: "));
        GUILayout.EndVertical();

        if (GUILayout.Button("Randomize Speed")) //creates a custom button in the inspector.
        {
            speed.floatValue = Random.Range(5f, 25f);
            //or for public you can put source.speed = Random.Range(5f, 25f);
        }

        serializedObject.ApplyModifiedProperties(); //**without this any changes made in inspector would not be saved.
    }
}
