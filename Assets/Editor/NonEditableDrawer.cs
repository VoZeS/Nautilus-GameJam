using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(NonEditableAttribute))]
public class NonEditableDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        GUI.enabled = false;
        EditorGUI.PropertyField(position, property, label);
        GUI.enabled = true;
    }
}
