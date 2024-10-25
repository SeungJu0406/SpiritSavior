using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : PlayerState
{
    
    private int idleAnimationIndex = (int)PlayerController.State.Idle;
    public IdleState(PlayerController player) : base(player)
    {
       
    }

    public override void Enter()
    {
        Debug.Log("Idle 상태 진입");
        player.playerView.PlayAnimation(idleAnimationIndex);

    }

    public override void Update()
    {
        if(Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0.1f)
        {
            player.ChangeState(PlayerController.State.Run);
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            player.ChangeState(PlayerController.State.Jump);
        }
    }

    public override void Exit()
    {
        Debug.Log("Idle 상태 종료");
    }
}
