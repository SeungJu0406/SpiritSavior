using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJumpState : PlayerState
{
    float wallJumpPower;
    public WallJumpState(PlayerController player) : base(player)
    {
        animationIndex = (int)PlayerController.State.WallJump;
        //wallJumpPower = player.wallJumpPower;
    }
    public override void Enter()
    {
        player.isWallJumpUsed = true;
        //player.Freeze();
        player.playerView.PlayAnimation(animationIndex);
        player.rigid.velocity = new Vector2(-player.isPlayerRight* player.wallJumpPower, 0.9f * player.wallJumpPower); //0.9는 점프조절 임시값
        player.FlipPlayer(player.isPlayerRight);
        //player.playerView.FlipRender(player.isPlayerRight);
        
    }

    public override void Update()
    {
        //player.playerView.IsAnimationFinished()
        
        //PlayAnimationInUpdate();

        //player.MoveInAir();

        if (player.isGrounded)
            player.ChangeState(PlayerController.State.Idle);

        //if(player.isWall)
        //    player.ChangeState(PlayerController.State.WallGrab);

        if(player.rigid.velocity.y < 0)
        {
            player.ChangeState(PlayerController.State.Fall);
        }

    }

    public override void Exit()
    {

    }

}
