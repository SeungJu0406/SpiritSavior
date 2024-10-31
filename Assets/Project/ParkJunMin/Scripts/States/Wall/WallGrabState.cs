using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGrabState : PlayerState
{
    
    public WallGrabState(PlayerController player) : base(player)
    {
        animationIndex = (int)PlayerController.State.WallGrab;
    }

    public override void Enter()
    {
        //Debug.Log("WALLGRAB");
        player.isWallJumpUsed = false;
        player.playerView.PlayAnimation(animationIndex);
        //위치를 고정시켜줘야함 -> 중력을 받지 않게
        player.rigid.velocity = Vector2.zero;
        player.rigid.gravityScale = 0;
    }

    public override void Update()
    {
        PlayAnimationInUpdate();

        if (player.playerView.IsAnimationFinished())
        {
            player.ChangeState(PlayerController.State.WallSliding);
        }
    }

    public override void Exit()
    {
        //player.rigid.gravityScale = 1;
    }

}
