using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustEffect : MonoBehaviour
{
    // 인스펙터에서 추가해 주세요.
    public ParticleSystem jumpDust;
    public ParticleSystem dJumpDust;
    public ParticleSystem movingDust;
    /// <summary>
    /// 먼지이펙트 
    /// 나중에 플레이어에 더해서
    /// Flip이나 착지 그렇때 작동되게 그쪽 함수에 추가
    /// 노션에 상세 적용방안 있음 참고.
    /// </summary>
    public void JumpDust()
    {
        jumpDust.Play();
    }
    public void DJumpDust()
    {
        dJumpDust.Play();
    }
    public void MovingDust()
    {
        movingDust.Play();
    }

    void Start()
    {
        
    }


    void Update()
    {
        
    }
}
