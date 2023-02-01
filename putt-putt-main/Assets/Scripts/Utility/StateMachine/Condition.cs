using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility.StateMachine
{
    public abstract class Condition : ScriptableObject
    {
        public abstract bool Evaluate(StateMachine stateMachine);
    }
}
