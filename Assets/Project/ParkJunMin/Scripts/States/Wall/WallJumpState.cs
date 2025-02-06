using UnityEngine;

namespace Project.ParkJunMin.Scripts.States.Wall
{
    public class WallJumpState : PlayerState
    {
        private float _wallJumpPower;
        public WallJumpState(PlayerController player) : base(player)
        {
            animationIndex = (int)PlayerController.State.WallJump;
        }
        public override void Enter()
        {
            player.rigid.gravityScale = 1;
            player.isDoubleJumpUsed = false;
            player.playerView.PlayAnimation(animationIndex);
            player.playerModel.JumpWallEvent();
            player.rigid.velocity = new Vector2(-player.isPlayerRight* player.playerModel.wallJumpPower, 1.5f * player.playerModel.wallJumpPower);
            player.FlipPlayer(player.isPlayerRight);
        }

        public override void Update()
        {
            if (player.isGrounded)
                player.ChangeState(PlayerController.State.Idle);

            if(Input.GetKeyDown(KeyCode.C))
            {
                player.ChangeState(PlayerController.State.DoubleJump);
            }

            if(player.rigid.velocity.y < -5.0f) // wallJump라는걸 알아보기 쉽게 하기 위함, 0으로 설정시 너무 빠른 fall로 전환
            { 
                player.ChangeState(PlayerController.State.Fall);
            }

        }

        public override void Exit()
        {

        }

    }
}
