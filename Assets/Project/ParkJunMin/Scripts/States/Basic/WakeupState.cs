namespace Project.ParkJunMin.Scripts.States.Basic
{
    public class WakeupState : PlayerState
    {
    
        public WakeupState(PlayerController player) : base(player)
        {
            animationIndex = (int)PlayerController.State.WakeUp;
        }

        public override void Enter()
        {
            player.playerView.PlayAnimation(animationIndex);
            player.playerModel.WakeUpEvent();
        }

        public override void Update()
        {
            if(player.playerView.IsAnimationFinished())
            {
                player.ChangeState(PlayerController.State.Idle);
            }
        }
        public override void Exit()
        {
            player.playerModel.invincibility = false;
        }
    }
}
