using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Switch : Disposable
{
    [Header("������ ����ġ �Է� ����?")]
    [SerializeField] bool canManyInput;

    [Header("����ġ�� ������ ���")]
    [SerializeField] SwichInteractable _swichInteractable;


    Vector2 posUI = new Vector2(2, 3);
    SwitchUI _switchUI;
    Coroutine _enterTriggerRoutine;
    bool _isKeeping = true;
    private void Awake()
    {
        _switchUI = GetComponent<SwitchUI>();
        _swichInteractable.SetSwitch(this);
        UnTrackingUIToPlayer();
    }

    protected override void Start()
    {
        if (_isDisposable)
        {
            _isKeeping = false;
            if (SceneChanger.Instance != null)
            {
                _isKeeping = SceneChanger.Instance.CheckKeepingTrap(name);
            }
            else if (TestSceneChanger.Instance != null)
            {
                _isKeeping = TestSceneChanger.Instance.CheckKeepingTrap(name);
            }
            if (!_isKeeping)
            {
                StartCoroutine(StartRoutine());
            }
        }
    }

    WaitForSeconds _startRoutine = new WaitForSeconds(0.1f);
    IEnumerator StartRoutine()
    {
        yield return _startRoutine;
        Active();
        Destroy(gameObject);
    }

    // Ʈ���ſ� ���� �� ĳ���Ϳ� ��ȣ�ۿ� ����
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _enterTriggerRoutine = _enterTriggerRoutine == null ? StartCoroutine(EnterTriggerRoutine()) : _enterTriggerRoutine;
            TrackingUIToPlayer(collision);
        }
    }

    // Ʈ���ſ��� ���� �� ĳ���Ϳ� ��ȣ�ۿ��� ����
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_enterTriggerRoutine != null)
        {
            StopCoroutine(_enterTriggerRoutine);
            _enterTriggerRoutine = null;
            UnTrackingUIToPlayer();
        }
    }

    protected override void ProcessActive()
    {
        if (_swichInteractable != null)
        {
            _swichInteractable.Interact();
        }
    }

    /// <summary>
    /// FŰ �Է��� ���޴� �ڷ�ƾ
    /// </summary>
    /// <returns></returns>
    IEnumerator EnterTriggerRoutine()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Active();
                if(canManyInput == false)
                {
                    Delete();
                    yield break;
                }            
            }
            yield return null;
        }
    }

    void TrackingUIToPlayer(Collider2D collider)
    {
        GameObject switchUI = _switchUI.GetUI("SwitchUI");
        switchUI.SetActive(true);
        switchUI.transform.SetParent(collider.transform);
        switchUI.transform.position = new Vector2(
            collider.transform.position.x + posUI.x,
            collider.transform.position.y + posUI.y);
    }

    void UnTrackingUIToPlayer()
    {
        GameObject switchUI = _switchUI.GetUI("SwitchUI");
        if (switchUI != null)
        {
            switchUI.SetActive(false);
            switchUI.transform.SetParent(transform);
        }
    }


    public bool GetIsKeeping()
    {
        return _isKeeping;
    }

    void Delete()
    {
        UnTrackingUIToPlayer();
        gameObject.SetActive(false);
    }
}
