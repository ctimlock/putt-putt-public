using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(StringReference))]
public class StringReferencePropertyDrawer : VariableReferencePropertyDrawer<string>
{
    protected override Rect CreateVariableTextField(Rect position, SerializedProperty property, GUIContent label)
    {
        var useConstantProperty = property.FindPropertyRelative("UseConstant");
        var constantValueProperty = property.FindPropertyRelative("ConstantValue");
        var referenceVariableProperty = property.FindPropertyRelative("ReferenceVariable");

        var useConstantValue = useConstantProperty.boolValue;
        var constantValue = constantValueProperty.stringValue;

        var variableTextFieldWidth = EditorGUIUtility.currentViewWidth - position.x - 5;
        var variableTextFieldRect = new Rect(position.position, new Vector2(variableTextFieldWidth, 20));

        if (useConstantProperty.boolValue)
        {
            var newValue = EditorGUI.TextField(variableTextFieldRect, constantValue.ToString());
            constantValueProperty.stringValue = newValue;
        } else {
            EditorGUI.ObjectField(variableTextFieldRect, referenceVariableProperty, GUIContent.none);
        }

        return position;
    }
}
