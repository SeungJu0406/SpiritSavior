using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : PlayerState
{
    
    public IdleState(PlayerController player) : base(player)
    {
        animationIndex = (int)PlayerController.State.Idle;
    }

    public override void Enter()
    {
        player.rigid.velocity = Vector2.zero;

        prevNature = player.playerModel.curNature; // �������¶� �־��ִ°��� ����

        player.isDoubleJumpUsed = false;
        //Debug.Log("Idle ���� ����");
        player.playerView.PlayAnimation(animationIndex);
    }

    public override void Update()
    {
        PlayAnimationInUpdate();
        //if (prevNature != player.playerModel.curNature)
        //{
        //    player.playerView.PlayAnimation(animationIndex);
        //    prevNature = player.playerModel.curNature;
        //}
        player.moveInput = Mathf.Abs(Input.GetAxisRaw("Horizontal"));
        if (player.moveInput > 0)
        {
            player.ChangeState(PlayerController.State.Run);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.ChangeState(PlayerController.State.Jump);
        }

        //�ϴ� �ּ� �� ���ư�
        //if(!player.isGrounded && player.rigid.velocity.y < -0.1f)
        //{
        //    player.ChangeState(PlayerController.State.Fall);
        //}

        //�ȹ̲������°� �ƴϰ� �ξ� �� �̲�����..y���� 0���� �ص� ����
        //�ּ� ó���ص� �� ���� �̲����� �� ����
        if (player.moveInput == 0)
        {
            player.rigid.velocity = new Vector2(0, player.rigid.velocity.y);
        }

    }

    public override void Exit()
    {
        
        //Debug.Log("Idle ���� ����");
    }
}
