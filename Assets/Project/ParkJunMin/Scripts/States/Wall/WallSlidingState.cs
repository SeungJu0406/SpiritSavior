using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSlidingState : PlayerState
{
    public WallSlidingState(PlayerController player) : base(player)
    {
        animationIndex = (int)PlayerController.State.WallSliding;
    }

    public override void Enter()
    {
        player.playerView.PlayAnimation(animationIndex);
        player.rigid.gravityScale = 0.2f; // 임시값 ,
        //업데이트에서 slidingSpeed만큼 계속 y속도를 주는게 나을까 그냥 중력값변경이 나을까
    }

    public override void Update()
    {
        PlayAnimationInUpdate();

        // WallJump 상태로 전환
        if (Input.GetKeyDown(KeyCode.C))
        {
            player.ChangeState(PlayerController.State.WallJump);
        }

        if(player.isGrounded)
            player.ChangeState(PlayerController.State.Idle);

    }

    public override void Exit()
    {
        player.rigid.gravityScale = 1f;
    }


}
