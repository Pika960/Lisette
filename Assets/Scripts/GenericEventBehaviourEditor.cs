#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[CustomEditor(typeof(GenericEventBehaviour))]
public class GenericEventBehaviourEditor : Editor
{
    override public void OnInspectorGUI()
    {
        var script = target as GenericEventBehaviour;

        serializedObject.Update();

        if (script.gameObject.name.Contains("DialogEvent"))
        {
            script.dialogResource = (TextAsset)EditorGUILayout.ObjectField("Dialog Asset", 
                script.dialogResource, typeof(TextAsset), true);
        }

        else if (script.gameObject.name.Contains("EncounterEvent"))
        {
            script.notificationResource = (TextAsset)EditorGUILayout.ObjectField("Notification Asset", 
                script.notificationResource, typeof(TextAsset), true);
        }

        else if (script.gameObject.name.Contains("ItemEvent"))
        {
            script.itemName = EditorGUILayout.TextField("Item Name", script.itemName);
            script.itemAmount = EditorGUILayout.IntField("Item Amount", script.itemAmount);
            script.dialogResource = (TextAsset)EditorGUILayout.ObjectField("Dialog Asset", 
                script.dialogResource, typeof(TextAsset), true);
            script.notificationResource = (TextAsset)EditorGUILayout.ObjectField("Notification Asset", 
                script.notificationResource, typeof(TextAsset), true);
        }

        else if (script.gameObject.name.Contains("DemoEndEvent"))
        {
            script.dialogResource = (TextAsset)EditorGUILayout.ObjectField("Dialog Asset", 
                script.dialogResource, typeof(TextAsset), true);
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(script);
            EditorSceneManager.MarkSceneDirty(script.gameObject.scene);
        }
    }
}
#endif
