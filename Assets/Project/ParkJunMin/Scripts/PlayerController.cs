using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public enum State {Idle, Run, Jump, Fall, Size}
    [SerializeField] State _curState = State.Idle;
    private BaseState[] _states = new BaseState[(int)State.Size];

    public PlayerModel playerModel;
    public PlayerView playerView;
    

    public SpriteRenderer renderer;

    public float moveSpeed;        // 이동속도
    public float maxMoveSpeed;
    public float lowJumpForce;     // 낮은점프 힘
    public float highJumpForce;    // 높은점프 힘
    public float maxJumpTime;     // 최대점프 시간
    public float jumpStartSpeed;   // 점프시작 속도
    public float jumpEndSpeed;     // 점프종료 속도

    //기본 이동속도에 따라 변화되는 변수 변경x
    [HideInInspector] public float moveSpeedInAir;    // 공중에서 플레이어의 속도
    [HideInInspector] public float maxMoveSpeedInAir; // 공중에서 플레이어의 속도의 최대값

    public float speedAdjustmentOffsetInAir; // 공중에서의 속도 = 땅에서의 속도 * 해당 변수
    public Rigidbody2D rigid;
    public bool isGrounded = false;        // 캐릭터와 땅여부 체크
    public bool isJumped = false;          // 점프중인지여부 체크
    public float jumpChargingTime = 0f;     // 스페이스바 누른시간 체크

    private void Awake()
    {
        playerModel = new PlayerModel();
        if(playerModel == null)
        {
            Debug.LogError("모델 생성 오류");
        }

        _states[(int)State.Idle] = new IdleState(this);
        _states[(int)State.Run] = new RunState(this);
        _states[(int)State.Jump] = new JumpState(this);
        _states[(int)State.Fall] = new FallState(this);
        moveSpeedInAir = moveSpeed * speedAdjustmentOffsetInAir;
        maxMoveSpeedInAir = maxMoveSpeed * speedAdjustmentOffsetInAir;
    }


    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        playerView = GetComponent<PlayerView>();
        

        _states[(int)_curState].Enter();
    }

    void Update()
    {
        _states[(int)_curState].Update();
        TagePlayer();
        
    }

    public void ChangeState(State nextState)
    {
        _states[(int)_curState].Exit();
        _curState = nextState;
        _states[(int)_curState].Enter();
    }



    public void MoveInAir()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rigid.velocity = new Vector2(moveInput * moveSpeedInAir, rigid.velocity.y);

        if (rigid.velocity.x > maxMoveSpeedInAir)
        {
            rigid.velocity = new Vector2(maxMoveSpeedInAir, rigid.velocity.y);
        }
        else if (rigid.velocity.x < -maxMoveSpeedInAir)
        {
            rigid.velocity = new Vector2(-(maxMoveSpeedInAir), rigid.velocity.y);
        }
        playerView.FlipRender(moveInput);
    }

    public void TagePlayer()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            playerView.ChangeSprite(); // 상시 애니메이션 재생 상태라 없어도 무방
            playerModel.TagElement(); // 속성 열거형 형식의 curNature를 바꿔줌
            //playerView.PlayAnimation((int)_curState);
        }
    }

    private void OnDestroy()
    {
       
    }

    // 레이어 땅 체크
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = false;
        }
    }
}


