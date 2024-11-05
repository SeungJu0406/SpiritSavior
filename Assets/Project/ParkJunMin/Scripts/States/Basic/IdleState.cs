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
        player.playerView.PlayAnimation(animationIndex);
    }

    public override void Update()
    {
        PlayAnimationInUpdate();
        player.moveInput = Mathf.Abs(Input.GetAxisRaw("Horizontal"));
        if (player.moveInput > 0 && player.isGrounded)
        {
            player.ChangeState(PlayerController.State.Run);
        }
        if (player.jumpBufferCounter > 0f)
        {
            player.ChangeState(PlayerController.State.Jump);
        }
        if (player.rigid.velocity.y != 0 && !player.isGrounded)
        {
            player.ChangeState(PlayerController.State.Fall);
        }
        if (player.rigid.velocity.y != 0 && !player.isGrounded)
        {
            player.ChangeState(PlayerController.State.Fall);
        }

        //ÈÎ¾À ´ú ¹Ì²ô·¯Áü
        if (player.moveInput == 0)
        {
            player.rigid.velocity = new Vector2(0, player.rigid.velocity.y);
        }
    }

    public override void Exit()
    {

    }
}
