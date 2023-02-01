using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(BoolReference))]
public class BoolReferencePropertyDrawer : VariableReferencePropertyDrawer<bool>
{
    protected override Rect CreateVariableTextField(Rect position, SerializedProperty property, GUIContent label)
    {
        var useConstantProperty = property.FindPropertyRelative("UseConstant");
        var constantValueProperty = property.FindPropertyRelative("ConstantValue");
        var referenceVariableProperty = property.FindPropertyRelative("ReferenceVariable");

        var useConstantValue = useConstantProperty.boolValue;
        var constantValue = constantValueProperty.boolValue;

        var variableTextFieldWidth = EditorGUIUtility.currentViewWidth - position.x - 5;
        Debug.Log(EditorGUIUtility.currentViewWidth - position.x - 400);
        var variableTextFieldRect = new Rect(position.position, new Vector2(variableTextFieldWidth, 20));

        if (useConstantProperty.boolValue)
        {
            var newValue = EditorGUI.TextField(variableTextFieldRect, constantValue.ToString());
            bool.TryParse(newValue, out constantValue);
            constantValueProperty.boolValue = constantValue;
        } else {
            EditorGUI.ObjectField(variableTextFieldRect, referenceVariableProperty, GUIContent.none);
        }

        return position;
    }
}
