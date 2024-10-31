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

        prevNature = player.playerModel.curNature; // 스폰상태때 넣어주는것이 나음

        player.isDoubleJumpUsed = false;
        //Debug.Log("Idle 상태 진입");
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

        // 안미끄러지는건 아니고 훨씬 덜 미끄러짐.. y까지 0으로 해도 같음
        if (player.moveInput == 0)
        {
            player.rigid.velocity = new Vector2(0, player.rigid.velocity.y);
        }

    }

    public override void Exit()
    {
        
        //Debug.Log("Idle 상태 종료");
    }
}
