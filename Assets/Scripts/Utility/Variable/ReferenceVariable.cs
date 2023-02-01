using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ReferenceVariable<T> : ScriptableObject
{
    [InspectorName("Value")]
    public T DefaultValue;

    [HideInInspector]
    public T CurrentValue;

    [HideInInspector]
    public T Value { get => CurrentValue; set => CurrentValue = value;}

    public void OnEnable()
    {
        CurrentValue = DefaultValue;
    }
}
