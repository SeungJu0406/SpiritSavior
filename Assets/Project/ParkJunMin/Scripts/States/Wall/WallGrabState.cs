using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGrabState : PlayerState
{
    
    public WallGrabState(PlayerController player) : base(player)
    {
        animationIndex = (int)PlayerController.State.WallGrab;
        //wallJump로 가기 위해선 WallGrab이 필수적이라 여기에 닮
        //wallGrab -> wallSliding -> wallJump 순서로 가야하기때문에 wallGrab에 닮.
        ability = PlayerModel.Ability.WallJump;
    }

    public override void Enter()
    {
        player.playerView.PlayAnimation(animationIndex);
        player.playerModel.GrabWallEvent();
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
    }

}
