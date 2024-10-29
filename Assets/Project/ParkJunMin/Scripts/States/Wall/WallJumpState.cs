using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJumpState : PlayerState
{
    float wallJumpPower;
    public WallJumpState(PlayerController player) : base(player)
    {
        animationIndex = (int)PlayerController.State.WallJump;
        wallJumpPower = player.wallJumpPower;
    }
    public override void Enter()
    {
        player.playerView.PlayAnimation(animationIndex);
        player.rigid.velocity = new Vector2(-player.isPlayerRight, 0.9f * wallJumpPower); //0.9는 점프조절 임시값
        player.playerView.FlipRender(player.isPlayerRight);
        
    }

    public override void Update()
    {
        //player.playerView.IsAnimationFinished()

        //PlayAnimationInUpdate();

        //player.MoveInAir();


    }

    public override void Exit()
    {

    }

}
