using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.StateMachine;

[CreateAssetMenu(menuName = "Data/States/Action/NextPlayerTurnAction")]
public class NextPlayerTurnAction : Action
{
    public override void Execute(StateMachine stateMachine)
    {
        var playerManager = stateMachine.GetCachedComponent<PlayerManager>();
        playerManager.NextTurn();
    }
}
