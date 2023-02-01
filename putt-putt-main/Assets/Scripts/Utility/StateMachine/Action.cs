using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility.StateMachine
{
    public abstract class Action : ScriptableObject
    {
        public abstract void Execute(StateMachine stateMachine);
    }
}
