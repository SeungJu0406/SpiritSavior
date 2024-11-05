using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FallState : PlayerState
{
    //private bool _isFalling;
    public FallState(PlayerController player) : base(player)
    {
        animationIndex = (int)PlayerController.State.Fall;
    }

    public override void Enter()
    {
        player.playerView.PlayAnimation(animationIndex);
    }
    
    public override void Update()
    {
        PlayAnimationInUpdate();
        player.MoveInAir();

        // 떨어지는 상태에서 더블점프로 상태변환 (더블점프를 안썼을 경우)
        if (!player.isDoubleJumpUsed && Input.GetKeyDown(KeyCode.C))
        {
            player.ChangeState(PlayerController.State.DoubleJump);
        }
        
        if(player.isGrounded)
        {
            if (player.isSlope)
            {
                // 경사인데 플레이어가 오를 수 있는 경사일 경우
                if (player.groundAngle < player.maxAngle)
                {
                    player.ChangeState(PlayerController.State.Land);
                }
                else
                {
                    // 오를 수 없는 경사에서 다 미끄러졌을 경우
                    if(player.rigid.velocity.y >= 0) 
                    {
                        player.ChangeState(PlayerController.State.Land);
                    }
                }
            }
            else
            {
                // 경사가 아닌 바닥이면 Land
                player.ChangeState(PlayerController.State.Land);
            }
        }
        //if (player.isGrounded)// && player.rigid.velocity.y < -0.01f) //player.coyoteTimeCounter > 0) //
        //{
        //    player.ChangeState(PlayerController.State.Land);
        //}
    }

    public override void Exit()
    {
    }
}
