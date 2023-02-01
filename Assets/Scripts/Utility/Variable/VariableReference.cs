using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableReference<T>
{
    public bool UseConstant = true;
    public T ConstantValue;
    public ReferenceVariable<T> ReferenceVariable;

    public T Value
    {
        get => UseConstant ? ConstantValue : ReferenceVariable.Value;
        set 
        {
            if (UseConstant) return;
            ReferenceVariable.Value = value;
        }
    }

    public static implicit operator T(VariableReference<T> variableReference) => variableReference.Value;
}
