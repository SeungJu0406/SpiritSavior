using UnityEngine;

[RequireComponent(typeof(Disposable))]
public class Trap : MonoBehaviour
{
    [Header("게임에서 일회용인가?")]
    [SerializeField] protected bool _isDisposable;


    protected Disposable _disposable;

    protected virtual void Start()
    {
        _disposable = GetComponent<Disposable>();
        _disposable.SetIsDisposable(_isDisposable);

        if (_isDisposable)
        {
            bool keeping = SceneChanger.Instance.CheckKeepingTrap(transform.position);
            if (!keeping)
            {
                gameObject.SetActive(false);
            }
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (_isDisposable)
        {
            if (collision.gameObject.tag == "Player")
            {
                _disposable.UnActiveTrap();
            }
        }
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isDisposable)
        {
            if (collision.gameObject.tag == "Player")
            {
                _disposable.UnActiveTrap();
            }
        }
    }

    protected virtual void InteractTrap()
    {
        _disposable.UnActiveTrap();
    }


}
