using System.Collections;
using UnityEngine;

public class TriggerTrap : Disposable
{
    [Header("떨어지는 포인트")]
    [SerializeField] Transform _fallingPoint;

    [Header("떨어지는 물체")]
    [SerializeField] FallingTrapObject _fallingTrapObject;
    [SerializeField] int _damage = 1;

    [Header("물체 라이프 타임")]
    [SerializeField] float _lifeTime;

    [Header("재발동 시간")]
    [SerializeField] float _restartTime;

    WaitForSeconds _lifeTimeDelay;
    WaitForSeconds _restartTimeDelay;
    bool _canActive = true;

    private void Awake()
    {
        _lifeTimeDelay = new WaitForSeconds(_lifeTime);
        _restartTimeDelay = new WaitForSeconds(_restartTime);
    }

    protected override void Start()
    {
        base.Start();      
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if(collision.gameObject.tag == "Player")
        {
            Active();
        }
    }

    protected override void ProcessActive()
    {
        ActiveTrap();
    }

    void ActiveTrap()
    {
        if (_canActive)
        {
            Manager.Sound.PlaySFX(Manager.Sound.Data.TrapActivationSound);

            FallingTrapObject fallingTrapObject = Instantiate(_fallingTrapObject, _fallingPoint.position, _fallingPoint.rotation);
            fallingTrapObject.SetLifeTimeDelay(_lifeTimeDelay);
            fallingTrapObject.SetDamage(_damage);
            StartCoroutine(RestartRoutine());
        }
    }

    IEnumerator RestartRoutine()
    {
        _canActive = false;
        yield return _restartTimeDelay;      
        _canActive = true;
    }
}