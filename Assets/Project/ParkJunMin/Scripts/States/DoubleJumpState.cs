using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpState : PlayerState
{
    public DoubleJumpState(PlayerController player) : base(player)
    {
    }

    public override void Enter()
    {
        animationIndex = (int)PlayerController.State.DoubleJump;
        player.playerView.PlayAnimation(animationIndex);
        base.Enter();
    }

    public override void Update()
    {

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
