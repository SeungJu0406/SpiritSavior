using UnityEngine;

public class JumpState : PlayerState
{
    private bool _hasJumped;
    private Vector2 _velocityDirection;
    private float _slopeDetectionDelayTimer = 0.2f;
    public JumpState(PlayerController player) : base(player)
    {
        animationIndex = (int)PlayerController.State.Jump;
    }

    public override void Enter()
    {
        //Debug.Log("점프 상태 진입");
        //player.rigid.sharedMaterial.friction = 0f;
        player.playerView.PlayAnimation(animationIndex);
        player.playerModel.JumpPlayerEvent();
        _hasJumped = true;

        player.jumpChargingTime = 0f;

        //player.maxFlightTime = 0.2f;

        //player.rigid.velocity = new Vector2(player.rigid.velocity.x, player.lowJumpForce); // 1단점프
    }

    public override void Update()
    {
        PlayAnimationInUpdate();

        //if (player.coyoteTimeCounter > 0f)
        //{

        // JumpVer1();

        JumpVer2();
        //}

        player.MoveInAir();

        //if (player.maxFlightTime > 0)
        //{
        //    player.maxFlightTime -= Time.deltaTime; // 이런거 오르막길에 의미없음
        //}
        // 점프 상태에서 더블점프로 상태변환
        if (!player.isDoubleJumpUsed && Input.GetKeyDown(KeyCode.C))
        {
            player.ChangeState(PlayerController.State.DoubleJump);
        }

        ////Dash 상태로 전환
        //player.CheckDashable();

        if(player.rigid.velocity.y < 0)
        {
            player.ChangeState(PlayerController.State.Fall);
        }


        // 점프에서 바로 idle로 전환됨
        //if (player.isGrounded)
        //{
        //    player.ChangeState(PlayerController.State.Idle);
        //}
    }

    public override void FixedUpdate()
    {

        //if (player.isGrounded && !player.isSlope)
        //{
        //    player.ChangeState(PlayerController.State.Idle);
        //}

        // slope면 이미 바닥이라는 얘기
        // 바닥인데 y축 속도가 일정값 이상으로 계속 증가하고 있다 = 경사면에서 비스듬히 계속 올라가고있다

        ///////////////////////////
        ///
        //if (player.isSlope)
        //{
        //    if (_slopeDetectionDelayTimer > 0)
        //    {
        //        // 점프 후 일정시간동안은 탐지하지 않음
        //        // 점프가 불가능할 정도로 빠르게 탐지하는것을 방지
        //        _slopeDetectionDelayTimer -= Time.fixedDeltaTime;
        //    }
        //    else
        //    {
        //        _velocityDirection = player.rigid.velocity.normalized;

        //        if (player.rigid.velocity.y < 0)
        //            player.ChangeState(PlayerController.State.Fall);

        //        // 
        //        // 벡터의 방향이 같은지 확인
        //        // 일정 경사 내에선 잘 작동하는거같은데 아직 확실치 않다 오류가 좀 많다 개선사항이 필요
        //        float alignment = Vector2.Dot(_velocityDirection, player.perpAngle);

        //        if (alignment > 0.98f || alignment < -0.98f)
        //        {
        //            //Debug.Log(_velocityDirection);
        //            //Debug.Log(player.perpAngle);
        //            //Debug.Log(alignment);
        //            player.ChangeState(PlayerController.State.Fall);
        //        }
        //    }
        //}

        //if (!player.isSlope && player.rigid.velocity.y < 0)
        //{
        //    player.ChangeState(PlayerController.State.Fall);
        //}

    }

    public override void Exit()
    {
        _slopeDetectionDelayTimer = 0.2f;
        _velocityDirection = Vector2.zero;
        player.jumpChargingTime = 0;
        //player.rigid.sharedMaterial.friction = 0.6f;
    }

    private void JumpVer1()
    {
        if (Input.GetKey(KeyCode.C) && _hasJumped) // 스페이스바를 누르는 동안 점프력 증가
        {
            player.jumpChargingTime += Time.deltaTime;
            if (player.jumpChargingTime >= player.jumpCirticalPoint)
            {
                // 높은점프로 결정될만큼 스페이스바를 임계 시간 이상 누른경우

                if (player.isSlope) //&& player.isGrounded)
                {
                    // 법선벡터의 수직인 벡터를 곱해도 의미 없음
                    //player.rigid.velocity = player.perpAngle * new Vector2(player.rigid.velocity.x, player.highJumpForce);



                    // 지면에 수직인 법선벡터 방향으로 속도를 주는 방법 
                    player.rigid.velocity = (player.groundHit.normal.normalized) * (player.highJumpForce + player.slopeJumpBoost);

                    //경사면이 아닐 경우와 같은 방법으로 속도를 주는 방법
                    // player.rigid.velocity = new Vector2(player.rigid.velocity.x, player.highJumpForce);


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

        if (Input.GetKeyUp(KeyCode.C) && _hasJumped)
        {
            // 낮은점프 실행
            if (player.isSlope) //&& player.isGrounded)
            {
                //player.rigid.velocity = player.perpAngle * new Vector2(player.rigid.velocity.x, player.lowJumpForce);



                // 지면에 수직인 법선벡터 방향으로 속도를 주는 방법 
                player.rigid.velocity = (player.groundHit.normal.normalized) * (player.lowJumpForce + player.slopeJumpBoost);

                //경사면이 아닐 경우와 같은 방법으로 속도를 주는 방법
                //player.rigid.velocity = new Vector2(player.rigid.velocity.x, player.lowJumpForce);



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
    }

    private void JumpVer2()
    {
        if (player.coyoteTimeCounter > 0f && Input.GetKey(KeyCode.C)) //player.coyoteTimeCounter > 0f && 
        {
            Debug.Log("Flag 1");
            player.rigid.velocity = new Vector2(player.rigid.velocity.x, player.highJumpForce);
            player.coyoteTimeCounter = 0f;
        }
    }
}
