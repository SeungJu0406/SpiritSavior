using System.Collections;
using System.Collections.Generic;
using Project.ParkJunMin.Scripts;
using UnityEngine;

public class DoubleJumpState : PlayerState
{
    
    public DoubleJumpState(PlayerController player) : base(player)
    {
        animationIndex = (int)PlayerController.State.DoubleJump;
        ability = PlayerModel.Ability.DoubleJump;
    }

    public override void Enter()
    {
        player.playerView.PlayAnimation(animationIndex);
        player.playerModel.DoubleJumpPlayerEvent();

        Vector2 curVelocity = player.rigid.velocity;
        curVelocity.y = 0;
        player.rigid.velocity = curVelocity;
        player.rigid.AddForce(new Vector2(player.rigid.velocity.x,player.playerModel.doubleJumpForce),ForceMode2D.Impulse);
        player.isDoubleJumpUsed = true;
    }

    public override void Update()
    {
        PlayAnimationInUpdate();
        player.MoveInAir();

        if (player.rigid.velocity.y < 0)
        {
            player.ChangeState(PlayerController.State.Fall);
        }

        //Dash ���·� ��ȯ
        if (player.isDashUsed && Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("��� ��Ÿ�����Դϴ�.");
        }
        else if (!player.isDashUsed && Input.GetKeyDown(KeyCode.X))
        {
            player.ChangeState(PlayerController.State.Dash);
        }
    }

    public override void Exit()
    {

    }
}
