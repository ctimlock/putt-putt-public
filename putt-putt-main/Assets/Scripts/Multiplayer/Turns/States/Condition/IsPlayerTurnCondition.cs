using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.StateMachine;

[CreateAssetMenu(menuName = "Data/States/Condition/IsPlayerTurn")]
public class IsPlayerTurnCondition : Condition
{
    public override bool Evaluate(StateMachine stateMachine)
    {
        var playerManager = stateMachine.GetCachedComponent<PlayerManager>();

        return playerManager.CurrentPlayerId != null;
    }
}
