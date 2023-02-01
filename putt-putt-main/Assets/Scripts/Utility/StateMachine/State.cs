using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility.StateMachine
{
    [CreateAssetMenu(menuName = "Data/States/State")]
    public class State : ScriptableObject
    {
        [SerializeField] public List<Action> Actions;
        [SerializeField] public List<Transition> Transitions;

        public virtual void Enter() {}

        public virtual void Exit() {}

        public State Run(StateMachine stateMachine)
        {
            if (Transitions.Count < 1) Debug.LogError($"{stateMachine.name}: State must have at least one Transition!");

            foreach (var action in Actions)
            {
                action.Execute(stateMachine);
            }

            foreach (var transition in Transitions)
            {
                var nextState = transition.Evaluate(stateMachine);

                if (nextState != null)
                {
                    return nextState;
                }
            }

            return null;
        }
    }
}
