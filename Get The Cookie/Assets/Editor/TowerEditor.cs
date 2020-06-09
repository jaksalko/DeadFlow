using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TowerGenerator))]
public class TowerEditor : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        TowerGenerator towerGenerator = (TowerGenerator)target;
        if(GUILayout.Button("MakeTower"))
        {
            towerGenerator.MakeTower();
        }
        if (GUILayout.Button("ClearTower"))
        {
            towerGenerator.ClearTower();
        }
        if (GUILayout.Button("Centralize"))
        {
            towerGenerator.Centralize();
        }
        if (GUILayout.Button("Up"))
        {
            towerGenerator.MoveUp();
        }
        if (GUILayout.Button("Left"))
        {
            towerGenerator.MoveLeft();
        }
        if (GUILayout.Button("Down"))
        {
            towerGenerator.MoveDown();
        }
        if (GUILayout.Button("Right"))
        {
            towerGenerator.MoveRight();
        }
    }
}
