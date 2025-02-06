namespace Project.ParkJunMin.Scripts.States.Basic
{
    public class DeadState : PlayerState
    {
        public DeadState(PlayerController player) : base(player)
        {
            animationIndex = (int)PlayerController.State.Dead;
        }
        public override void Enter()
        {
            player.playerModel.hp = 0;
            player.playerView.PlayAnimation(animationIndex);
            player.playerModel.DiePlayerEvent();
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
}
