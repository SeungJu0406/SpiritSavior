using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// ������ ProcessActive���� ������ �ۼ��� ��, Active �Լ��� �����ų ��
/// </summary>
public abstract class Disposable : MonoBehaviour
{

    [Header("���ӿ��� ��ȸ���ΰ�?")]
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
    /// ���⼭ ������ �ۼ��ϰ� Active�� ȣ���� ��
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
    /// ��ȸ�� bool �� ����
    /// </summary>
    /// <param name="value"></param>
    public void SetIsDisposable(bool value)
    {
        _isDisposable = value;
    }

    /// <summary>
    /// ���ӿ��� ���� X
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
    /// ���ӿ��� ����
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