using UnityEngine;

namespace Project.ParkJunMin.Scripts.States.Basic
{
    public class DamagedState : PlayerState
    {
        private Vector2 knockbackDirection;
        private bool knockbackFlag;
        public DamagedState(PlayerController player) : base(player)
        {
            animationIndex = (int)PlayerController.State.Damaged;
        }

        public override void Enter()
        {
            knockbackFlag = true;

            player.playerModel.invincibility = true;
        
            player.playerView.PlayAnimation(animationIndex);
        }

        public override void Update()
        {
            // 피격상태가 끝나는걸 확인
            if (knockbackFlag || !(player.rigid.velocity.sqrMagnitude < 0.1f)) 
                return; 
            player.ChangeState(player.playerModel.hp > 0 ? PlayerController.State.WakeUp : PlayerController.State.Dead);
        }

        public override void FixedUpdate()
        {
            if(knockbackFlag)
                KnockbackPlayer();
        }

        public override void Exit()
        {
            knockbackDirection = Vector2.zero;
        }

        private void KnockbackPlayer()
        {
            //넉백될 방향 정의
            knockbackDirection = -player.rigid.velocity.normalized;

            //기존의 운동량 초기화
            player.rigid.velocity = Vector2.zero;

            //넉백
            player.rigid.AddForce(knockbackDirection * player.playerModel.knockbackForce, ForceMode2D.Impulse);

            knockbackFlag = false;
        }

    }
}
