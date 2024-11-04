using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : PlayerState
{
    Vector2 dashDirection;
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
        //if (player.isWall)
        //{
        //    Debug.Log("대시 중 벽 감지");
        //    if (!player.isGrounded)
        //    {
        //        Vector2 curPosition = player.transform.position;
        //        curPosition += (Vector2.up * 0.5f);
        //        player.transform.position = new Vector2(curPosition.x, curPosition.y); //, player.transform.position.z);
        //    }
        //}

        // 대시중 wallCheckRay에 groundLayer가 감지됐을 경우

        //if((player._groundLayerMask & (1 << player.wallHit.collider.gameObject.layer)) != 0)
        //{

        //}



        if (player.playerView.IsAnimationFinished())
        {
            Debug.Log("대시 애니메이션 종료");
            player.ChangeState(PlayerController.State.Fall);
        }

    }

    public override void FixedUpdate()
    {

    }

    public override void Exit()
    {
        player.rigid.gravityScale = 1;
    }


}
