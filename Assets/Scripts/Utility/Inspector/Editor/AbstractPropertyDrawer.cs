//https://forum.unity.com/threads/editor-tool-better-scriptableobject-inspector-editing.484393/

using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEditor;
using System.Linq;

/// <summary>
/// Draws the property field for any field marked with AbstractAttribute.
/// </summary>
[CustomPropertyDrawer(typeof(AbstractAttribute), true)]
public class AbstractAttributeDrawer : PropertyDrawer
{
    private static Dictionary<Type, AbstractPropertyOption[]> OptionsForType = new Dictionary<Type, AbstractPropertyOption[]>();
 
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var serialisedObject = property.serializedObject;

        var abstractType = (attribute as AbstractAttribute).AbstractType;
        AbstractPropertyOption[] abstractTypeOptions = GetDropdownOptions(abstractType);

        var typeName = (!string.IsNullOrEmpty(property.managedReferenceFullTypename))
            ? property.managedReferenceFullTypename.Split(' ').Last()
            : "None";

        var newLabelText = $"{label.text} ({typeName})";

        Rect foldoutFieldRect = new Rect(position);
        foldoutFieldRect.height = EditorGUIUtility.singleLineHeight;
        position.y += foldoutFieldRect.height;

        property.isExpanded = EditorGUI.Foldout(foldoutFieldRect, property.isExpanded, newLabelText, true);
        if (!property.isExpanded) return;
    
        if (property.managedReferenceFullTypename == null)
        {
            var firstType = abstractTypeOptions[0].Type;
            var newReference = System.Activator.CreateInstance(firstType);
            property.managedReferenceValue = newReference;
            serialisedObject.ApplyModifiedProperties();
        }

        var currentOption = abstractTypeOptions
            .FirstOrDefault(option => option.FullTypeName == property.managedReferenceFullTypename);
        var currentOptionIndex = Array.IndexOf(abstractTypeOptions, currentOption);

        var dropDownOptions = abstractTypeOptions.Select(option => option.OptionContent).ToArray();

        Rect popupFieldRect = new Rect(position);
        popupFieldRect.height = EditorGUIUtility.singleLineHeight;

        var newTypeOptionIndex = EditorGUI.Popup(popupFieldRect, currentOptionIndex, dropDownOptions);

        if (currentOptionIndex != newTypeOptionIndex)
        {
            var newType = abstractTypeOptions[newTypeOptionIndex].Type;
            var newObjectReference = System.Activator.CreateInstance(newType);
            property.managedReferenceValue = newObjectReference;
            serialisedObject.ApplyModifiedProperties();
        }

        position.x += 8;
        position.xMax -= 8;
        position.y += EditorGUIUtility.singleLineHeight;
        position.y += EditorGUIUtility.standardVerticalSpacing;

        var marchingRect = new Rect(position);
        var propertyEnumerator = property.GetEnumerator();

        while (propertyEnumerator.MoveNext())
        {
            var childProperty = propertyEnumerator.Current as SerializedProperty;
            if (childProperty == null) continue;

            var propertyHeight = EditorGUI.GetPropertyHeight(childProperty, true);

            EditorGUI.PropertyField(position, property, true);

            position.y += propertyHeight;
            position.y += EditorGUIUtility.standardVerticalSpacing;
        }
 
        serialisedObject.ApplyModifiedProperties();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        var totalHeight = EditorGUIUtility.singleLineHeight;
        
        if (!property.isExpanded) return totalHeight;

        totalHeight += EditorGUIUtility.singleLineHeight;

        var propertyEnumerator = property.GetEnumerator();

        while (propertyEnumerator.MoveNext())
        {
            var childProperty = propertyEnumerator.Current as SerializedProperty;
            if (childProperty == null) continue;

            var propertyHeight = EditorGUI.GetPropertyHeight(childProperty, true);
            totalHeight += propertyHeight;
            totalHeight += EditorGUIUtility.standardVerticalSpacing;
        }

        return totalHeight;
    }

    public AbstractPropertyOption[] GetDropdownOptions(Type abstractType)
    {
        if (OptionsForType.ContainsKey(abstractType)) return OptionsForType[abstractType];

        var allAssembelies = System.AppDomain.CurrentDomain.GetAssemblies();
        var allTypes = allAssembelies.SelectMany(assembely => assembely.GetTypes());
        var dropDownOptions = allTypes
            .Where((type) =>
                abstractType.IsAssignableFrom(type)
                && type.IsClass
                && !type.IsAbstract)
            .Select(t => new AbstractPropertyOption(t))
            .ToArray();

        OptionsForType[abstractType] = dropDownOptions;

        return dropDownOptions;
    }
}