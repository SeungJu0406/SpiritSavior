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

    public override void Interact()
    {
        // _isMoving이 활성화 안되면 아마 안될것
        // 그전에 switch되나 확인차 했던것.
        MovePlatform();
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
        if (_isMoving == true)
        {
            if (_delayMove == null)
            {
                _delayMove = StartCoroutine(DelayMove());
                MovePlatform();
            }
            else if (_delayMove != null)
            {
                StopCoroutine(_delayMove);
                Debug.Log("코루틴끝");
                _delayMove = null;
            }
        }
        else if (_isMoving == false)
        {
            RetreatPlatform();
        }

    }
    IEnumerator DelayMove()
    {
        Debug.Log("Coroutine STARTS!");
        yield return new WaitForSeconds(delay);
        Debug.Log($"{delay}후 Coroutine 끝");
    }

    public void MovePlatform()
    {
        transform.position = Vector3.MoveTowards(transform.position, nextPosition, moveSpeed * Time.deltaTime);
    }
    public void RetreatPlatform()
    {
        // A(시작점)로 복귀
        transform.position = Vector3.MoveTowards(transform.position, pointA.position, moveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _isMoving = true;
            collision.transform.SetParent(transform);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.parent = null;
            _isMoving = false;
            Debug.Log(_isMoving);
        }
    }
}
