using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJumpState : PlayerState
{
    public WallJumpState(PlayerController player) : base(player)
    {
        animationIndex = (int)PlayerController.State.WallJump;
    }
    public override void Enter()
    {
        player.playerView.PlayAnimation(animationIndex);
        
    }

    public override void Update()
    {
        PlayAnimationInUpdate();
    }

    public override void Exit()
    {

    }

}
