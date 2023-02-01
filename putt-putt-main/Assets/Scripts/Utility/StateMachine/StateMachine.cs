using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility.StateMachine
{
    public class StateMachine : MonoBehaviour
    {
        private Dictionary<Type, Component> CachedComponentsByType = new Dictionary<Type, Component>();
        [SerializeField] private State InitialState;
        private State CurrentState;

        private void Awake()
        {
            CurrentState = InitialState;
        }

        private void Update()
        {
            var nextState = CurrentState.Run(this);

            if (nextState != null)
            {
                Enter(nextState);
            }
        }

        public void Enter(State nextState)
        {
            CurrentState.Exit();
            
            CurrentState = nextState;

            CurrentState.Enter();
        }

        public T GetCachedComponent<T>() where T : Component
        {
            var hasCachedComponent = CachedComponentsByType.TryGetValue(typeof(T), out Component cached);
            if (hasCachedComponent) return cached as T;

            var component = this.GetComponent<T>();
            if (component) return component;

            Debug.LogError($"{this.name} must have a {typeof(T)} component attached to it!");

            return null;
        }
    }
} 

