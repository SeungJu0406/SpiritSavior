using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class RunState : PlayerState
{
    public RunState(PlayerController player) : base(player)
    {
        animationIndex = (int)PlayerController.State.Run;
    }

    public override void Enter()
    {
        //Debug.Log("Run 상태 진입");
        
        player.playerView.PlayAnimation(animationIndex);
        player.isGrounded = true;
    }
    public override void Update()
    {
        if(player.isGrounded)
        {
            Run();
        }
        else
        {
            player.ChangeState(PlayerController.State.Fall);
            //player.MoveInAir();
        }
        
        

        PlayAnimationInUpdate();
        // Idle 상태로 전환
        if (Mathf.Abs(player.rigid.velocity.x) < 0.01f)
        {
            player.ChangeState(PlayerController.State.Idle);
        }

        // Jump 상태로 전환
        if (Input.GetKeyDown(KeyCode.Space)) //&& player.isGrounded) // 조건 나중에 뺄 수도 있음
        {
            player.ChangeState(PlayerController.State.Jump);
        }

        ////Fall 상태로 전환 // 경사면은 어떻게?
        //if (player.rigid.velocity.y < 0)
        //{
        //    Debug.Log(player.rigid.velocity.y);
        //    player.ChangeState(PlayerController.State.Fall);
        //}

        //임시 피격 트리거
        //if (Input.GetKeyDown(KeyCode.O))
        //{
        //    player.ChangeState(PlayerController.State.Damaged);
        //}


    }

    private void Run()
    {
        float moveInput = Input.GetAxis("Horizontal");
        player.rigid.velocity = new Vector2(moveInput * player.moveSpeed, player.rigid.velocity.y);

        if (player.rigid.velocity.x > player.maxMoveSpeed)
        {
            player.rigid.velocity = new Vector2(player.maxMoveSpeed, player.rigid.velocity.y);
        }
        else if (player.rigid.velocity.x < -player.maxMoveSpeed)
        {
            player.rigid.velocity = new Vector2(-(player.maxMoveSpeed), player.rigid.velocity.y);
        }

        //Debug.Log(player.rigid.velocity);
        player.playerView.FlipRender(moveInput);
    }

    public override void Exit()
    {
        //Debug.Log("Run 상태 종료");
    }
}
