using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.StateMachine;

public class GameLoadedCondition : Condition
{
    private PlayerManager PlayerManager;

    public override bool Evaluate(StateMachine stateMachine)
    {
        if (PlayerManager == null) PlayerManager = stateMachine.GetCachedComponent<PlayerManager>();

        PlayerManager.NextTurn();

        return true;
    }
}
