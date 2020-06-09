using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CameraSetting))]
public class CameraEditor : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        CameraSetting cameraSetting = (CameraSetting)target;

        if(GUILayout.Button("F1"))
        {
            cameraSetting.CameraSpot(0);
        }
        if (GUILayout.Button("F2"))
        {
            cameraSetting.CameraSpot(1);
        }
        if (GUILayout.Button("F3"))
        {
            cameraSetting.CameraSpot(2);
        }

    }

}
