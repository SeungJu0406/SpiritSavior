using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassController : MonoBehaviour
{
    [SerializeField] GameObject grassParticles;

    // private ParticleSystem _grassParticlesInstance;

    private void SpawnGrassParticles()
    {
        ObjectPool.SpawnObject(grassParticles, transform.position, transform.rotation, ObjectPool.PoolType.ParticleSystem);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SpawnGrassParticles();
        }
    }
    void Start()
    {
        
    }


    void Update()
    {
        
    }
}
