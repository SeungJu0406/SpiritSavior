using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public partial class PlayerController : MonoBehaviour
{
    public enum State {Idle, Run, Jump, DoubleJump, Fall, WallGrab, WallSliding, WallJump, Damaged, WakeUp, Dead, Spawn, Size}
    [SerializeField] State _curState = State.Spawn;
    private BaseState[] _states = new BaseState[(int)State.Size];

    public PlayerModel playerModel = new PlayerModel();
    public PlayerView playerView;

    private Collider2D _playerCollider;
    private int groundLayerMask;
    private int wallLayerMask; 

    

    public SpriteRenderer renderer;
    [Header("Player Setting")]
    public float moveSpeed;        // �̵��ӵ�
    //public float maxMoveSpeed;     // �̵��ӵ��� �ִ밪
    public float lowJumpForce;     // �������� ��
    public float highJumpForce;    // �������� ��
    public float maxJumpTime;     // �ִ����� �ð�
    public float slopeJumpBoost; // ���鿡���� �߰� ���� ������ ��
    public float jumpCirticalPoint;
    public float doubleJumpForce; // ���� ������ �󸶳� ���� �ö��� ����
    public float knockbackForce; // �ǰݽ� �󸶳� �ڷ� �з��� �� ����

    //�⺻ �̵��ӵ��� ���� ��ȭ�Ǵ� ���� ����x
    [HideInInspector] public float moveSpeedInAir;    // ���߿��� �÷��̾��� �ӵ�
    [HideInInspector] public float maxMoveSpeedInAir; // ���߿��� �÷��̾��� �ӵ��� �ִ밪

    [Header("SpeedInAir = SpeedInGround * x")]
    public float speedAdjustmentOffsetInAir; // ���߿����� �ӵ� = �������� �ӵ� * �ش� ����

    [Header("Checking")]
    public Rigidbody2D rigid;
    public float hp;
    
    //public bool hasJumped = false;          //
    public float jumpChargingTime = 0f;     // �����̽��� �����ð� üũ
    public bool isDoubleJumpUsed; // �������� ��� ������ ��Ÿ���� ����
    public bool isDead = false; // �׾����� Ȯ��
    
    [Header("Ground & Slope & Wall Checking")]
    [SerializeField] Transform _groundCheckPoint;
    public Transform _wallCheckPoint;
    private float _wallCheckDistance = 0.01f;
    private float _wallCheckHeight = 1.2f;

    [SerializeField] private float _groundCheckDistance;
    public float groundAngle;
    public Vector2 perpAngle;
    public bool isSlope;
    public float maxAngle; // �̵� ������ �ִ� ����

    //[HideInInspector] public float maxFlightTime; // ���� �� �ٷ� fall ���·� ���� �ʱ� ���� ����

    public int isPlayerRight = 1;
    public bool isGrounded;        // ĳ���Ͱ� ���� �پ��ִ��� üũ

    public RaycastHit2D groundHit;


    //public bool isWall;                  // ĳ���Ͱ� ���� �پ��ִ��� üũ
    public bool isWallJumpUsed;         // ������ �������� ��� �ߴ��� üũ
    public float wallSlidingSpeed = 0.5f; // �߷°�� �������� ���� �����ؾ���
    public float wallJumpPower;

    private Vector2 _wallCheckBoxSize;
    Coroutine _wallCheckRoutine;
    //Coroutine _groundCheckRoutine;
    public float moveInput;

    private void Awake()
    {
        if(playerModel != null)
        {
            playerModel.curNature = PlayerModel.Nature.Red;
        }
        else
        {
            Debug.LogError("�� ���� ����");
        }

        rigid = GetComponent<Rigidbody2D>();
        if (rigid == null)
            Debug.LogError("rigidBody����");
        _playerCollider = GetComponent<CapsuleCollider2D>();
        

        _states[(int)State.Idle] = new IdleState(this);
        _states[(int)State.Run] = new RunState(this);
        _states[(int)State.Jump] = new JumpState(this);
        _states[(int)State.DoubleJump] = new DoubleJumpState(this);
        _states[(int)State.Fall] = new FallState(this);
        _states[(int)State.WallGrab] = new WallGrabState(this);
        _states[(int)State.WallSliding] = new WallSlidingState(this);
        _states[(int)State.WallJump] = new WallJumpState(this);
        _states[(int)State.Damaged] = new DamagedState(this, knockbackForce);
        _states[(int)State.WakeUp] = new WakeupState(this);
        _states[(int)State.Dead] = new DeadState(this);
        _states[(int)State.Spawn] = new SpawnState(this);
        moveSpeedInAir = moveSpeed * speedAdjustmentOffsetInAir;
        //maxMoveSpeedInAir = maxMoveSpeed * speedAdjustmentOffsetInAir;


        if (_groundCheckPoint == null)
            _groundCheckPoint = transform.Find("BottomPivot");

        if (_wallCheckPoint == null)
            _wallCheckPoint = transform.Find("WallCheckPoint");

        //if (_groundCheckRoutine == null)
        //    _groundCheckRoutine = StartCoroutine(CheckGroundRayRoutine());

        if (_wallCheckRoutine == null) // �ۼ���
            _wallCheckRoutine = StartCoroutine(CheckWallRoutine());

        _wallCheckBoxSize = new Vector2(_wallCheckDistance, _wallCheckHeight);

        //�ӽ� ü�� Ȯ�ο�
        hp = playerModel.hp;
    }


    void Start()
    {
        playerView = GetComponent<PlayerView>();
        _states[(int)_curState].Enter();
        SubscribeEvents();
        wallLayerMask = LayerMask.GetMask("Wall");
        groundLayerMask = LayerMask.GetMask("Ground");
    }

    void Update()
    {
        _states[(int)_curState].Update();
        TagePlayer();

        //CheckGroundRaycast();



        //// �̲����� ����1
        //if (player.moveInput == 0)
        //{
        //    player.rigid.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePosition;
        //}
        //else
        //{
        //    player.rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
        //}
        // �̲��� ����2
        //if(moveInput == 0)
        //{
        //    rigid.velocity = new Vector2(0,rigid.velocity.y);
        //}

        ////�ӽ� �ǰ� Ʈ����
        //if (Input.GetKeyDown(KeyCode.O))
        //{
        //    playerModel.TakeDamage(1); // �ӽ�
        //}

        ////�ӽ� ���� Ʈ����
        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //    playerModel.DiePlayer();
        //    Debug.Log("����");
        //}

        //�ӽ� ü�� Ȯ�ο�
        //hp = playerModel.hp;

    }

    private void FixedUpdate()
    {
        _states[(int)(_curState)].FixedUpdate();
        //���⼭ �ٴ�üũ�� �ϴϱ� �ϳ��� �ذ��..
        CheckGroundRaycast();
    }


    public void ChangeState(State nextState)
    {
        _states[(int)_curState].Exit();
        _curState = nextState;
        _states[(int)_curState].Enter();
    }

    private void CheckGroundRaycast()
    {
        groundHit = Physics2D.Raycast(_groundCheckPoint.position, Vector2.down, _groundCheckDistance, groundLayerMask);
        //��ֺ��ͷ� ������ ����
        isGrounded = groundHit;
        // Vector2.Perpendicular(Vector2 A) : A�� ������ �ݽð� �������� 90�� ȸ���� ���Ͱ��� ��ȯ

        if(isGrounded)
        {
            perpAngle = Vector2.Perpendicular(groundHit.normal).normalized; // 
            groundAngle = Vector2.Angle(groundHit.normal, Vector2.up);

            if(groundAngle != 0)
                isSlope = true;
            else
                isSlope = false;

            //��������, ���鿡�� ����
            Debug.DrawLine(groundHit.point, groundHit.point + groundHit.normal, Color.blue);

            // ���������� ������ ����, ����
            Debug.DrawLine(groundHit.point, groundHit.point + perpAngle, Color.red);

        }
    }


    //public void MoveInAir()
    //{
    //    float moveInput = Input.GetAxisRaw("Horizontal");

    //    Vector2 targetVelocity = rigid.velocity + new Vector2(moveInput * moveSpeed*Time.deltaTime, 0);
    //    targetVelocity = Vector2.ClampMagnitude(targetVelocity, maxMoveSpeedInAir); // �ӵ�����
    //    rigid.velocity = targetVelocity;

    //    FlipPlayer(moveInput);

    //    //���� ��������� ã�ƾ���
    //    isWall = Physics2D.BoxCast(_wallCheckPoint.position, _wallCheckBoxSize, 0, Vector2.right * isPlayerRight, _wallCheckDistance, wallLayerMask);

    //    if (isWall && _curState != State.WallJump)
    //    {
    //        if (moveInput == isPlayerRight && moveInput != 0)
    //        ChangeState(State.WallGrab);
    //    }
    //}

    //�ӽ��ּ�

    public void MoveInAir()
    {     
        // �� ���� ���� ������ ����
        // �������� 0���� �δ°��� ������� �����°��� ��Ȯ�� ���� �ʿ䰡 ����
        rigid.sharedMaterial.friction = 0f;

        moveInput = Input.GetAxisRaw("Horizontal");

        rigid.velocity = new Vector2(moveInput * moveSpeedInAir, rigid.velocity.y);

        FlipPlayer(moveInput);

        RaycastHit2D hit = Physics2D.BoxCast(_wallCheckPoint.position, _wallCheckBoxSize, 0, Vector2.right * isPlayerRight, _wallCheckDistance);

        if(hit.collider == null)
            return;

        if ((wallLayerMask & (1 << hit.collider.gameObject.layer)) != 0) //��Ʈ�������� ���̾� ��ġ ���� Ȯ�� (���� ������)
        {
            if (_curState == State.WallJump)
                return;

            if(moveInput == isPlayerRight && moveInput != 0)
                ChangeState(State.WallGrab);
        }
        else
        {
            //rigid.sharedMaterial.friction = 0f;
        }
    }
    public void FlipPlayer(float _moveDirection)
    {
        playerView.FlipRender(_moveDirection);
        AdjustWallCheckPoint();
        AdjustColliderOffset();
    }

    private void AdjustWallCheckPoint()
    {
        _wallCheckPoint.localPosition = new Vector2(Mathf.Abs(_wallCheckPoint.localPosition.x) * isPlayerRight, _wallCheckPoint.localPosition.y);
    }

    private void AdjustColliderOffset()
    {
        _playerCollider.offset = new Vector2(Mathf.Abs(_playerCollider.offset.x) * isPlayerRight, _playerCollider.offset.y);
    }

    public void TagePlayer()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            playerView.ChangeSprite(); // ��� �ִϸ��̼� ��� ���¶� ��� ����
            playerModel.TagPlayerEvent(); // �Ӽ� ������ ������ curNature�� �ٲ��� + �±� �̺�Ʈ Invoke
        }
    }


    private void HandlePlayerDied()
    {
        isDead = true;
        ChangeState(State.Dead);
    }

    private void HandlePlayerDamaged()
    {
        ChangeState(State.Damaged);
    }

    /// <summary>
    /// �÷��̾� �ʱ�ȭ �� ���� �۾�
    /// </summary>
    public void HandlePlayerSpawn()
    {
        ChangeState(State.Spawn);
        // _playerUI.SetHp(playerModel.hp); // �ϴ� �ּ�ó��, �������� ������ �÷��̾�� �ؾ��Ҽ��� ����
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //NaturePlatform naturePlatform = collision.gameObject.GetComponent<NaturePlatform>();

        //if (naturePlatform != null)
        //{
        //    if(playerModel.curNature == )
        //}
    }

    private void OnDestroy()
    {
        UnsubscribeEvents();

        //if (_groundCheckRoutine != null)
        //    StopCoroutine(_groundCheckRoutine);

        if(_wallCheckRoutine != null)
            StopCoroutine(_wallCheckRoutine);
    }

    private void SubscribeEvents()
    {
        playerModel.OnPlayerDamageTaken += HandlePlayerDamaged;
        playerModel.OnPlayerDied += HandlePlayerDied;
        playerModel.OnPlayerSpawn += HandlePlayerSpawn;
    }

    private void UnsubscribeEvents()
    {
        playerModel.OnPlayerDamageTaken -= HandlePlayerDamaged;
        playerModel.OnPlayerDied -= HandlePlayerDied;
        playerModel.OnPlayerSpawn -= HandlePlayerSpawn;
    }

    IEnumerator CheckGroundRayRoutine()
    {
        WaitForSeconds delay = new WaitForSeconds(0.1f);
        while (true)
        {

            //Debug.DrawRay(_groundCheckPoint.position, Vector2.down * _groundCheckDistance, Color.green);
            //isGrounded = Physics2D.Raycast(_groundCheckPoint.position, Vector2.down, _groundCheckDistance,groundLayerMask); //_rayPoint.up * -1
            yield return delay;
        }
    }

    IEnumerator CheckWallRoutine()
    {
        WaitForSeconds delay = new WaitForSeconds(0.1f);

        //while (true)
        //{
        //    Debug.DrawRay(_wallCheckPoint.position, Vector2.right * isPlayerRight * _wallCheckDistance, Color.red);
        //    isWall = Physics2D.Raycast(_wallCheckPoint.position, Vector2.right * isPlayerRight, _wallCheckDistance, wallLayerMask);
        //    yield return delay;
        //}

        //BoxCast�� ���� ���� üũ�� ������ ������
        while (true)
        {
            Vector2 origin = _wallCheckPoint.position;
            Vector2 direction = Vector2.right * isPlayerRight;
            Vector2 offset = direction * _wallCheckDistance;

            Vector2 topLeft = origin + (Vector2.up * _wallCheckBoxSize.y / 2) + (Vector2.left * _wallCheckBoxSize.x / 2 * isPlayerRight) + offset;
            Vector2 topRight = origin + (Vector2.up * _wallCheckBoxSize.y / 2) + (Vector2.right * _wallCheckBoxSize.x / 2 * isPlayerRight) + offset;
            Vector2 bottomLeft = origin + (Vector2.down * _wallCheckBoxSize.y / 2) + (Vector2.left * _wallCheckBoxSize.x / 2 * isPlayerRight) + offset;
            Vector2 bottomRight = origin + (Vector2.down * _wallCheckBoxSize.y / 2) + (Vector2.right * _wallCheckBoxSize.x / 2 * isPlayerRight) + offset;

            Debug.DrawLine(topLeft, topRight, Color.red);
            Debug.DrawLine(topRight, bottomRight, Color.red);
            Debug.DrawLine(bottomRight, bottomLeft, Color.red);
            Debug.DrawLine(bottomLeft, topLeft, Color.red);

            //isWall = Physics2D.BoxCast(_wallCheckPoint.position, _wallCheckBoxSize, 0, Vector2.right * isPlayerRight, _wallCheckDistance, wallLayerMask);
            yield return delay;
        }

    }

    public void Freeze()
    {
        Invoke("DelayWallJump", 0.3f);
    }

    public void DelayWallJump()
    {
        isWallJumpUsed = false;
    }


    // ���̾� �� üũ
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
    //    {
    //        isGrounded = true;
    //    }
    //}

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
    //    {
    //        isGrounded = false;
    //    }
    //}
}


