using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(FloorGenerator))]
public class FloorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        FloorGenerator floorGenerator = (FloorGenerator)target;
        if (GUILayout.Button("Generate Floor"))
        {
            floorGenerator.MakeFloor();
        }

        if (GUILayout.Button("Clear Floor"))
        {
            floorGenerator.ClearFloor();
        }


    }
}
