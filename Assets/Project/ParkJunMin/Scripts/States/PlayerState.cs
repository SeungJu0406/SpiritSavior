using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace Project.ParkJunMin.Scripts.States
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class PlayerState : BaseState
    {
        protected readonly PlayerController player;
        protected PlayerModel.Nature prevNature;
        public PlayerModel.Ability ability = PlayerModel.Ability.None;
        protected int animationIndex;

        protected PlayerState(PlayerController player)
        {
            this.player = player;
        }

        /// <summary>
        /// ������Ʈ���� �Ӽ� ������ Ž���ϰ� �ִϸ��̼��� ����ϴ� �޼���
        /// </summary>
        protected void PlayAnimationInUpdate()
        {
            AnimatorStateInfo curAnimationState = player.playerView.animator.GetCurrentAnimatorStateInfo(0);
            float normalizedTime = curAnimationState.normalizedTime % 1;
            if (prevNature == player.playerModel.curNature) 
                return;
            player.playerView.PlayAnimation(animationIndex, normalizedTime);
            prevNature = player.playerModel.curNature;
        }
    }
}
