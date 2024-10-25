using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : PlayerState
{
    private int idleAnimationIndex = (int)PlayerController.State.Jump;
    public JumpState(PlayerController player) : base(player)
    {
    }

    public override void Enter()
    {
        Debug.Log("점프 상태 진입");
        player.playerView.PlayAnimation(idleAnimationIndex);
        player.isJumped = true;
        player.jumpChargingTime = 0f;
        player.rigid.velocity = new Vector2(player.rigid.velocity.x, player.lowJumpForce); // 1단점프
    }

    public override void Update()
    {                                                                        // 스페이스바를 누르는 동안 점프력 증가
        if (Input.GetKey(KeyCode.Space) && player.isJumped)
        {
            player.jumpChargingTime += Time.deltaTime;

            if (player.jumpChargingTime < player.maxJumpTime && player.rigid.velocity.y > 0)  // 상승 중 추가 점프력
            {
                float jumpForce = Mathf.Lerp(player.lowJumpForce, player.highJumpForce, player.jumpChargingTime / player.maxJumpTime);
                player.rigid.velocity = new Vector2(player.rigid.velocity.x, jumpForce);  // 점프 강도
            }
            else
            {
                player.isJumped = false;
            }
        }

        // 스페이스바 때면 점프 종료
        if (Input.GetKeyUp(KeyCode.Space))
        {
            player.isJumped = false;
        }

        // 점프 속도를 빠르게
        if (player.rigid.velocity.y > 0)  // 캐릭터가 상승 중
        {
            player.rigid.velocity += Vector2.up * Physics2D.gravity.y * (player.jumpStartSpeed - 1) * Time.deltaTime;
        }

        player.MoveInAir();


        if (player.rigid.velocity.y < 0)
        {
            Debug.Log(player.rigid.velocity.y);
            player.ChangeState(PlayerController.State.Fall);
        }
            
    }

    public override void Exit()
    {
        Debug.Log("점프 상태 종료");
    }

}
