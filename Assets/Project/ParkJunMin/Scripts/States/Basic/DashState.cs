using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : PlayerState
{
    Vector2 dashDirection;
    public DashState(PlayerController player) : base(player)
    {
        animationIndex = (int)PlayerController.State.Dash;
    }

    public override void Enter()
    {
        player.isDashUsed = true;
        player.dashDeltaTime = 0;
        player.playerView.PlayAnimation(animationIndex);
        player.playerModel.DashPlayerEvent();

        dashDirection = new Vector2(player.moveInput, 0);

        //player.rigid.MovePosition((Vector2)player.transform.position + dashDirection * player.dashForce);

        player.rigid.gravityScale = 0;
        dashDirection = new Vector2(player.moveInput, 0);
        player.rigid.velocity = dashDirection * player.dashForce;

        if (player.isSlope) // 일단 경사면에서 다르게 처리하게 바꿀경우를 대비해서 나눠놓음
        {
            //player.rigid.velocity = player.perpAngle * player.dashForce * player.moveInput * -1.0f;

            dashDirection = new Vector2(player.moveInput, 0);
            player.rigid.velocity = dashDirection * (player.dashForce);

            //player.rigid.velocity = player.perpAngle * player.dashForce * player.moveInput * -1.0f;

        }
        else
        {
            dashDirection = new Vector2(player.moveInput, 0);
            player.rigid.velocity = dashDirection * player.dashForce;
        }


        //slope시에는 힘을 다르게 줘야함
    }

    public override void Update()
    {
        if(player.playerView.IsAnimationFinished())
        {
            Debug.Log("대시 애니메이션 종료");
            player.rigid.gravityScale = 1;
            player.ChangeState(PlayerController.State.Fall);
        }

    }

    public override void Exit()
    {
    }


}
