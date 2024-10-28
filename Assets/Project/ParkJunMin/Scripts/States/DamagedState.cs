using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagedState : PlayerState
{
    private float _knockbackForce;
    public DamagedState(PlayerController player, float knockbackForce) : base(player)
    {
        this._knockbackForce = knockbackForce;
    }

    public override void Enter()
    {
        Debug.Log("피격 상태 진입");

        //넉백될 방향 정의
        //한 축만 넉백될지 두 축 모두 넉백될지는 추후 결정
        Vector2 knockbackDirection = -player.rigid.velocity.normalized;
        //기존의 운동량 초기화
        player.rigid.velocity = Vector2.zero;

        //무적상태 
        player.invincibility = true;
        

        animationIndex = (int)PlayerController.State.Damaged;
        player.playerView.PlayAnimation(animationIndex);
        player.playerModel.TakeDamage(1); // 일단 무조건 1 데미지

        //피격시 넉백
        
        player.rigid.AddForce(knockbackDirection * _knockbackForce, ForceMode2D.Impulse);

    }

    public override void Update()
    {
        // 피격상태가 끝나는걸 확인
        if(player.rigid.velocity.magnitude < 0.1f) // 넉백의 힘이 거의 사라졌을 때
        {
            player.invincibility = false;

            if (player.playerModel.hp > 0)
                player.ChangeState(PlayerController.State.Idle);
            else
                player.ChangeState(PlayerController.State.Dead);
        }

    }

    public override void Exit()
    {
        Debug.Log("피격 상태 탈출");
    }
}
