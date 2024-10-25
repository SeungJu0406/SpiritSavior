using UnityEngine;

public class TriggerTrap : Trap
{
    [Header("떨어지는 포인트")]
    [SerializeField] Transform _fallingPoint;

    [Header("떨어지는 물체")]
    [SerializeField] FallingTrapObject _fallingTrapObject;

    [Header("물체 라이프 타임")]
    [SerializeField] float _lifeTime;

    protected override void Start()
    {
        base.Start();

        _fallingTrapObject.SetFallingPoint(_fallingPoint);
        _fallingTrapObject.SetLifeTimeDelay(new WaitForSeconds(_lifeTime));
    }

    
}