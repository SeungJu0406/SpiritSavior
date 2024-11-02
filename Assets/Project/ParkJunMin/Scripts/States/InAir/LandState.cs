using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LandState : PlayerState
{
    public LandState(PlayerController player) : base(player)
    {
        animationIndex = (int)PlayerController.State.Land;
    }

    public override void Enter()
    {
        player.playerView.PlayAnimation(animationIndex);
        player.isDoubleJumpUsed = false;
    }

    public override void Update()
    {
        if (Input.anyKey)
        {
            player.ChangeState(PlayerController.State.Idle);
            return;
        }

        if (player.playerView.IsAnimationFinished())
        {
            player.ChangeState(PlayerController.State.Idle);
        }
    }

    public override void Exit()
    {
        
    }

    public override void FixedUpdate()
    {
        
    }

}
