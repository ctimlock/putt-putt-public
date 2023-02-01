using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Utility.StateMachine
{
    [Serializable]
    public class Transition
    {
        public Condition Condition;
        public State TrueState;
        public State FalseState;

        public State Evaluate(StateMachine stateMachine)
        {
            var result = Condition.Evaluate(stateMachine);

            if (result && TrueState != null)
            {
                return TrueState;
            }
            
            if (!result && FalseState != null)
            {
                return FalseState;
            }

            return null;
        }
    }
}
