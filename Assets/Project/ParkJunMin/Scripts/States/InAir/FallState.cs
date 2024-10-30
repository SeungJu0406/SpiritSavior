using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FallState : PlayerState
{
    private bool _isFalling;
    public FallState(PlayerController player) : base(player)
    {
        animationIndex = (int)PlayerController.State.Fall;
    }

    public override void Enter()
    {
        // 떨어질 때 빨리 떨어지게
        // 캐릭터가 하강중
        //Debug.Log("Fall 상태 진입");
        
        player.playerView.PlayAnimation(animationIndex);
        _isFalling = true;
        //player.rigid.gravityScale = 5;
        //player.rigid.velocity += Vector2.up * Physics2D.gravity.y * (player.jumpEndSpeed - 1) * Time.deltaTime;
    }
    
    public override void Update()
    {
        
        PlayAnimationInUpdate();
        player.MoveInAir();

        // 떨어지는 상태에서 더블점프로 상태변환 (더블점프를 안썼을 경우)
        if (!player.isDoubleJumpUsed && Input.GetKeyDown(KeyCode.Space))
        {
            player.ChangeState(PlayerController.State.DoubleJump);
        }

        if (player.isGrounded)
        {
            player.isDoubleJumpUsed = false;
            player.ChangeState(PlayerController.State.Idle);
        }
            

    }

    public override void Exit()
    {
        //Debug.Log("Fall 상태 종료");
        _isFalling = false;
       // player.rigid.gravityScale = 1;
    }
}
