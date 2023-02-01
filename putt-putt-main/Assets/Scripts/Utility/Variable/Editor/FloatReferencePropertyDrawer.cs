using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(FloatReference))]
public class FloatReferencePropertyDrawer : VariableReferencePropertyDrawer<float>
{
    protected override Rect CreateVariableTextField(Rect position, SerializedProperty property, GUIContent label)
    {
        var useConstantProperty = property.FindPropertyRelative("UseConstant");
        var constantValueProperty = property.FindPropertyRelative("ConstantValue");
        var referenceVariableProperty = property.FindPropertyRelative("ReferenceVariable");

        var useConstantValue = useConstantProperty.boolValue;
        var constantValue = constantValueProperty.floatValue;

        var variableTextFieldWidth = EditorGUIUtility.currentViewWidth - position.x - 5;
        Debug.Log(EditorGUIUtility.currentViewWidth - position.x - 400);
        var variableTextFieldRect = new Rect(position.position, new Vector2(variableTextFieldWidth, 20));

        if (useConstantProperty.boolValue)
        {
            var newValue = EditorGUI.TextField(variableTextFieldRect, constantValue.ToString());
            float.TryParse(newValue, out constantValue);
            constantValueProperty.floatValue = constantValue;
        } else {
            EditorGUI.ObjectField(variableTextFieldRect, referenceVariableProperty, GUIContent.none);
        }

        return position;
    }
}
