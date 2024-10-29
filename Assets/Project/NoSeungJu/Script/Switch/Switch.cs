using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Switch : Trap
{
    [Header("여러번 스위치 입력 가능?")]
    [SerializeField] bool canManyInput;

    [Header("스위치에 반응할 기믹")]
    [SerializeField] SwichInteractable _swichInteractable;


    Vector2 posUI = new Vector2(2, 3);
    SwitchUI _switchUI;
    Coroutine _enterTriggerRoutine;

    private void Awake()
    {
        _switchUI = GetComponent<SwitchUI>();
        UnTrackingUIToPlayer();
    }

    // 트리거에 들어올 때 캐릭터와 상호작용 시작
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _enterTriggerRoutine = _enterTriggerRoutine == null ? StartCoroutine(EnterTriggerRoutine()) : _enterTriggerRoutine;
            TrackingUIToPlayer(collision);
        }
    }

    // 트리거에서 나갈 때 캐릭터와 상호작용을 멈춤
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_enterTriggerRoutine != null)
        {
            StopCoroutine(_enterTriggerRoutine);
            _enterTriggerRoutine = null;
            UnTrackingUIToPlayer();
        }
    }

    /// <summary>
    /// 스위치 기믹과 상호작용
    /// </summary>
    void Interact()
    {
        _swichInteractable.Interact();
    }

    /// <summary>
    /// F키 입력을 대기받는 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator EnterTriggerRoutine()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Interact();
                _disposable.UnActiveTrap();
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

    void Delete()
    {
        UnTrackingUIToPlayer();
        gameObject.SetActive(false);
    }
}
