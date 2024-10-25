using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : PlayerState
{
    private int idleAnimationIndex = (int)PlayerController.State.Run;
    public RunState(PlayerController player) : base(player)
    {

    }

    public override void Enter()
    {
        Debug.Log("Run 상태 진입");
        player.playerView.PlayAnimation(idleAnimationIndex);
        player.isGrounded = true;
    }
    public override void Update()
    {
        Run();

        // Idle 상태로 전환
        if (Mathf.Abs(player.rigid.velocity.x) < 0.01f)
        {
            player.ChangeState(PlayerController.State.Idle);
        }

        // Jump 상태로 전환
        if (Input.GetKeyDown(KeyCode.Space) && player.isGrounded) // 조건 나중에 뺄 수도 있음
        {
            player.ChangeState(PlayerController.State.Jump);
        }
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

        player.playerView.FlipRender(moveInput);
    }

    public override void Exit()
    {
        Debug.Log("Run 상태 종료");
    }
}
