using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager Instance;

    [SerializeField] GameObject runFX;
    [SerializeField] GameObject jumpFX;
    [SerializeField] GameObject dJumpFX;
    [SerializeField] GameObject hitFX;

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
