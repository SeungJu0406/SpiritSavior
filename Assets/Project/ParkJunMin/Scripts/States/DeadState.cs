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
        //애니메이션 끝나고 자동 리스폰
    }

    public override void Update()
    {
        if(player.playerView.IsAnimationFinished())
        { 
            player.ChangeState(PlayerController.State.Spawn);
        }
            
    }

    public override void Exit()
    { 

    }
}
