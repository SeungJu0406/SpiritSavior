using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ElevatorPlatform : MonoBehaviour
{
    [SerializeField] GameObject _elevatingPlatform;
    PolygonCollider2D _elePlatformCollider;

    // [SerializeField] float _movingTime; // 시간동안 움직이다 사라지는 버전 쓰게된다면
    [Header("움직임관련 \t 속도: ")]
    [SerializeField] float _movingSpeed;
    [Header("움직일 거리: ")]
    [SerializeField] float _movingDistance;

    [Header("좌우 = 빈칸 / 위아래 = 체크")]
    [SerializeField] bool _fourWay;
    [Header("왼쪽 = 빈칸 / 오른쪽 = 체크")]
    [SerializeField] bool _horizontal; // F = 왼쪽(-) T = 오른쪽(+)
    [Header("아래 = 빈칸 / 위 = 체크")]
    [SerializeField] bool _vertical; // F = 왼쪽(-) T = 오른쪽(+)


    private float _movingDistanceX;
    private float _movingDistanceY;

    private float _movingDirectionX;
    private float _movingDirectionY;

//    private bool _startMoving = true;
//    private bool _alreadyMoving = false;


    Vector2 originalLocation;

    Vector2 targetY;
    Vector2 targetX;
    private void Awake()
    {
        //_movingPlatform = GetComponent<GameObject>();
        _elePlatformCollider = GetComponent<PolygonCollider2D>();

        originalLocation = transform.position;

        // 좌우 움직임 Check
        if (_horizontal == false)
        {
            _movingDirectionX = -1;
        }
        else if (_horizontal == true)
        {
            _movingDirectionX = 1;
        }
        // 위아래 움직임 Check
        if (_vertical == false)
        {
            _movingDirectionY = -1;
        }
        else if (_vertical == true)
        {
            _movingDirectionY = 1;
        }
    }
    void Start()
    {
        SetDestinationX();
        SetDestinationY();
        Debug.Log($"{originalLocation}");
    }


    void Update()
    {


     //   if (_fourWay == false)
     //   {
     //       _elevatingPlatform.transform.position = Vector2.MoveTowards(transform.position, targetX, _movingSpeed * Time.deltaTime);
     //   }
     //   else if (_fourWay == true)
     //   {
     //       _elevatingPlatform.transform.position = Vector2.MoveTowards(transform.position, targetY, _movingSpeed * Time.deltaTime);
     //   }

    }
    private void MoveHorizontal()
    {
        _elevatingPlatform.transform.position = Vector2.Lerp(transform.position, targetX, _movingSpeed * Time.deltaTime);
    }
    private void MoveVertical()
    {
        _elevatingPlatform.transform.position = Vector2.Lerp(transform.position, targetY, _movingSpeed * Time.deltaTime);
    }


    private void SetDestinationX()
    {
        // Left or Right
        _movingDistanceX = _movingDirectionX * _movingDistance + transform.position.x;
        targetX = new Vector2(_movingDistanceX, transform.position.y);
    }

    private void SetDestinationY()
    {
        // UP or Down
        _movingDistanceY = _movingDirectionY * _movingDistance + transform.position.y;
        targetY = new Vector2(transform.position.x, _movingDistanceY);
    }




    private void OnCollisionEnter2D(Collision2D collision)
    {
       

        if (collision.gameObject.tag == "Player")
        {
            // 밟으면(충돌인지되면) 부모 하에 둬서 이동 같이.
            MoveHorizontal();
            //collision.transform.SetParent(transform);


            // 밟고 1초후에 없어지는 나중에 뭐 여러 종류 발판늘리게되거나 하면
            // Destroy(gameObject,1f);
            
        }
    }
  //  private void OnCollisionExit2D(Collision2D collision)
  //  {
  //      if (collision.gameObject.tag == "Player")
  //      {
  //          // 접촉끝나면 자식해제
  //          collision.transform.SetParent(null);
  //          _elevatingPlatform.transform.position = Vector2.MoveTowards(transform.position, originalLocation, _movingSpeed * Time.deltaTime);
  //      }
  //
  //  }
}


