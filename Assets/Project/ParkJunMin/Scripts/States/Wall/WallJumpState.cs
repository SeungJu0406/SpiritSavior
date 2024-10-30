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
        player.isDoubleJumpUsed = false;
        //player.Freeze();
        player.playerView.PlayAnimation(animationIndex);
        player.rigid.velocity = new Vector2(-player.isPlayerRight* player.wallJumpPower, 1.5f * player.wallJumpPower); //0.9는 점프조절 임시값
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

        if(Input.GetKeyDown(KeyCode.Space))
        {
            player.ChangeState(PlayerController.State.DoubleJump);
        }
        //if(player.isWall)
        //    player.ChangeState(PlayerController.State.WallGrab);

        if(player.rigid.velocity.y < -5.0f)
        {
            player.ChangeState(PlayerController.State.Fall);
        }

    }

    public override void Exit()
    {

    }

}
