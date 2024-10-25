using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FallState : PlayerState
{
    private float _raycastDistance = 0.5f;
    private float _groundedrayHitDistance = 0.2f;
    private bool _isFalling;
    public FallState(PlayerController player) : base(player)
    {
    }

    public override void Enter()
    {
        // 떨어질 때 빨리 떨어지게
        // 캐릭터가 하강중
        Debug.Log("Fall 상태 진입");
        _isFalling = true;
        player.rigid.velocity += Vector2.up * Physics2D.gravity.y * (player.jumpEndSpeed - 1) * Time.deltaTime;
    }
    
    public override void Update()
    {
        //RaycastHit2D rayHit = Physics2D.Raycast(player.rigid.position, Vector2.down, raycastDistance);
        //Debug.DrawRay(player.rigid.position, Vector2.down * raycastDistance, Color.red);
        //if (rayHit.collider != null)
        //{
        //    if (rayHit.collider != null && rayHit.distance < groundedrayHitDistance)
        //    {
        //        player.ChangeState(PlayerController.State.Idle);
        //    }
        //}
        if(player.isGrounded)
            player.ChangeState(PlayerController.State.Idle);

    }

    public override void Exit()
    {
        Debug.Log("Fall 상태 종료");
        _isFalling = false;
    }
}
