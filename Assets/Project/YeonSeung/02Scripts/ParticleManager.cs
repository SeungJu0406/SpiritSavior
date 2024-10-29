using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager Instance;


    [Header("달리기 FX ")]
    [SerializeField] GameObject runFX;
    [Header("점프 FX ")]
    [SerializeField] GameObject jumpFX;
    [Header("이단 점프 FX ")]
    [SerializeField] GameObject dJumpFX;
    [Header("피격 FX ")]
    [SerializeField] GameObject hitFX;
    [Header("잔디 밟는 FX ")]
    [SerializeField] GameObject GrassFX;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }


    public void PlayRunFX()
    {
        ObjectPool.SpawnObject(runFX, transform.position, transform.rotation, ObjectPool.PoolType.ParticleSystem);
    }
    public void PlayJumpFX()
    {
        ObjectPool.SpawnObject(runFX, transform.position, transform.rotation, ObjectPool.PoolType.ParticleSystem);
    }
    public void PlayDoubleJumpFX()
    {
        ObjectPool.SpawnObject(runFX, transform.position, transform.rotation, ObjectPool.PoolType.ParticleSystem);
    }
    public void PlayHitFX()
    {
        ObjectPool.SpawnObject(runFX, transform.position, transform.rotation, ObjectPool.PoolType.ParticleSystem);
    }


    void Start()
    {
        
    }


    void Update()
    {
        
    }
}
