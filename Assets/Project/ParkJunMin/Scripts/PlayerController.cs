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
    //private LayerMask wallLayer;

    private Collider2D _playerCollider;
    private int wallLayerMask; 

    

    public SpriteRenderer renderer;
    [Header("Player Setting")]
    public float moveSpeed;        // 이동속도
    public float maxMoveSpeed;     // 이동속도의 최대값
    public float lowJumpForce;     // 낮은점프 힘
    public float highJumpForce;    // 높은점프 힘
    public float maxJumpTime;     // 최대점프 시간
    public float jumpCirticalPoint;
    //public float jumpStartSpeed;   // 점프시작 속도
    public float jumpEndSpeed;     // 점프종료 속도
    public float doubleJumpForce; // 더블 점프시 얼마나 위로 올라갈지 결정
    public float knockbackForce; // 피격시 얼마나 뒤로 밀려날 지 결정
    

    //기본 이동속도에 따라 변화되는 변수 변경x
    [HideInInspector] public float moveSpeedInAir;    // 공중에서 플레이어의 속도
    [HideInInspector] public float maxMoveSpeedInAir; // 공중에서 플레이어의 속도의 최대값

    [Header("SpeedInAir = SpeedInGround * x")]
    public float speedAdjustmentOffsetInAir; // 공중에서의 속도 = 땅에서의 속도 * 해당 변수

    [Header("Checking")]
    public Rigidbody2D rigid;
    public float hp;
    
    //public bool hasJumped = false;          //
    public float jumpChargingTime = 0f;     // 스페이스바 누른시간 체크
    public bool isDoubleJumpUsed; // 더블점프 사용 유무를 나타내는 변수
    public bool isDead = false; // 죽었는지 확인
    
    //[HideInInspector] public float lastMoveDirection = 0f;
    




    [Header("Ground & Wall Checking")]
    [SerializeField] Transform _groundCheckPoint;
    public Transform _wallCheckPoint;
    private float _wallCheckDistance = 0.01f;
    private float _wallCheckHeight = 1.2f;
    private float _groundCheckDistance = 0.2f;
    public int isPlayerRight = 1;
    public bool isGrounded = false;        // 캐릭터가 땅에 붙어있는지 체크
    public bool isWall;                  // 캐릭터가 벽에 붙어있는지 체크
    public bool isWallJumpUsed;         // 벽에서 벽점프를 사용 했는지 체크
    public float wallSlidingSpeed = 0.5f; // 중력계수 조정으로 할지 결정해야함
    public float wallJumpPower;

    private Vector2 _wallCheckBoxSize;
    //public LayerMask wallLayer; // 사용 여부 확실치 않음
    //public Vector2 wallCheckSize;
    Coroutine _wallCheckRoutine;
    Coroutine _groundCheckRoutine;




    private void Awake()
    {
        if(playerModel != null)
        {
            playerModel.curNature = PlayerModel.Nature.Red;
        }
        else
        {
            Debug.LogError("모델 생성 오류");
        }

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
        maxMoveSpeedInAir = maxMoveSpeed * speedAdjustmentOffsetInAir;
        

        if (_groundCheckPoint == null)
            _groundCheckPoint = transform.Find("BottomPivot");

        if (_wallCheckPoint == null)
            _wallCheckPoint = transform.Find("WallCheckPoint");

        if (_groundCheckRoutine == null)
            _groundCheckRoutine = StartCoroutine(CheckGroundRayRoutine());

        if (_wallCheckRoutine == null) // 작성중
            _wallCheckRoutine = StartCoroutine(CheckWallRoutine());

        _wallCheckBoxSize = new Vector2(_wallCheckDistance, _wallCheckHeight);

        //임시 체력 확인용
        hp = playerModel.hp;
    }


    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        if (rigid == null)
            Debug.LogError("rigidBody없음");
        playerView = GetComponent<PlayerView>();
        _states[(int)_curState].Enter();
        SubscribeEvents();
        wallLayerMask = LayerMask.GetMask("Wall");
    }

    void Update()
    {
        _states[(int)_curState].Update();
        TagePlayer();


            ////임시 피격 트리거
            //if (Input.GetKeyDown(KeyCode.O))
            //{
            //    playerModel.TakeDamage(1); // 임시
            //}

            ////임시 죽음 트리거
            //if (Input.GetKeyDown(KeyCode.P))
            //{
            //    playerModel.DiePlayer();
            //    Debug.Log("죽음");
            //}

            //임시 체력 확인용
            //hp = playerModel.hp;

        }

    private void FixedUpdate()
    {
        _states[(int)(_curState)].FixedUpdate();
    }


    public void ChangeState(State nextState)
    {
        _states[(int)_curState].Exit();
        _curState = nextState;
        _states[(int)_curState].Enter();
    }



    //public void MoveInAir()
    //{
    //    float moveInput = Input.GetAxisRaw("Horizontal");

    //    Vector2 targetVelocity = rigid.velocity + new Vector2(moveInput * moveSpeed*Time.deltaTime, 0);
    //    targetVelocity = Vector2.ClampMagnitude(targetVelocity, maxMoveSpeedInAir); // 속도제한
    //    rigid.velocity = targetVelocity;

    //    FlipPlayer(moveInput);

    //    //추후 개선방안을 찾아야함
    //    isWall = Physics2D.BoxCast(_wallCheckPoint.position, _wallCheckBoxSize, 0, Vector2.right * isPlayerRight, _wallCheckDistance, wallLayerMask);

    //    if (isWall && _curState != State.WallJump)
    //    {
    //        if (moveInput == isPlayerRight && moveInput != 0)
    //        ChangeState(State.WallGrab);
    //    }
    //}

    //임시주석

    public void MoveInAir()
    {     // 기존 메서드

        float moveInput = Input.GetAxisRaw("Horizontal");

        rigid.velocity = new Vector2(moveInput * moveSpeedInAir, rigid.velocity.y);

        if (rigid.velocity.x > maxMoveSpeedInAir)
        {
            rigid.velocity = new Vector2(maxMoveSpeedInAir, rigid.velocity.y);
        }
        else if (rigid.velocity.x < -maxMoveSpeedInAir)
        {
            rigid.velocity = new Vector2(-(maxMoveSpeedInAir), rigid.velocity.y);
        }

        //if(_curState != State.WallJump)

        //playerView.FlipRender(moveInput);
        FlipPlayer(moveInput);

        //if(moveInput > 0 && isWall)


        //isWall = Physics2D.BoxCast(_wallCheckPoint.position, _wallCheckBoxSize, 0, Vector2.right * isPlayerRight, _wallCheckDistance, wallLayerMask);

        RaycastHit2D hit = Physics2D.BoxCast(_wallCheckPoint.position, _wallCheckBoxSize, 0, Vector2.right * isPlayerRight, _wallCheckDistance);

        if(hit.collider == null)
            return;

        if ((wallLayerMask & (1 << hit.collider.gameObject.layer)) != 0) //비트연산으로 레이어 일치 여부 확인 (제일 빠를것)
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

        //if (isWall && _curState != State.WallJump)
        //{
        //    if (moveInput == isPlayerRight && moveInput != 0)
        //        ChangeState(State.WallGrab);
        //}

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
            playerView.ChangeSprite(); // 상시 애니메이션 재생 상태라 없어도 무방
            playerModel.TagPlayerEvent(); // 속성 열거형 형식의 curNature를 바꿔줌 + 태그 이벤트 Invoke
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
    /// 플레이어 초기화 및 스폰 작업
    /// </summary>
    public void HandlePlayerSpawn()
    {
        ChangeState(State.Spawn);
        // _playerUI.SetHp(playerModel.hp); // 일단 주석처리, 순서상의 문제로 플레이어에서 해야할수도 있음
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

        if (_groundCheckRoutine != null)
            StopCoroutine(_groundCheckRoutine);

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
            Debug.DrawRay(_groundCheckPoint.position, Vector2.down * _groundCheckDistance, Color.green);
            isGrounded = Physics2D.Raycast(_groundCheckPoint.position, Vector2.down, _groundCheckDistance); //_rayPoint.up * -1
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

        //BoxCast를 통해 벽을 체크한 범위를 보여줌
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


    // 레이어 땅 체크
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


