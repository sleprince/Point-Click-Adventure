using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement; //for marking scene dirty.

[CustomEditor(typeof(ItemAction))]
public class ItemActionEditor : Editor
{
    ItemAction source;
    SerializedProperty s_itemDatabase, s_giveItem, s_yesActions, s_noActions, s_amount;

    private void OnEnable()
    {
        source = (ItemAction)target;
        s_itemDatabase = serializedObject.FindProperty("itemDatabase");
        s_giveItem = serializedObject.FindProperty("giveItem");
        s_yesActions = serializedObject.FindProperty("yesActions");
        s_noActions = serializedObject.FindProperty("noActions");
        s_amount = serializedObject.FindProperty("amount");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(s_itemDatabase, new GUIContent("Item Database: ")); //render item database.

        if (source.ItemDatabase != null)
        {
            //draw the popup or enum for selecting items
            source.itemId = EditorGUILayout.Popup(source.itemId, source.ItemDatabase.ItemsNames.ToArray());

            EditorGUILayout.PropertyField(s_giveItem, new GUIContent("Give Item: "));

            //draw the item entry
            DrawItemEntry(source.CurrentItem);

            EditorExtensions.DrawActionsArray(s_yesActions, "Yes Actions: "); //new version with static function from extensions, arguments
            //are the serialized array of actions from ItemAction.cs,  and the label string for editorGUIlayour label field.

            //EditorGUILayout.PropertyField(s_yesActions, new GUIContent("Yes Actions: "), true); //this was just for testing out.

            EditorExtensions.DrawActionsArray(s_noActions, "No Actions: ");

            //EditorGUILayout.PropertyField(s_noActions, new GUIContent("No Actions: "), true);
        }

        if (GUI.changed)
        {
            if (source.ItemDatabase != null)
                source.ChangeItem(source.ItemDatabase.GetItem(source.itemId)); //get item based on itemID.

            EditorUtility.SetDirty(source); //dirty as not using the project's saved serialized field.
            EditorSceneManager.MarkSceneDirty(source.gameObject.scene); //when a change to the scene has taken place, using gameobject
            //where this scene resides. Makes it so whenever we change something in this GUI an asterisk will appear with the scene name
            //and we can opt to save the changes.
        }

        serializedObject.ApplyModifiedProperties();


    }

    void DrawItemEntry(Item item) //copied from inventory editor but delete button taken out.  //changed to Item instead of serialized
                                  //property because we are getting the CurrentItem directly from ItemAction not ItemDatabase.
    {


        GUILayout.BeginVertical("box");

        GUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("Item Id: " + item.ItemId.ToString(), GUILayout.Width(75f)); //again getting properties directly, much
        //simpler.
        EditorGUILayout.LabelField("Item Name: " + item.ItemName);

        GUILayout.EndHorizontal();

        EditorGUILayout.LabelField("Item Description: " + item.ItemDesc, GUILayout.Height(70f));
        GUILayout.Label(item.ItemDesc, EditorStyles.wordWrappedLabel); //so that the text word
        //wraps.

        GUILayout.BeginHorizontal();

        var spriteViewer = AssetPreview.GetAssetPreview(item.ItemSprite);
        GUILayout.Label(spriteViewer);

        if (item.AllowMultiple)
            EditorGUILayout.PropertyField(s_amount);

        GUILayout.EndHorizontal();

        GUILayout.EndVertical();
    }
}
