using System.Collections;
using System.Collections.Generic;
using Project.ParkJunMin.Scripts;
using UnityEngine;

public class DashState : PlayerState
{
    public DashState(PlayerController player) : base(player)
    {
        animationIndex = (int)PlayerController.State.Dash;
        ability = PlayerModel.Ability.Dash;
    }

    public override void Enter()
    {
        player.isDashUsed = true;
        player.dashDeltaTime = 0;
        player.playerView.PlayAnimation(animationIndex);
        player.playerModel.DashPlayerEvent();

        player.rigid.gravityScale = 0;

        //if (player.isSlope) // 일단 경사면에서 다르게 처리하게 바꿀경우를 대비해서 나눠놓음
        //{
        //    dashDirection = new Vector2(player.moveInput, 0);
        //    player.rigid.velocity = dashDirection * (player.dashForce);
        //}
        //else
        {
           
        }
    }

    public override void Update()
    {
        PlayAnimationInUpdate();
        
        if (player.playerView.IsAnimationFinished())
        {
            //Debug.Log("대시 애니메이션 종료");
            player.ChangeState(PlayerController.State.Fall);
        }
        //player.AdjustDash();
    }

    public override void FixedUpdate()
    {
        player.rigid.velocity = new Vector2(player.isPlayerRight * player.transform.localScale.x * player.playerModel.dashForce, 0f);
    }

    public override void Exit()
    {
        player.rigid.gravityScale = 1;
    }


}
