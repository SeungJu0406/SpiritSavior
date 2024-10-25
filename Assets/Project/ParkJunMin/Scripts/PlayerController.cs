using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum State {Idle, Run, Jump, Fall, Size}
    [SerializeField] State _curState = State.Idle;
    private BaseState[] _states = new BaseState[(int)State.Size];

    public SpriteRenderer renderer;

    [SerializeField] public float moveSpeed;        // 이동속도
    [SerializeField] public float maxMoveSpeed;
    [SerializeField] public float lowJumpForce = 10f;     // 낮은점프 힘
    [SerializeField] public float highJumpForce = 25f;    // 높은점프 힘
    [SerializeField] public float maxJumpTime = 0.2f;     // 최대점프 시간
    [SerializeField] public float jumpStartSpeed = 18f;   // 점프시작 속도
    [SerializeField] public float jumpEndSpeed = 10f;     // 점프종료 속도

    public Rigidbody2D rigid;
    public bool isGrounded = false;        // 캐릭터와 땅여부 체크
    public bool isJumped = false;          // 점프중인지여부 체크
    public float spacebarTime = 0f;     // 스페이스바 누른시간 체크

    private void Awake()
    {
        _states[(int)State.Idle] = new IdleState(this);
        _states[(int)State.Run] = new RunState(this);
        _states[(int)State.Jump] = new JumpState(this);
        _states[(int)State.Fall] = new FallState(this);
    }


    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
        _states[(int)_curState].Enter();
    }

    void Update()
    {
        _states[(int)_curState].Update();
    }

    public void ChangeState(State nextState)
    {
        _states[(int)_curState].Exit();
        _curState = nextState;
        _states[(int)_curState].Enter();
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


