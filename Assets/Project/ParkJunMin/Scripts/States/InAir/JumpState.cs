using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : PlayerState
{
    private bool _hasJumped;
    private Vector2 _jumpDirection;
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
        player.maxFlightTime = 0.2f;
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

                if (player.isSlope) //&& player.isGrounded)
                {
                    // 법선벡터의 수직인 벡터를 곱해도 의미 없음
                    //player.rigid.velocity = player.perpAngle * new Vector2(player.rigid.velocity.x, player.highJumpForce);



                    // 지면에 수직인 법선벡터 방향으로 속도를 주는 방법 
                    player.rigid.velocity = (player.groundHit.normal.normalized) * player.highJumpForce;



                    //_jumpDirection = (player.groundHit.normal + Vector2.up).normalized;
                    //_jumpDirection.y = Mathf.Max(_jumpDirection.y, 0.5f);
                    //player.rigid.velocity = _jumpDirection * player.highJumpForce;

                    // 단순히 up 방향에 추가 오프셋값을 주는 방법
                    //_jumpDirection = Vector2.up.normalized;
                    //player.rigid.velocity = _jumpDirection * (player.highJumpForce + 10.0f);

                }
                else
                {
                    player.rigid.velocity = new Vector2(player.rigid.velocity.x, player.highJumpForce);
                }
                        //&& player.isGrounded)
                
                _hasJumped = false;

                //player.maxFlightTime = 0.2f;

            }
        }

        if (Input.GetKeyUp(KeyCode.Space) && _hasJumped)
        {
            // 낮은점프 실행
            if (player.isSlope) //&& player.isGrounded)
            {
                //player.rigid.velocity = player.perpAngle * new Vector2(player.rigid.velocity.x, player.lowJumpForce);



                // 지면에 수직인 법선벡터 방향으로 속도를 주는 방법 
                player.rigid.velocity = (player.groundHit.normal.normalized) * player.lowJumpForce;




                //_jumpDirection = (player.groundHit.normal + Vector2.up).normalized;
                //player.rigid.velocity = _jumpDirection * player.lowJumpForce;

                //_jumpDirection = (player.groundHit.normal + Vector2.up).normalized;
                //_jumpDirection.y = Mathf.Max(_jumpDirection.y, 0.5f);
                //player.rigid.velocity = _jumpDirection * player.lowJumpForce;

                // 단순히 up 방향에 추가 오프셋값을 주는 방법
                //_jumpDirection = Vector2.up.normalized;
                //player.rigid.velocity = _jumpDirection * (player.lowJumpForce + 10.0f);
            }
            else
            {
                player.rigid.velocity = new Vector2(player.rigid.velocity.x, player.lowJumpForce);
            }
            //&& player.isGrounded)

            _hasJumped = false;
            //player.jumpMaintainTime = 0.2f;
        }

        player.MoveInAir();

        if (player.maxFlightTime > 0)
        {
            player.maxFlightTime -= Time.deltaTime; // 이런거 오르막길에 의미없음
        }
        else if (player.rigid.velocity.y < 0)
        {
            //Debug.Log(player.rigid.velocity.y);
            player.ChangeState(PlayerController.State.Fall);
        }

        // 점프 상태에서 더블점프로 상태변환
        if (!player.isDoubleJumpUsed && Input.GetKeyDown(KeyCode.Space))
        {
            player.ChangeState(PlayerController.State.DoubleJump);
        }




        //if(player.isGrounded)
        //{
        //    player.ChangeState(PlayerController.State.Idle);
        //}
            
    }

    public override void Exit()
    {
        Debug.Log("점프 상태 종료");
    }

}
