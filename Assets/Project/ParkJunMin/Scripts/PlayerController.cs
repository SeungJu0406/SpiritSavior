using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public partial class PlayerController : MonoBehaviour
{
    public enum State {Idle, Run, Jump, DoubleJump, Fall, WallGrab, WallSliding, WallJump, Damaged, WakeUp, Dead, Spawn, Size}
    [SerializeField] State _curState;
    private BaseState[] _states = new BaseState[(int)State.Size];

    public PlayerModel playerModel = new PlayerModel();
    public PlayerView playerView;

    private Collider2D _playerCollider;
    private int groundLayerMask;
    private int wallLayerMask; 

    

    public SpriteRenderer renderer;
    [Header("Player Setting")]
    public float moveSpeed;        // 이동속도
    //public float maxMoveSpeed;     // 이동속도의 최대값
    public float lowJumpForce;     // 낮은점프 힘
    public float highJumpForce;    // 높은점프 힘
    public float maxJumpTime;     // 최대점프 시간
    public float slopeJumpBoost; // 경사면에서의 추가 점프 오프셋 값
    public float jumpCirticalPoint;
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
    private bool _isStuck; // 벽에 끼었는지 확인
    public bool isDead = false; // 죽었는지 확인
    
    [Header("Ground & Slope & Wall Checking")]
    [SerializeField] Transform _groundCheckPoint;
    public Transform _wallCheckPoint;
    private float _wallCheckDistance = 0.01f;
    private float _wallCheckHeight = 1.2f;

    [SerializeField] private float _groundCheckDistance;
    public float groundAngle;
    public Vector2 perpAngle;
    public bool isSlope;
    public float maxAngle; // 이동 가능한 최대 각도

    //[HideInInspector] public float maxFlightTime; // 점프 후 바로 fall 상태로 들어가지 않기 위한 변수

    public int isPlayerRight = 1;
    public bool isGrounded;        // 캐릭터가 땅에 붙어있는지 체크

    public RaycastHit2D groundHit;


    //public bool isWall;                  // 캐릭터가 벽에 붙어있는지 체크
    public bool isWallJumpUsed;         // 벽에서 벽점프를 사용 했는지 체크
    public float wallSlidingSpeed = 0.5f; // 중력계수 조정으로 할지 결정해야함
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
            Debug.LogError("모델 생성 오류");
        }

        rigid = GetComponent<Rigidbody2D>();
        if (rigid == null)
            Debug.LogError("rigidBody없음");
        _playerCollider = GetComponent<CapsuleCollider2D>();
        

        _states[(int)State.Idle] = new IdleState(this);
        _states[(int)State.Run] = new RunState(this);
        _states[(int)State.Jump] = new JumpState(this);
        _states[(int)State.DoubleJump] = new DoubleJumpState(this);
        _states[(int)State.Fall] = new FallState(this);
        _states[(int)State.WallGrab] = new WallGrabState(this);
        _states[(int)State.WallSliding] = new WallSlidingState(this);
        _states[(int)State.WallJump] = new WallJumpState(this);
        _states[(int)State.Damaged] = new DamagedState(this);
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

        if (_wallCheckRoutine == null) // 작성중
            _wallCheckRoutine = StartCoroutine(CheckWallDisplayRoutine());

        _wallCheckBoxSize = new Vector2(_wallCheckDistance, _wallCheckHeight);

        //임시 체력 확인용
        hp = playerModel.hp;
    }


    void Start()
    {
        playerView = GetComponent<PlayerView>();
        _curState = State.Spawn;
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



        //// 미끄러짐 방지1
        //if (player.moveInput == 0)
        //{
        //    player.rigid.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePosition;
        //}
        //else
        //{
        //    player.rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
        //}
        // 미끄럼 방지2
        //if(moveInput == 0)
        //{
        //    rigid.velocity = new Vector2(0,rigid.velocity.y);
        //}

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
        //여기서 바닥체크를 하니까 하나는 해결됨..
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
        //노멀벡터로 각도를 구함
        isGrounded = groundHit;
        // Vector2.Perpendicular(Vector2 A) : A의 값에서 반시계 방향으로 90도 회전한 벡터값을 반환

        if(isGrounded)
        {
            perpAngle = Vector2.Perpendicular(groundHit.normal).normalized; // 
            groundAngle = Vector2.Angle(groundHit.normal, Vector2.up);

            if(groundAngle != 0)
                isSlope = true;
            else
                isSlope = false;

            //법선벡터, 지면에서 수직
            Debug.DrawLine(groundHit.point, groundHit.point + groundHit.normal, Color.blue);

            // 법선벡터의 수직인 벡터, 경사면
            Debug.DrawLine(groundHit.point, groundHit.point + perpAngle, Color.red);

        }
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
    {     
        // 벽 끼임 방지 현상을 위해
        // 마찰력을 0으로 두는곳과 원래대로 돌리는곳을 정확히 정할 필요가 있음
        //rigid.sharedMaterial.friction = 0f;

        moveInput = Input.GetAxisRaw("Horizontal");

        rigid.velocity = new Vector2(moveInput * moveSpeedInAir, rigid.velocity.y);

        FlipPlayer(moveInput);

        RaycastHit2D hit = Physics2D.BoxCast(_wallCheckPoint.position, _wallCheckBoxSize, 0, Vector2.right * isPlayerRight, _wallCheckDistance);

        if(hit.collider == null)
            return;

        if ((wallLayerMask & (1 << hit.collider.gameObject.layer)) != 0) //비트연산으로 레이어 일치 여부 확인 (제일 빠를것) // 벽타기 가능한 벽일경우
        {
            if (_curState == State.WallJump)
                return;

            if(moveInput == isPlayerRight && moveInput != 0)
                ChangeState(State.WallGrab);
        }
        else // 벽타기 불가능한 벽이었을 경우
        {
            Debug.Log($"벽에 끼임 {rigid.velocity}");
            // 벽에 끼었을 때
            
            if (moveInput != 0 && rigid.velocity.y == Vector2.zero.y)
            {
                if(moveInput == Mathf.Sign(-hit.normal.x))
                {
                    // 이래도 벽감지가 끝나면 끼어버림
                    rigid.velocity = new Vector2(0, rigid.velocity.y);
                }
            }
        }
    }

    private void CheckWall()
    {
        RaycastHit2D hit = Physics2D.BoxCast(_wallCheckPoint.position, _wallCheckBoxSize, 0, Vector2.right * isPlayerRight, _wallCheckDistance);

        if (hit.collider == null)
            return;

        if ((wallLayerMask & (1 << hit.collider.gameObject.layer)) != 0) //비트연산으로 레이어 일치 여부 확인 (제일 빠를것)
        {
            if (_curState == State.WallJump)
                return;

            if (moveInput == isPlayerRight && moveInput != 0)
                ChangeState(State.WallGrab);
        }
        else
        {
            //rigid.sharedMaterial.friction = 0f;
        }

        if (moveInput != 0 && rigid.velocity == Vector2.zero)
        {

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
            playerView.ChangeSprite(); // 상시 애니메이션 재생 상태라 없어도 무방
            playerModel.TagPlayerEvent(); // 속성 열거형 형식의 curNature를 바꿔줌 + 태그 이벤트 Invoke
        }
    }


    private void HandlePlayerDied()
    {
        //ChangeState(State.Dead);
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
        //ChangeState(State.Spawn);
        // _playerUI.SetHp(playerModel.hp); // 일단 주석처리, 순서상의 문제로 플레이어에서 해야할수도 있음
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

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

    IEnumerator CheckWallDisplayRoutine()
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

    //public void Freeze()
    //{
    //    Invoke("DelayWallJump", 0.3f);
    //}

    //public void DelayWallJump()
    //{
    //    isWallJumpUsed = false;
    //}


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


