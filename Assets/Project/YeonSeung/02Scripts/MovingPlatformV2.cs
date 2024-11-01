using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformV2 : SwichInteractable
{
    [Header("시작점")]
    public Transform pointA;
    [Header("도착점")]
    public Transform pointB;
    [Header("이동속도")]
    [SerializeField] float moveSpeed;
    [Header("기다리는 시간")]
    [SerializeField] float delay;

    private Vector3 nextPosition;

    private bool _isMoving;


    private Coroutine _delayMove;

    // 패트롤 기능 만들고 그걸 사용해야됨.



    public override void Interact()
    {
        if (_isMoving == false)
        {
            _isMoving = true;
        }
        else
        {
            _isMoving = false;
        }
        // _isMoving이 활성화 안되면 아마 안될것
        // 그전에 switch되나 확인차 했던것.
        // MovePlatform();
    }

    private void Awake()
    {
        _isMoving = false;
    }
    void Start()
    {
        nextPosition = pointB.position;
    }


    void Update()
    {
        // _isMoving이 활성화때만 움직이기
        /*
        if (_isMoving == true)
        {
            if (_delayMove == null)
            {
                
                
                MovePlatform();
            }
            else if (_delayMove != null)
            {
                Debug.Log("코루틴끝");
                StopCoroutine(_delayMove);
                _delayMove = null;
            }
        }
        else if (_isMoving == false)
        {
            
            RetreatPlatform();
        }
        */
        // Patrol
        // transform.position = Vector3.MoveTowards(transform.position, nextPosition, moveSpeed * Time.deltaTime);
        // if (transform.position == nextPosition)
        // {
        //     nextPosition = (nextPosition == pointA.position) ? pointB.position : pointA.position;
        // }
        Patrol();

    }
    IEnumerator DelayMove()
    {
        Debug.Log("Coroutine STARTS!");
        yield return new WaitForSeconds(delay);
       
        Debug.Log($"{delay}초 지남");
        // 여기서 활성화 해서 MovePlatform();
        _isMoving = true;
    }

    public void Patrol()
    {
        transform.position = Vector3.MoveTowards(transform.position, nextPosition, moveSpeed * Time.deltaTime);
        if (transform.position == nextPosition)
        {
            nextPosition = (nextPosition == pointA.position) ? pointB.position : pointA.position;
        }
    }

    /// <summary>
    /// 플레이어 움직이는 메서드
    /// </summary>
    public void MovePlatform()
    {
        // Debug.Log("start moving");
        transform.position = Vector3.MoveTowards(transform.position, nextPosition, moveSpeed * Time.deltaTime);
    }
    /// <summary>
    /// 처음 위치로 돌아가는 메서드
    /// </summary>
    public void RetreatPlatform()
    {
        // A(시작점)로 복귀
        transform.position = Vector3.MoveTowards(transform.position, pointA.position, moveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _delayMove = StartCoroutine(DelayMove());
            // _isMoving = true;
            collision.transform.SetParent(transform);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.parent = null;
            // 떨어지면 isMoving비활성화
            _isMoving = false;
            Debug.Log($"isMoving {_isMoving}");
        }
    }
}
