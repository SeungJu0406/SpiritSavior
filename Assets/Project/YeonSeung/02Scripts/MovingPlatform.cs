using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : Trap
{
    [SerializeField] GameObject _movingPlatform;
    PolygonCollider2D _movingPlatformCollider;

    // [SerializeField] float _movingTime; // 시간동안 움직이다 사라지는 버전 쓰게된다면
    [Header("움직임관련 \t 속도: ")]
    [SerializeField] float _movingSpeed;
    [Header("움직일 거리: ")]
    [SerializeField] float _movingDistance;
    private float _movingDirection;

    [Header("좌우 = 빈칸 / 위아래 = 체크")]
    [SerializeField] bool _fourWay;
    [Header("왼쪽 = 빈칸 / 오른쪽 = 체크")]
    [SerializeField] bool _horizontal; // F = 왼쪽(-) T = 오른쪽(+)
    [Header("아래 = 빈칸 / 위 = 체크")]
    [SerializeField] bool _vertical; // F = 왼쪽(-) T = 오른쪽(+)


    Vector2 finalTarget;
    Vector2 target;
    private void Awake()
    {
        //_movingPlatform = GetComponent<GameObject>();
        _movingPlatformCollider = GetComponent<PolygonCollider2D>();

        if (_fourWay == false) // F = 좌우
        {

            // 좌우 움직임 Check
            if (_horizontal == false)
            {
                _movingDirection = -1;
            }
            else
            {
                _movingDirection = 1;
            }
        }
        else if (_fourWay == true)// 위아래
        {
            // 위아래 움직임 Check
            if (_vertical == false)
            {
                _movingDirection = -1;
            }
            else
            {
                _movingDirection = 1;
            }
        }
        else
            return;


     //   // 좌우 움직임 Check
     //   if (_horizontal == false)
     //   {
     //       _movingDirection = -1;
     //   }
     //   else
     //   {
     //       _movingDirection = 1;
     //   }

     //   // 위아래 움직임 Check
     //   if (_vertical == false)
     //   {
     //       _movingDirection = -1;
     //   }
     //   else
     //   {
     //       _movingDirection = 1;
     //   }



        SetDestinationX();
        SetDestinationY();

    }
    void Start()
    {
        
    }


    void Update()
    {
        
        _movingPlatform.transform.position = Vector2.MoveTowards(transform.position, target, _movingSpeed * Time.deltaTime);
    }
    private void SetDestination()
    {
        // Left or Right
        _movingDistance = _movingDirection * _movingDistance + transform.position.x;
        target = new Vector2(_movingDistance, transform.position.x);

    }
    private void SetDestinationX()
    {
        // Left or Right
        _movingDistance = _movingDirection * _movingDistance + transform.position.x;
        target = new Vector2(_movingDistance, transform.position.x);
    }

    private void SetDestinationY()
    {

        // UP or Down
        _movingDistance = _movingDirection * _movingDistance + transform.position.y;
        target = new Vector2(_movingDistance, transform.position.y);
    }


    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);

        if(collision.gameObject.tag == "Player")
        {
            // 밟으면(충돌인지되면) 부모 하에 둬서 이동 같이.
            collision.transform.SetParent(transform);

            // 밟고 1초후에 없어지는 나중에 뭐 여러 종류 발판늘리게되거나 하면
            // Destroy(gameObject,1f);
        }
    }
    protected void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            // 접촉끝나면 자식해제
            collision.transform.SetParent(null);
        }

    }

}
