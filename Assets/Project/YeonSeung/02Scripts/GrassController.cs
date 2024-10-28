using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassController : MonoBehaviour
{
    [SerializeField] ParticleSystem grassParticles;

    private ParticleSystem _grassParticlesInstance;

    private void SpawnGrassParticles()
    {
        _grassParticlesInstance = Instantiate(grassParticles, transform.position, Quaternion.identity);
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
