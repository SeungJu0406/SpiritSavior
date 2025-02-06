using System.Collections;
using System.Collections.Generic;
using Project.ParkJunMin.Scripts.States;
using UnityEngine;

public class WallSlidingState : PlayerState
{
    public WallSlidingState(PlayerController player) : base(player)
    {
        animationIndex = (int)PlayerController.State.WallSliding;
    }

    public override void Enter()
    {
        player.playerView.PlayAnimation(animationIndex);
        player.playerModel.SlideWallEvent();
        player.rigid.gravityScale = 0.2f;
    }

    public override void Update()
    {
        PlayAnimationInUpdate();

        // WallJump 상태로 전환
        if (Input.GetKeyDown(KeyCode.C))
        {
            player.ChangeState(PlayerController.State.WallJump);
        }

        if (!player.isWall)
            player.ChangeState(PlayerController.State.Fall);


        if(player.isGrounded)
            player.ChangeState(PlayerController.State.Idle);

    }

    public override void Exit()
    {
        player.rigid.gravityScale = 1f;
    }


}
