using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpPointController : Warp
{
    [SerializeField] Transform warpLayout;
    [SerializeField] Material unActiveWarp;
    [SerializeField] Material ActiveWarp;
    private SpriteRenderer _spriteRenderer;
    private bool _inWarp;
    public bool warpActive;
    

    private void Start()
    {
        _inWarp = false;
        warpActive = false;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.material = unActiveWarp;
    }

    // warp point 접근확인
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _inWarp = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _inWarp = false;
        }
    }
    
    // warpActive : 워프 활성화
    // warpUIActive : 워프 UI 활성화(활성화된 워프로 향하는 button 활성화)
    public void Update()
    {
        if (_inWarp)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                // E키 입력받아 워프 활성화
                warpActive = true;
                _spriteRenderer.material = ActiveWarp;

                // E키 입력으로 워프 UI(또는 버튼) 조종 
                if (warpUIActive)
                {
                    warpUIActive = false;
                }
                else
                {
                    warpUIActive = true;
                }
            }
        }
        else // 워프포인트 밖으로 나오면 자동으로 UI 종료
        {
            warpUIActive = false;
        }
    }
}
