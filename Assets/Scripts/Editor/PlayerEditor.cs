using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Player))]
public class PlayerEditor : Editor
{
    SerializedProperty maxSpeed;
    float savedMaxSpeed;
    SerializedProperty speed;
    float savedSpeed;
    SerializedProperty jumpPower;
    float savedJumpPower;
    SerializedProperty moveJumpFactor;
    float savedMoveJumpFactor;

    private void OnEnable()
    {
        maxSpeed = serializedObject.FindProperty("maxSpeed");
        speed = serializedObject.FindProperty("speed");
        jumpPower = serializedObject.FindProperty("jumpPower");
        moveJumpFactor = serializedObject.FindProperty("moveJumpFactor");
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUILayout.BeginHorizontal();
        {
            bool reset = GUILayout.Button("ResetValues", GUILayout.Width(100f));
            if (reset) {
                maxSpeed.floatValue = 0;
                speed.floatValue = 0;
                jumpPower.floatValue = 0;
                moveJumpFactor.floatValue = 0;
            }
            bool save = GUILayout.Button("SaveValues", GUILayout.Width(100f));
            if (save)
            {
                savedMaxSpeed = maxSpeed.floatValue;
                savedSpeed = speed.floatValue;
                savedJumpPower = jumpPower.floatValue;
                savedMoveJumpFactor = moveJumpFactor.floatValue;
            }
            bool load = GUILayout.Button("LoadValues", GUILayout.Width(100f));
            if (load)
            {
                maxSpeed.floatValue = savedMaxSpeed;
                speed.floatValue = savedSpeed;
                jumpPower.floatValue = savedJumpPower;
                moveJumpFactor.floatValue = savedMoveJumpFactor;
            }
        }
        EditorGUILayout.EndHorizontal();
        serializedObject.ApplyModifiedProperties();
    }
}
