using UnityEditor;
using UnityEngine;

// From https://answers.unity.com/questions/489942/how-to-make-a-readonly-property-in-inspector.html, user  It3ration
// ex: [ReadOnly] public string a;
// ex: [ReadOnlyAttribute, SerializeField] private int b;

public class ReadOnlyAttribute : PropertyAttribute { }

[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property,
        GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, label, true);
    }

    public override void OnGUI(Rect position,
        SerializedProperty property,
        GUIContent label)
    {
        GUI.enabled = false;
        EditorGUI.PropertyField(position, property, label, true);
        GUI.enabled = true;
    }
}