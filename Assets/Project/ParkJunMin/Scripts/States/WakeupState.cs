using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WakeupState : PlayerState
{
    
    public WakeupState(PlayerController player) : base(player)
    {
        animationIndex = (int)PlayerController.State.WakeUp;
    }

    public override void Enter()
    {
        player.playerView.PlayAnimation(animationIndex);
        
    }

    public override void Update()
    {
        if(player.playerView.IsAnimationFinished())
        {
            player.ChangeState(PlayerController.State.Idle);
        }
    }
    public override void Exit()
    {
        player.playerModel.invincibility = false;
    }
}
