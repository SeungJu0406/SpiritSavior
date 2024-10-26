using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : BaseState
{
    public PlayerController player;
    protected PlayerModel.Nature prevNature;
    public int animationIndex;
    public PlayerState(PlayerController player)
    {
        this.player = player;
    }

    /// <summary>
    /// 업데이트에서 속성 변경을 탐지하고 애니메이션을 재생하는 메서드
    /// </summary>
    protected void PlayAnimationInUpdate()
    {
        if (prevNature != player.playerModel.curNature)
        {
            player.playerView.PlayAnimation(animationIndex);
            prevNature = player.playerModel.curNature;
        }
        //애니메이션 진행 상황에 맞춰 다음 진행 될 애니메이션이 진행되면 좋을거같음
    }
}
