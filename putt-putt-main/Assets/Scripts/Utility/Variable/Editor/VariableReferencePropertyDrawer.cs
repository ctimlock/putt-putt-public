using UnityEngine;
using UnityEditor;

public class VariableReferencePropertyDrawer<T> : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        EditorGUI.BeginProperty(position, label, property);

        var prefixLabelPosition = position;
        position = EditorGUI.PrefixLabel(prefixLabelPosition, GUIUtility.GetControlID(FocusType.Passive), label);

        position.x -= 20;

        position = CreateUseContextDropDownMenuButton(position, property, label);

        position = CreateVariableTextField(position, property, label);

        EditorGUI.EndProperty();
    }

    private Rect CreateUseContextDropDownMenuButton(Rect position, SerializedProperty property, GUIContent label)
    {
        var dropDownMenuButtonRect = new Rect(position.position, new Vector2(20, 20));
        var dropDownMenuButtonTexture = EditorGUIUtility.FindTexture("CustomTool");
        var dropDownMenuButtonGuiContent = new GUIContent(dropDownMenuButtonTexture);
        var dropDownMenuButtonGuiStyle = new GUIStyle()
        {
            fixedWidth = 50f,
            border = new RectOffset(1, 1, 1, 1),
        };
        var dropDownMenuButton = EditorGUI.DropdownButton(
            dropDownMenuButtonRect,
            dropDownMenuButtonGuiContent,
            FocusType.Keyboard,
            dropDownMenuButtonGuiStyle
        );

        if (dropDownMenuButton) CreateUseContextDropDownMenu(position, property, label);

        position.x += dropDownMenuButtonRect.width;
        return position;
    }

    private Rect CreateUseContextDropDownMenu(Rect position, SerializedProperty property, GUIContent label)
    {
        var useConstantValueProperty = property.FindPropertyRelative("UseConstant");
        var useConstantValue = useConstantValueProperty.boolValue;

        var genericMenu = new GenericMenu();
        
        var useConstantGuiContent = new GUIContent("Use Constant");
        var useConstantCallback = GetSetBooleanPropertyCallback(useConstantValueProperty, true);
        genericMenu.AddItem(useConstantGuiContent, useConstantValue, useConstantCallback);

        var useReferenceGuiContent = new GUIContent("Use Reference");
        var useReferenceCallback = GetSetBooleanPropertyCallback(useConstantValueProperty, false);
        genericMenu.AddItem(useReferenceGuiContent, !useConstantValue, useReferenceCallback);

        genericMenu.DropDown(position);

        return position;
    }

    private GenericMenu.MenuFunction GetSetBooleanPropertyCallback(SerializedProperty property, bool value)
    {
        return () => {
            property.boolValue = value;
            property.serializedObject.ApplyModifiedProperties();
        };
    }

    protected virtual Rect CreateVariableTextField(Rect position, SerializedProperty property, GUIContent label)
    {
        var useConstantProperty = property.FindPropertyRelative("UseConstant");
        var constantValueProperty = property.FindPropertyRelative("ConstantValue");
        var referenceVariableProperty = property.FindPropertyRelative("ReferenceVariable");

        var useConstantValue = useConstantProperty.boolValue;
        var constantValue = constantValueProperty.intValue;

        var variableTextFieldWidth = EditorGUIUtility.currentViewWidth - position.x - 5;
        Debug.Log(EditorGUIUtility.currentViewWidth - position.x - 400);
        var variableTextFieldRect = new Rect(position.position, new Vector2(variableTextFieldWidth, 20));

        if (useConstantProperty.boolValue)
        {
            var newValue = EditorGUI.TextField(variableTextFieldRect, constantValue.ToString());
            int.TryParse(newValue, out constantValue);
            constantValueProperty.intValue = constantValue;
        } else {
            EditorGUI.ObjectField(variableTextFieldRect, referenceVariableProperty, GUIContent.none);
        }

        return position;
    }
}
