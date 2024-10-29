using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpState : PlayerState
{
    
    public DoubleJumpState(PlayerController player) : base(player)
    {
        animationIndex = (int)PlayerController.State.DoubleJump;
    }

    public override void Enter()
    {
        
        player.playerView.PlayAnimation(animationIndex);
        player.playerModel.DoubleJumpPlayerEvent();
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
    }

    public override void Exit()
    {

    }
}
