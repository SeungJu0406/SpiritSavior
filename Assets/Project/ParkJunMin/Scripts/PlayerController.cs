using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public partial class PlayerController : MonoBehaviour
{
    public enum State {Idle, Run, Dash, Jump, DoubleJump, Fall, Land, WallGrab, WallSliding, WallJump, Damaged, WakeUp, Dead, Spawn, Size}

    [SerializeField] State _curState;
    //public State prevState;
    //private BaseState[] _states = new BaseState[(int)State.Size];
    private PlayerState[] _states = new PlayerState[(int)State.Size];

    public PlayerModel.Ability unlockedAbilities = PlayerModel.Ability.None;

    public PlayerModel playerModel = new PlayerModel();
    public PlayerView playerView;

    private Collider2D _playerCollider;
    private int groundLayerMask;
    private int wallLayerMask; 

    

    //public SpriteRenderer renderer;
    [Header("Player Setting")]
    public float moveSpeed;        // 이동속도
    //public float maxMoveSpeed;     // 이동속도의 최대값
    public float dashForce;         // 대시 힘
    [HideInInspector] public float lowJumpForce;     // 낮은점프 힘 // 폐기
    public float jumpForce;    // 높은점프 힘
    public float maxJumpTime;     // 최대점프 시간
    [HideInInspector] public float slopeJumpBoost; // 경사면에서의 추가 점프 오프셋 값 // 폐기
    [HideInInspector] public float jumpCirticalPoint; // 낮은점프, 높은점프를 가르는 시점 // 폐기
    public float doubleJumpForce; // 더블 점프시 얼마나 위로 올라갈지 결정
    public float knockbackForce; // 피격시 얼마나 뒤로 밀려날 지 결정

    //기본 이동속도에 따라 변화되는 변수 변경x
    [HideInInspector] public float moveSpeedInAir;    // 공중에서 플레이어의 속도
    [HideInInspector] public float maxMoveSpeedInAir; // 공중에서 플레이어의 속도의 최대값

    [Header("SpeedInAir = SpeedInGround * x")]
    public float speedAdjustmentOffsetInAir; // 공중에서의 속도 = 땅에서의 속도 * 해당 변수

    [Header("Checking")]
    [HideInInspector] public Rigidbody2D rigid;
    public float hp;
    
    //public bool hasJumped = false;          //
    [HideInInspector] public float jumpChargingTime = 0f;     // 스페이스바 누른시간 체크
    public bool isDoubleJumpUsed; // 더블점프 사용 유무를 나타내는 변수
    public bool isDashUsed; // 대시를 사용했는지 유무를 나타내는 변수
    public float dashCoolTime; // 대시 사용 후 쿨타임
    [HideInInspector] public float dashDeltaTime;
    public bool isStuck; // 벽에 끼었는지 확인
    public bool isDead = false; // 죽었는지 확인
    
    [Header("Ground & Slope & Wall Checking")]
    [SerializeField] Transform _groundCheckPoint;
    public Transform _wallCheckPoint;
    private float _wallCheckDistance = 0.01f;
    private float _wallCheckHeight = 2.25f; // 너무 길면 경사도 벽으로 인식함

    [SerializeField] private float _groundCheckDistance;
    [SerializeField] private float _slopeCheckDistance;
    public float groundAngle;
    public Vector2 perpAngle;
    public bool isSlope;
    public float maxAngle; // 이동 가능한 최대 각도

    //[HideInInspector] public float maxFlightTime; // 점프 후 바로 fall 상태로 들어가지 않기 위한 변수

    public int isPlayerRight = 1;
    public bool isGrounded;        // 캐릭터가 땅에 붙어있는지 체크

    public RaycastHit2D groundHit;
    public RaycastHit2D slopeHit;
    public RaycastHit2D wallHit;


    public bool isWall;                  // 캐릭터가 벽에 붙어있는지 체크
    public bool isWallJumpUsed;         // 벽에서 벽점프를 사용 했는지 체크
    public float wallSlidingSpeed = 0.5f; // 중력계수 조정으로 할지 결정해야함
    public float wallJumpPower;

    private Vector2 _wallCheckBoxSize;
    Coroutine _wallCheckRoutine;
    //Coroutine _groundCheckRoutine;

    [Header("Input")]
    public float moveInput;

    // 코요테 타임
    public float coyoteTime = 0.2f;
    [HideInInspector] public float coyoteTimeCounter;

    //점프 버퍼
    public float jumpBufferTime = 0.2f;
    [HideInInspector] public float jumpBufferCounter;
    //[HideInInspector]
    //[HideInInspector]

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
        _states[(int)State.Dash] = new DashState(this);
        _states[(int)State.Jump] = new JumpState(this);
        _states[(int)State.DoubleJump] = new DoubleJumpState(this);
        _states[(int)State.Fall] = new FallState(this);
        _states[(int)State.Land] = new LandState(this);
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
        if (Time.timeScale == 0)
            return;

        _states[(int)_curState].Update();
        TagePlayer();
        CheckDashCoolTime();

        //벽체크의 경우 fixedUpdate에서 수행하면 wallGrab 애니메이션이 자주 재생이 안된다
        //벽 체크 주기의 문제같다. Update에서 하니 문제가 사라짐
        CheckWall();
        ControlCoyoteTime();
        ControlJumpBuffer();
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

        //임시 피격 트리거
        if (Input.GetKeyDown(KeyCode.O))
        {
            playerModel.TakeDamageEvent(1); // 임시
        }

        ////임시 죽음 트리거
        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //    playerModel.DiePlayer();
        //    Debug.Log("죽음");
        //}

        ////임시 능력 해금 트리거
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UnlockAbility(PlayerModel.Ability.Tag);
            //Debug.Log("태그 해금");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            UnlockAbility(PlayerModel.Ability.Dash);
            //Debug.Log("대시 해금");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            UnlockAbility(PlayerModel.Ability.WallJump);
            //Debug.Log("벽점프 해금");
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            UnlockAbility(PlayerModel.Ability.DoubleJump);
           // Debug.Log("더블점프 해금");
        }



        //임시 체력 확인용
        //hp = playerModel.hp;

    }

    private void FixedUpdate()
    {
        if (Time.timeScale == 0) 
            return;
        _states[(int)(_curState)].FixedUpdate();
        //여기서 바닥체크를 하니까 하나는 해결됨..
        CheckGroundRaycast();
        //CheckWall();
    }

    public void CheckDashCoolTime()
    {
        if (!isDashUsed)
            return;

        // 대쉬를 쓰고 쿨타임만큼 지난경우
        if(dashDeltaTime >= dashCoolTime)
        {
            isDashUsed = false;
            //dashDeltaTime을 0으로 초기화해주는건 대시진입시 해줌
        }
        else
        {
            dashDeltaTime += Time.deltaTime;
        }
    }

    public void ChangeState(State nextState)
    {
        // 어빌리티가 해금됐는지 확인하는 과정

        ////방안1.
        //if (_states[(int)nextState].ability != Ability.None)
        //{
        //    if (HasAbility(_states[(int)nextState].ability))
        //    {
        //        _states[(int)_curState].Exit();
        //        _curState = nextState;
        //        _states[(int)_curState].Enter();
        //    }
        //    else
        //    {
        //        Debug.Log("아직 해금하지 않은 능력");
        //    }
        //}
        //else
        //{
        //    _states[(int)_curState].Exit();
        //    _curState = nextState;
        //    _states[(int)_curState].Enter();
        //}

        // 더블점프의 예외사항 처리
        if(_curState == State.WallJump && nextState == State.DoubleJump)
        {
            _states[(int)_curState].Exit();
            _curState = nextState;
            _states[(int)_curState].Enter();
        }


        //방안2. 중복 코드를 줄임
        if (_states[(int)nextState].ability == PlayerModel.Ability.None || HasAbility(_states[(int)nextState].ability))
        {
            _states[(int)_curState].Exit();
            _curState = nextState;
            _states[(int)_curState].Enter();
        }
        else
        {
            //Debug.Log("아직 해금하지 않은 능력");
        }
        
    }

    private void CheckGroundRaycast()
    {
        // 땅 체크와 땅이 평지인지 경사면인지 체크하는 메서드

        groundHit = Physics2D.Raycast(_groundCheckPoint.position, Vector2.down, _groundCheckDistance, groundLayerMask);
        slopeHit = Physics2D.Raycast(_groundCheckPoint.position, Vector2.down, _slopeCheckDistance, groundLayerMask);
        //노멀벡터로 각도를 구함
        isGrounded = groundHit;
        // Vector2.Perpendicular(Vector2 A) : A의 값에서 반시계 방향으로 90도 회전한 벡터값을 반환

        if(isGrounded)
        {
            //if (rigid.sharedMaterial.friction != 0.6f)
            //    rigid.sharedMaterial.friction = 0.6f;

            perpAngle = Vector2.Perpendicular(groundHit.normal).normalized; // 
            groundAngle = Vector2.Angle(groundHit.normal, Vector2.up);

            if(groundAngle != 0)
                isSlope = true;
            else
                isSlope = false;


            if(groundAngle > maxAngle)
            {
                Debug.Log(groundAngle);
                moveInput = 0;
            }
            else
            {
                //Debug.Log(groundAngle);
            }


            //법선벡터, 지면에서 수직
            Debug.DrawLine(groundHit.point, groundHit.point + groundHit.normal, Color.blue);

            // 법선벡터의 수직인 벡터, 경사면
            Debug.DrawLine(groundHit.point, groundHit.point + perpAngle, Color.red);

        }
    }
    private void CheckWall()
    {
        wallHit = Physics2D.BoxCast(_wallCheckPoint.position, _wallCheckBoxSize, 0, Vector2.right * isPlayerRight, _wallCheckDistance);
        isWall = wallHit;

        if (wallHit.collider == null)
            return;

        // 트리거였을시 return
        if (wallHit.collider.isTrigger)
            return;

        if (HasAbility(PlayerModel.Ability.WallJump) && (wallLayerMask & (1 << wallHit.collider.gameObject.layer)) != 0) //비트연산으로 레이어 일치 여부 확인 (제일 빠를것) // 벽타기 가능한 벽일 경우
        {
            if (isGrounded || _curState == State.WallJump || _curState == State.WallGrab || _curState == State.WallSliding ) //너무 긴데
                return;

            if (moveInput == isPlayerRight && moveInput != 0) //&& _curState != State.WallGrab && _curState != State.WallSliding)
                ChangeState(State.WallGrab);
        }
        else // 벽타기 불가능한 벽이었을 경우
        {
            float wallAngle = Vector2.Angle(Vector2.up, wallHit.normal);
            if (wallAngle > maxAngle)
            {
                Vector2 slideDirection = Vector2.Perpendicular(wallHit.normal).normalized;
                //Debug.Log("a");
                //rigid.velocity = new Vector2(0, rigid.velocity.y);
                rigid.velocity = new Vector2(slideDirection.x * rigid.velocity.x, rigid.velocity.y);

            }
            //if(rigid.sharedMaterial.friction != 0)
            //    rigid.sharedMaterial.friction = 0f;


            //float slopeAngle = Vector2.Angle(Vector2.up, wallHit.normal); // 벽의 법선 벡터와 수직 벡터의 각도
            //if (slopeAngle > 45f) // 예를 들어, 45도 이상의 경사면
            //{
            //    // 경사면일 경우 점프를 무시하거나 적절한 처리를 합니다.
            //    if (rigid.velocity.y > 0) // 현재 플레이어가 위로 점프하고 있다면
            //    {
            //        rigid.velocity = new Vector2(rigid.velocity.x, 0); // y축 속도를 0으로 설정하여 점프를 멈추게 함
            //    }
            //}

            ////Debug.Log($"벽에 끼임 {rigid.velocity}");
            //// 벽에 끼었을 때

            //if (moveInput != 0 && rigid.velocity.y == Vector2.zero.y)
            //{
            //    if (moveInput == Mathf.Sign(-wallHit.normal.x))
            //    {
            //        Debug.Log("aa");
            //        // 이래도 벽감지가 끝나면 끼어버림
            //        Vector2 pushBack = new Vector2(wallHit.normal.x * 0.1f, 0f);
            //        rigid.position += pushBack;
            //        moveInput = 0; // 플레이어 입력 무시
            //        //rigid.velocity = new Vector2(0, -5.0f);  //rigid.velocity.y*2.0f);
            //        // 너무 무식한 방법인데 다른방법이 없을까
            //    }
            //}
        }
    }

    public void MoveInAir()
    {
        // 벽 끼임 방지 현상을 위해
        // 마찰력을 0으로 두는곳과 원래대로 돌리는곳을 정확히 정할 필요가 있음
        //rigid.sharedMaterial.friction = 0f;
        if (!isStuck)
        {
            moveInput = Input.GetAxisRaw("Horizontal");
        }
        

        rigid.velocity = new Vector2(moveInput * moveSpeedInAir, rigid.velocity.y);

        FlipPlayer(moveInput);

        //RaycastHit2D hit = Physics2D.BoxCast(_wallCheckPoint.position, _wallCheckBoxSize, 0, Vector2.right * isPlayerRight, _wallCheckDistance);
        //CheckWall();
        //Dash 상태로 전환
        CheckDashable();
    }

    private void ControlCoyoteTime()
    {
        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
    }

    private void ControlJumpBuffer()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }
    }

    public void UnlockAbility(PlayerModel.Ability ability)
    {
        if(HasAbility(ability))
        {
            Debug.Log("이미 해금된 능력입니다.");
            return;
        }

        unlockedAbilities |= ability;
        playerModel.UnlockAbilityEvent(ability);
        Debug.Log($"{ability} 해금");
    }

    public bool HasAbility(PlayerModel.Ability ability)
    {
        return (unlockedAbilities & ability) == ability;
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
        if(Input.GetKeyDown(KeyCode.Z))
        {
            if(HasAbility(PlayerModel.Ability.Tag))
            {
                // playerView.ChangeSprite(); // 상시 애니메이션 재생 상태라 없어도 무방
                playerModel.TagPlayerEvent(); // 속성 열거형 형식의 curNature를 바꿔줌 + 태그 이벤트 Invoke
            }
            else
            {
                Debug.Log("태그 능력 해금 안됨");
            }
        }
    }

    public void CheckDashable()
    {
        //Dash 상태로 전환
        if (moveInput != 0)
        {
            if (isDashUsed && Input.GetKeyDown(KeyCode.X))
            {
                Debug.Log("대시 쿨타임중입니다.");
            }
            else if (!isDashUsed && Input.GetKeyDown(KeyCode.X))
            {
                ChangeState(State.Dash);
            }
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
        //playerModel.OnAbilityUnlocked += 
    }

    private void UnsubscribeEvents()
    {
        playerModel.OnPlayerDamageTaken -= HandlePlayerDamaged;
        playerModel.OnPlayerDied -= HandlePlayerDied;
        playerModel.OnPlayerSpawn -= HandlePlayerSpawn;
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

    //IEnumerator CheckGroundRayRoutine()
    //{
    //    WaitForSeconds delay = new WaitForSeconds(0.1f);
    //    while (true)
    //    {

    //        //Debug.DrawRay(_groundCheckPoint.position, Vector2.down * _groundCheckDistance, Color.green);
    //        //isGrounded = Physics2D.Raycast(_groundCheckPoint.position, Vector2.down, _groundCheckDistance,groundLayerMask); //_rayPoint.up * -1
    //        yield return delay;
    //    }
    //}
}


