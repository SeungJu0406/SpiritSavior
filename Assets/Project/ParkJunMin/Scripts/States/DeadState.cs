using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : PlayerState
{
    public DeadState(PlayerController player) : base(player)
    {
    }

    public override void Enter()
    {
        animationIndex = (int)PlayerController.State.Dead;
        player.playerView.PlayAnimation(animationIndex);
        //player.playerModel.DiePlayer(); // 죽음시 이벤트 실행을 어디서할지는 추후 결정
    }

    public override void Update()
    {
        
    }

    public override void Exit()
    { 

    }
}
