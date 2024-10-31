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
        // ������ �� ���� ��������
        // ĳ���Ͱ� �ϰ���
        //Debug.Log("Fall ���� ����");
        
        player.playerView.PlayAnimation(animationIndex);
        _isFalling = true;
        //player.rigid.gravityScale = 5;
        //player.rigid.velocity += Vector2.up * Physics2D.gravity.y * (player.jumpEndSpeed - 1) * Time.deltaTime;
    }
    
    public override void Update()
    {
        
        PlayAnimationInUpdate();
        player.MoveInAir();

        // �������� ���¿��� ���������� ���º�ȯ (���������� �Ƚ��� ���)
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
        Debug.Log("Fall ���� ����");
        _isFalling = false;

        // ���� ���� ������ �����ϱ� ���� ��Ÿ�� �Ұ����� ������ 0���� �ְ�
        // �� ���¸� ����� �ٽ� ���󺹱� ���ְ������ 
        // ���� enter������ �� �־��ִ� ��� �ܿ� �� ���� ����� ������?
        
        player.rigid.sharedMaterial.friction = 0.6f;


        // player.rigid.gravityScale = 1;
    }
}
