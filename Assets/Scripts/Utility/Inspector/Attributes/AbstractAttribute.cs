using System;
using UnityEngine;
 
/// <summary>
/// Use this property on an Abstract Class to allow the editors drawing the field to allow for
/// choice and serialisation of child classes in the inspector. 
/// </summary>
public class AbstractAttribute : PropertyAttribute
{
    public Type AbstractType;

    public AbstractAttribute(Type type)
    {
        this.AbstractType = type; 
    }
}