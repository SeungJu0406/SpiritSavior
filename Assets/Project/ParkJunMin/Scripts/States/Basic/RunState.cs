using UnityEngine;

namespace Project.ParkJunMin.Scripts.States.Basic
{
    public class RunState : PlayerState
    {
        public RunState(PlayerController player) : base(player)
        {
            animationIndex = (int)PlayerController.State.Run;
        }

        public override void Enter()
        {
            player.playerView.PlayAnimation(animationIndex);
            player.playerModel.RunPlayerEvent();
        }
        public override void Update()
        {
        
            PlayAnimationInUpdate();

            if (Mathf.Abs(player.rigid.velocity.x) < 0.01f && player.moveInput == 0)
            {
                player.ChangeState(PlayerController.State.Idle);
            }
            else if 
                ((player.coyoteTimeCounter <= 0 && player.rigid.velocity.y != 0f) 
                 || player.groundAngle > player.playerModel.maxAngle)
            {
                player.ChangeState(PlayerController.State.Fall);
            }

            else if (player.jumpBufferCounter > 0)
            {
                player.ChangeState(PlayerController.State.Jump);
            }

            player.CheckDashable();

        }

        private void Run()
        {
            player.moveInput = Input.GetAxisRaw("Horizontal");

            if (player.isSlope && (player.groundAngle < player.playerModel.maxAngle))
            {
                player.rigid.velocity = player.perpAngle * (player.playerModel.moveSpeed * player.moveInput * -1.0f);
            }
            else if (!player.isSlope && player.isGrounded)
            {
                player.rigid.velocity = new Vector2(player.moveInput * player.playerModel.moveSpeed, player.rigid.velocity.y);
            }
            player.FlipPlayer(player.moveInput);
        }

        public override void FixedUpdate()
        {
            Run();
        }

        public override void Exit()
        {

        }
    }
}
