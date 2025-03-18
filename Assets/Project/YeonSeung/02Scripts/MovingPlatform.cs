using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : Disposable
{
    [SerializeField] GameObject _movingPlatform;
    PolygonCollider2D _movingPlatformCollider;

    // [SerializeField] float _movingTime; // �ð����� �����̴� ������� ���� ���Եȴٸ�
    [Header("�����Ӱ��� \t �ӵ�: ")]
    [SerializeField] float _movingSpeed;
    [Header("������ �Ÿ�: ")]
    [SerializeField] float _movingDistance;
    
    [Header("�¿� = ��ĭ / ���Ʒ� = üũ")]
    [SerializeField] bool _fourWay;
    [Header("���� = ��ĭ / ������ = üũ")]
    [SerializeField] bool _horizontal; // F = ����(-) T = ������(+)
    [Header("�Ʒ� = ��ĭ / �� = üũ")]
    [SerializeField] bool _vertical; // F = ����(-) T = ������(+)

    
    private float _movingDistanceX;
    private float _movingDistanceY;

    private float _movingDirectionX;
    private float _movingDirectionY;



    Vector2 targetY;
    Vector2 targetX;
    private void Awake()
    {
        //_movingPlatform = GetComponent<GameObject>();
        _movingPlatformCollider = GetComponent<PolygonCollider2D>();


        // �¿� ������ Check
        if (_horizontal == false)
        {
            _movingDirectionX = -1;
        }
        else if(_horizontal == true) 
        {
            _movingDirectionX = 1;
        }
        // ���Ʒ� ������ Check
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
    }


    void Update()
    {
        if (_fourWay == false)
        {
            _movingPlatform.transform.position = Vector2.MoveTowards(transform.position, targetX, _movingSpeed * Time.deltaTime);
        }
        else if (_fourWay == true)
        {
            _movingPlatform.transform.position = Vector2.MoveTowards(transform.position, targetY, _movingSpeed * Time.deltaTime);
        }
        
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


    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);

        if(collision.gameObject.tag == "Player")
        {
            // ������(�浹�����Ǹ�) �θ� �Ͽ� �ּ� �̵� ����.
            collision.transform.SetParent(transform);

            // ��� 1���Ŀ� �������� ���߿� �� ���� ���� ���Ǵø��Եǰų� �ϸ�
            // Destroy(gameObject,1f);
        }
    }
    protected void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            // ���˳����� �ڽ�����
            collision.transform.SetParent(null);
        }

    }

    protected override void ProcessActive()
    {
        throw new System.NotImplementedException();
    }
}
