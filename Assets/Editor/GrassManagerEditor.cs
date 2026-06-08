using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GrassManager))]
public class GrassManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GrassManager manager = (GrassManager)target;

        GUILayout.Space(10);

        if (GUILayout.Button("Generate Grass"))
        {
            manager.GenerateGrass();
        }

        if (GUILayout.Button("Clear Grass"))
        {
            manager.ClearGrass();
        }
    }
}