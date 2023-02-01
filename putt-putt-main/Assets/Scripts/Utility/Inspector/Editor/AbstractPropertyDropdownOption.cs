using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractPropertyOption
{
    public Type Type;
    public GUIContent OptionContent;
    public string FullTypeName;

    public AbstractPropertyOption(Type type)
    {
        this.Type = type;
        this.OptionContent = new GUIContent(type.Name);
        this.FullTypeName = $"{type.Assembly.GetName().Name} {type.FullName}";
    }
}
