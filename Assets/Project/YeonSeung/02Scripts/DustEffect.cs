using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustEffect : MonoBehaviour
{
    // �ν����Ϳ��� �߰��� �ּ���.
    public ParticleSystem dust;


    // CreateDust
    /* ���߿� �÷��̾ ���ؼ�
     * Flip�̳� ���� �׷��� �۵��ǰ� ���� �Լ��� �߰�
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
