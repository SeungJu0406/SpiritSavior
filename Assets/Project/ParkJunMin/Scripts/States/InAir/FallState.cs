using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FallState : PlayerState
{
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
                if (player.groundAngle < player.maxAngle) // 플레이어가 오를 수 있는 경사면 일 경우
                {
                    player.ChangeState(PlayerController.State.Land);
                }
                else
                {
                    // 오를 수 없는 경사면일 경우 미끄러짐
                    if(player.rigid.velocity.y >= 0) // 다 미끄러졌으면
                    {
                        player.ChangeState(PlayerController.State.Land);
                    }
                }
            }
            else
            {
                // 평지일 경우
                player.ChangeState(PlayerController.State.Land);
            }
        }

    }

    public override void Exit()
    {

    }
}
