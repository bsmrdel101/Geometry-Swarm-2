using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Enemy))]
public class EnemyEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Enemy enemy = (Enemy)target;

        switch (enemy.HitboxType)
        {
            case HitboxType.Box:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("BoxCol"), new GUIContent("Collider Dimensions"));
                break;
            case HitboxType.Poly:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("PolyColPoints"), new GUIContent("Collider Points"), true);
                break;
        }
        serializedObject.ApplyModifiedProperties();
    }
}
