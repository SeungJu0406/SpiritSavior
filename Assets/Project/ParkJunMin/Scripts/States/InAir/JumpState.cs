using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : PlayerState
{
    private bool _hasJumped;
    public JumpState(PlayerController player) : base(player)
    {
        animationIndex = (int)PlayerController.State.Jump;
    }

    public override void Enter()
    {
        //Debug.Log("점프 상태 진입");
        
        player.playerView.PlayAnimation(animationIndex);
        player.playerModel.JumpPlayerEvent();
        _hasJumped = true;
        player.jumpChargingTime = 0f;
        //player.rigid.velocity = new Vector2(player.rigid.velocity.x, player.lowJumpForce); // 1단점프
    }

    public override void Update()
    {   
        PlayAnimationInUpdate();
        if (Input.GetKey(KeyCode.Space) && _hasJumped) // 스페이스바를 누르는 동안 점프력 증가
        {
            player.jumpChargingTime += Time.deltaTime;
            if(player.jumpChargingTime >= player.jumpCirticalPoint)
            {
                // 높은점프로 결정될만큼 스페이스바를 임계 시간 이상 누른경우
                player.rigid.velocity = new Vector2(player.rigid.velocity.x, player.highJumpForce);
                _hasJumped = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space) && _hasJumped)
        {
            // 낮은점프 실행
            player.rigid.velocity = new Vector2(player.rigid.velocity.x, player.lowJumpForce);
            _hasJumped = false;
        }

        player.MoveInAir();

        // 점프 상태에서 더블점프로 상태변환
        if (!player.isDoubleJumpUsed && Input.GetKeyDown(KeyCode.Space))
        {
            player.ChangeState(PlayerController.State.DoubleJump);
        }


        if (player.rigid.velocity.y < 0)
        {
            //Debug.Log(player.rigid.velocity.y);
            player.ChangeState(PlayerController.State.Fall);
        }

        //if(player.isGrounded)
        //{
        //    player.ChangeState(PlayerController.State.Idle);
        //}
            
    }

    public override void Exit()
    {
        //Debug.Log("점프 상태 종료");
    }

}
