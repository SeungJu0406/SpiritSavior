using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 무조건 ProcessActive에서 로직을 작성한 후, Active 함수로 실행시킬 것
/// </summary>
public abstract class Disposable : MonoBehaviour
{

    [Header("게임에서 일회용인가?")]
    [SerializeField] protected bool _isDisposable;

    protected virtual void Start()
    {
        int count = Manager.Game.AddInstanceDisposableDic(gameObject.scene.name, gameObject);
        gameObject.name = $"{gameObject.scene.name} ".GetText().Append(name).Append($" {count}").ToString();

        if (_isDisposable)
        {
            bool keeping = false;
            if (SceneChanger.Instance != null)
            {
                keeping = SceneChanger.Instance.CheckKeepingTrap(name);
            }
            else if (TestSceneChanger.Instance != null)
            {
                keeping = TestSceneChanger.Instance.CheckKeepingTrap(name);
            }
            if (!keeping)
            {
                Destroy(gameObject);
            }
        }
    }

    /// <summary>
    /// 여기서 로직을 작성하고 Active로 호출할 것
    /// </summary>
    protected abstract void ProcessActive();

    protected void Active()
    {
        ProcessActive();
        if (_isDisposable)
            UnActiveFromGame();
    }

    protected void UnActive()
    {
        ProcessUnActive();
    }

    protected virtual void ProcessUnActive() { }
    /// <summary>
    /// 일회용 bool 값 세팅
    /// </summary>
    /// <param name="value"></param>
    public void SetIsDisposable(bool value)
    {
        _isDisposable = value;
    }

    /// <summary>
    /// 게임에서 삭제 X
    /// </summary>
    public void ActiveFromGame()
    {
        if (SceneChanger.Instance != null)
        {
            SceneChanger.Instance.SetKeepingTrap(name, true);
        }
        else if(TestSceneChanger.Instance != null)
        {
            TestSceneChanger.Instance.SetKeepingTrap(name, true);
        }
        
    }

    /// <summary>
    /// 게임에서 삭제
    /// </summary>
    public void UnActiveFromGame()
    {
        if (SceneChanger.Instance != null)
        {
            SceneChanger.Instance.SetKeepingTrap(name, false);
        }
        else if (TestSceneChanger.Instance != null)
        {
            TestSceneChanger.Instance.SetKeepingTrap(name, false);
        }
    }
    protected virtual void OnCollisionEnter2D(Collision2D collision) { }
    protected virtual void OnTriggerEnter2D(Collider2D collision) { }

}