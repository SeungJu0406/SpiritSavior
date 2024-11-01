using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpState : PlayerState
{
    
    public DoubleJumpState(PlayerController player) : base(player)
    {
        animationIndex = (int)PlayerController.State.DoubleJump;
        ability = PlayerController.Ability.DoubleJump;
    }

    public override void Enter()
    {
        
        player.playerView.PlayAnimation(animationIndex);
        player.playerModel.DoubleJumpPlayerEvent();

        Vector2 curVelocity = player.rigid.velocity;
        curVelocity.y = 0;
        player.rigid.velocity = curVelocity;
        player.rigid.AddForce(new Vector2(player.rigid.velocity.x,player.doubleJumpForce),ForceMode2D.Impulse);
        player.isDoubleJumpUsed = true;
        
    }

    public override void Update()
    {
        PlayAnimationInUpdate();
        player.MoveInAir();

        if (player.rigid.velocity.y < 0)
        {
            //Debug.Log(player.rigid.velocity.y);
            player.ChangeState(PlayerController.State.Fall);
        }

        //Dash 상태로 전환
        if (player.isDashUsed && Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("대시 쿨타임중입니다.");
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
