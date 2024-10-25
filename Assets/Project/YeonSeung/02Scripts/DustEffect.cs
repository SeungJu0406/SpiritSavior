using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustEffect : MonoBehaviour
{
    // 인스펙터에서 추가해 주세요.
    public ParticleSystem dust;


    // CreateDust
    /* 나중에 플레이어에 더해서
     * Flip이나 착지 그렇때 작동되게 그쪽 함수에 추가
     */
    void CreateDust()
    {
        dust.Play();
    }

    void Start()
    {
        
    }


    void Update()
    {
        
    }
}
