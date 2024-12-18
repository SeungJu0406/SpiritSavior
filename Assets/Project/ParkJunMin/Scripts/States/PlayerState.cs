using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : BaseState
{
    public PlayerController player;
    protected PlayerModel.Nature prevNature;
    public PlayerModel.Ability ability = PlayerModel.Ability.None;
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
        AnimatorStateInfo curAnimationState = player.playerView.animator.GetCurrentAnimatorStateInfo(0);
        float normalizedTime = curAnimationState.normalizedTime % 1;
        if (prevNature != player.playerModel.curNature)
        {
            player.playerView.PlayAnimation(animationIndex, normalizedTime);
            prevNature = player.playerModel.curNature;
        }
    }
}
