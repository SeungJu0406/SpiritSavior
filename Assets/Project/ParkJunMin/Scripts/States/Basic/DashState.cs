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

        if (player.isSlope) // �ϴ� ���鿡�� �ٸ��� ó���ϰ� �ٲܰ�츦 ����ؼ� ��������
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


        //slope�ÿ��� ���� �ٸ��� �����
    }

    public override void Update()
    {
        if(player.playerView.IsAnimationFinished())
        {
            Debug.Log("��� �ִϸ��̼� ����");
            player.rigid.gravityScale = 1;
            player.ChangeState(PlayerController.State.Fall);
        }

    }

    public override void Exit()
    {
    }


}