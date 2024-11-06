using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformV2 : SwichInteractable
{
    [Header("순찰모드(자동이동)\n비활성시: 올라타야움직임")]
    [SerializeField] bool _isPatrol;
    
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

    [SerializeField] GameObject _platformFX;



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
       // _platformFX = GetComponent<GameObject>();
    }


    void Update()
    {
        // 패트롤 비활성시 
        if (_isPatrol == false)
        {
            if (_isMoving == true)
            {
                // _isMoving이 활성화때만 움직이기
                if (_delayMove == null)
                {
                
                
                    MovePlatform();
                }
                else if (_delayMove != null)
                {
                    //Debug.Log("코루틴끝");
                    StopCoroutine(_delayMove);
                    _delayMove = null;
                }
            }
            else if (_isMoving == false)
            {
            
                RetreatPlatform();
            }
        }
        
        // Patrol
        // transform.position = Vector3.MoveTowards(transform.position, nextPosition, moveSpeed * Time.deltaTime);
        // if (transform.position == nextPosition)
        // {
        //     nextPosition = (nextPosition == pointA.position) ? pointB.position : pointA.position;
        // }

        // 패트롤 활성시
        if(_isPatrol)
        {
            Patrol();
        }

    }
    IEnumerator DelayMove()
    {
       // Debug.Log("Coroutine STARTS!");
        yield return new WaitForSeconds(delay);
       
       // Debug.Log($"{delay}초 지남");
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
            Manager.Sound.PlaySFX(Manager.Sound.Data.PlatformOnSound);
            _platformFX.SetActive(false);
            _delayMove = StartCoroutine(DelayMove());
            // _isMoving = true;
            collision.transform.SetParent(transform);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Manager.Sound.PlaySFX(Manager.Sound.Data.PlatformOffSound);
            _platformFX.SetActive(true);
            collision.transform.SetParent(Manager.Game.RespawnPoint);
            // 떨어지면 isMoving비활성화
            _isMoving = false;
           // Debug.Log($"isMoving {_isMoving}");
        }
    }
}
