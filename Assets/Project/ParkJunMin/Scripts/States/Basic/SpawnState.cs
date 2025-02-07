namespace Project.ParkJunMin.Scripts.States.Basic
{
    public class SpawnState : PlayerState
    {
        public SpawnState(PlayerController player) : base(player)
        {
            animationIndex = (int)PlayerController.State.Spawn;
        }

        public override void Enter()
        {
            ResetPlayer();
            player.playerView.PlayAnimation(animationIndex);
        
            player.playerModel.SpawnPlayerEvent();
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

        }

        private void ResetPlayer()
        {
            prevNature = player.playerModel.curNature;
            player.playerModel.hp = player.playerModel.curMaxHp;
            player.playerModel.invincibility = false;
            player.transform.position = Manager.Game.RespawnPos;
        }

    }
}
