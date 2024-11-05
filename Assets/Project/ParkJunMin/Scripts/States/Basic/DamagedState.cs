using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

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
        if (!knockbackFlag && player.rigid.velocity.sqrMagnitude < 0.1f) // 넉백의 힘이 거의 사라졌을 때
        {
            if (player.playerModel.hp > 0)
            {
                player.ChangeState(PlayerController.State.WakeUp);
            }
            else
            {
                player.ChangeState(PlayerController.State.Dead);
            }
        }
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
        player.rigid.AddForce(knockbackDirection * player.knockbackForce, ForceMode2D.Impulse);

        knockbackFlag = false;
    }

}
