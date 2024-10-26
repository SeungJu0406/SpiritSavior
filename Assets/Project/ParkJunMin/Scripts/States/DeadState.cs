using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : PlayerState
{
    public DeadState(PlayerController player) : base(player)
    {
    }

    public override void Enter()
    {
        animationIndex = (int)PlayerController.State.Dead;
        player.playerView.PlayAnimation(animationIndex);
        player.playerModel.DiePlayer();
    }

    public override void Update()
    {
        
    }

    public override void Exit()
    { 

    }
}
