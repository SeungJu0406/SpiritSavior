using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// teg 0 : 땅 Ground 추가

public class PlayerMover : MonoBehaviour
{
    private float _moveSpeed = 10f;        // 이동속도
    private float _lowJumpForce = 10f;     // 낮은점프 힘
    private float _highJumpForce = 25f;    // 높은점프 힘
    private float _maxJumpTime = 0.2f;     // 최대점프 시간
    private float _jumpStartSpeed = 18f;   // 점프시작 속도
    private float _jumpEndSpeed = 10f;     // 점프종료 속도
    
    private Rigidbody2D _rigid;
    private bool _iGround = false;        // 캐릭터와 땅여부 체크
    private bool _iJump = false;          // 점프중인지여부 체크
    private float _spacebarTime = 0f;     // 스페이스바 누른시간 체크

    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 이동
        float moveInput = Input.GetAxis("Horizontal");
        _rigid.velocity = new Vector2(moveInput * _moveSpeed, _rigid.velocity.y);

        // 점프 스타트
        if (Input.GetKeyDown(KeyCode.Space) && _iGround)
        {
            _iJump = true;
            _spacebarTime = 0f;
            _rigid.velocity = new Vector2(_rigid.velocity.x, _lowJumpForce); // 1단점프
        }

        // 스페이스바를 누르는 동안 점프력 증가
        if (Input.GetKey(KeyCode.Space) && _iJump)
        {
            _spacebarTime += Time.deltaTime;

            if (_spacebarTime < _maxJumpTime && _rigid.velocity.y > 0)  // 상승 중 추가 점프력
            {
                float jumpForce = Mathf.Lerp(_lowJumpForce, _highJumpForce, _spacebarTime / _maxJumpTime);
                _rigid.velocity = new Vector2(_rigid.velocity.x, jumpForce);  // 점프 강도
            }
        }

        // 스페이스바 때면 점프 종료
        if (Input.GetKeyUp(KeyCode.Space))
        {
            _iJump = false;
        }

        // 점프 속도를 빠르게
        if (_rigid.velocity.y > 0)  // 캐릭터가 상승 중
        {
            _rigid.velocity += Vector2.up * Physics2D.gravity.y * (_jumpStartSpeed - 1) * Time.deltaTime;
        }

        // 떨어질때 빨리 떨어지게 
        if (_rigid.velocity.y < 0)  // 캐릭터가 하강 중
        {
            _rigid.velocity += Vector2.up * Physics2D.gravity.y * (_jumpEndSpeed - 1) * Time.deltaTime;
        }
    }

    // 태그 땅 체크
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _iGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _iGround = false;
        }
    }
}