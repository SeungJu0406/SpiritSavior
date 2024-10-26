using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : Trap
{
    [SerializeField] GameObject _movingPlatform;
    PolygonCollider2D _movingPlatformCollider;

    [Header("움직임관련")]
    [SerializeField] float _movingTime;
    [SerializeField] float _movingSpeed;
    [SerializeField] float _movingDistance;
    private float _movingDirection;
    [Header("왼쪽 = 빈칸 / 오른쪽 체크")]
    [SerializeField] bool _direction; // F = 왼쪽(-) T = 오른쪽(+)

    Vector2 target;
    private void Awake()
    {
        //_movingPlatform = GetComponent<GameObject>();
        _movingPlatformCollider = GetComponent<PolygonCollider2D>();
        if (_direction == false)
        {
            _movingDirection = -1;
        }
        else
        {
            _movingDirection = 1;
        }
        SetDestination();

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
        target = new Vector2(_movingDistance, transform.position.y);
        // 올라가는 발판이 필요하려나
    }
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);

        if(collision.gameObject.tag == "Player")
        {
            // 밟고 1초후에 없어지는
            // Destroy(gameObject,1f);
        }
    }

}
