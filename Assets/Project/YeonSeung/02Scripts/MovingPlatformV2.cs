using System.Collections;
using System.Collections.Generic;
using Project.ParkJunMin.Scripts.States.Switch;
using UnityEngine;

public class MovingPlatformV2 : SwichInteractable
{
    [Header("�������(�ڵ��̵�)\n��Ȱ����: �ö�Ÿ�߿�����")]
    [SerializeField] bool _isPatrol;
    
    [Header("������")]
    public Transform pointA;
    [Header("������")]
    public Transform pointB;
    [Header("�̵��ӵ�")]
    [SerializeField] float moveSpeed;
    [Header("��ٸ��� �ð�")]
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
        // _isMoving�� Ȱ��ȭ �ȵǸ� �Ƹ� �ȵɰ�
        // ������ switch�ǳ� Ȯ���� �ߴ���.
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
        // ��Ʈ�� ��Ȱ���� 
        if (_isPatrol == false)
        {
            if (_isMoving == true)
            {
                // _isMoving�� Ȱ��ȭ���� �����̱�
                if (_delayMove == null)
                {
                
                
                    MovePlatform();
                }
                else if (_delayMove != null)
                {
                    //Debug.Log("�ڷ�ƾ��");
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

        // ��Ʈ�� Ȱ����
        if(_isPatrol)
        {
            Patrol();
        }

    }
    IEnumerator DelayMove()
    {
       // Debug.Log("Coroutine STARTS!");
        yield return new WaitForSeconds(delay);
       
       // Debug.Log($"{delay}�� ����");
        // ���⼭ Ȱ��ȭ �ؼ� MovePlatform();
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
    /// �÷��̾� �����̴� �޼���
    /// </summary>
    public void MovePlatform()
    {
        // Debug.Log("start moving");
        transform.position = Vector3.MoveTowards(transform.position, nextPosition, moveSpeed * Time.deltaTime);
    }
    /// <summary>
    /// ó�� ��ġ�� ���ư��� �޼���
    /// </summary>
    public void RetreatPlatform()
    {
        // A(������)�� ����
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
            // �������� isMoving��Ȱ��ȭ
            _isMoving = false;
           // Debug.Log($"isMoving {_isMoving}");
        }
    }
}
