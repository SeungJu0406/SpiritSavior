using System.Collections;
using System.Collections.Generic;
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
        //Debug.Log("���� ���� ����");
        
        player.playerView.PlayAnimation(animationIndex);
        player.playerModel.JumpPlayerEvent();
        _hasJumped = true;
        player.jumpChargingTime = 0f;

        //player.maxFlightTime = 0.2f;

        //player.rigid.velocity = new Vector2(player.rigid.velocity.x, player.lowJumpForce); // 1������
    }

    public override void Update()
    {   
        PlayAnimationInUpdate();
        if (Input.GetKey(KeyCode.Space) && _hasJumped) // �����̽��ٸ� ������ ���� ������ ����
        {
            player.jumpChargingTime += Time.deltaTime;
            if(player.jumpChargingTime >= player.jumpCirticalPoint)
            {
                // ���������� �����ɸ�ŭ �����̽��ٸ� �Ӱ� �ð� �̻� �������

                if (player.isSlope) //&& player.isGrounded)
                {
                    // ���������� ������ ���͸� ���ص� �ǹ� ����
                    //player.rigid.velocity = player.perpAngle * new Vector2(player.rigid.velocity.x, player.highJumpForce);



                    // ���鿡 ������ �������� �������� �ӵ��� �ִ� ��� 
                    player.rigid.velocity = (player.groundHit.normal.normalized) * (player.highJumpForce + player.slopeJumpBoost);

                    //������ �ƴ� ���� ���� ������� �ӵ��� �ִ� ���
                    // player.rigid.velocity = new Vector2(player.rigid.velocity.x, player.highJumpForce);


                    //_jumpDirection = (player.groundHit.normal + Vector2.up).normalized;
                    //_jumpDirection.y = Mathf.Max(_jumpDirection.y, 0.5f);
                    //player.rigid.velocity = _jumpDirection * player.highJumpForce;

                    // �ܼ��� up ���⿡ �߰� �����°��� �ִ� ���
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
            // �������� ����
            if (player.isSlope) //&& player.isGrounded)
            {
                //player.rigid.velocity = player.perpAngle * new Vector2(player.rigid.velocity.x, player.lowJumpForce);



                // ���鿡 ������ �������� �������� �ӵ��� �ִ� ��� 
                player.rigid.velocity = (player.groundHit.normal.normalized) * (player.lowJumpForce + player.slopeJumpBoost);

                //������ �ƴ� ���� ���� ������� �ӵ��� �ִ� ���
                //player.rigid.velocity = new Vector2(player.rigid.velocity.x, player.lowJumpForce);



                //_jumpDirection = (player.groundHit.normal + Vector2.up).normalized;
                //player.rigid.velocity = _jumpDirection * player.lowJumpForce;

                //_jumpDirection = (player.groundHit.normal + Vector2.up).normalized;
                //_jumpDirection.y = Mathf.Max(_jumpDirection.y, 0.5f);
                //player.rigid.velocity = _jumpDirection * player.lowJumpForce;

                // �ܼ��� up ���⿡ �߰� �����°��� �ִ� ���
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

        //if (player.maxFlightTime > 0)
        //{
        //    player.maxFlightTime -= Time.deltaTime; // �̷��� �������濡 �ǹ̾���
        //}
        // ���� ���¿��� ���������� ���º�ȯ
        if (!player.isDoubleJumpUsed && Input.GetKeyDown(KeyCode.Space))
        {
            player.ChangeState(PlayerController.State.DoubleJump);
        }

        //if(player.isGrounded)
        //{
        //    player.ChangeState(PlayerController.State.Idle);
        //}
            
    }

    public override void FixedUpdate()
    {
        // slope�� �̹� �ٴ��̶�� ���
        // �ٴ��ε� y�� �ӵ��� ������ �̻����� ��� �����ϰ� �ִ� = ���鿡�� �񽺵��� ��� �ö󰡰��ִ�
        if (player.isSlope) 
        {
            if(_slopeDetectionDelayTimer > 0)
            {
                // ���� �� �����ð������� Ž������ ����
                // ������ �Ұ����� ������ ������ Ž���ϴ°��� ����
                _slopeDetectionDelayTimer -= Time.fixedDeltaTime;
            }
            else
            {
                _velocityDirection = player.rigid.velocity.normalized;
                //Vector2 slopeDirection = player.groundHit.normal.normalized; //player.perpAngle;

                // ������ ������ ������ Ȯ��
                float alignment = Vector2.Dot(_velocityDirection, player.perpAngle);

                if (alignment > 0.98f || alignment < -0.98f)
                {
                    Debug.Log(_velocityDirection);
                    Debug.Log(player.perpAngle);
                    Debug.Log(alignment);
                    player.ChangeState(PlayerController.State.Fall);
                }
            }

        }

        if (!player.isSlope && player.rigid.velocity.y < 0)
        {
            //Debug.Log(player.rigid.velocity.y);
            player.ChangeState(PlayerController.State.Fall);
        }
    }

    public override void Exit()
    {
        Debug.Log("���� ���� ����");
        _slopeDetectionDelayTimer = 0.2f;
        _velocityDirection = Vector2.zero;
    }

}
