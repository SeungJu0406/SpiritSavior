using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassController : MonoBehaviour
{
   // [SerializeField] GameObject grassParticles;
    // ParticleManager particleManager;
    PlayerController _player;

    // private ParticleSystem _grassParticlesInstance;

    private void SpawnGrassParticles(Vector2 pos)
    {
       // ObjectPool.SpawnObject(grassParticles, transform.position, transform.rotation, ObjectPool.PoolType.ParticleSystem);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ParticleManager.Instance.PlayGrassFX();

           //  ObjectPool.SpawnObject(grassParticles, collision.transform.position, transform.rotation, ObjectPool.PoolType.ParticleSystem);
            // SpawnGrassParticles(collision.transform.position);
        }
    }
    void Start()
    {
        _player = Manager.Game.Player;
     //   grassParticles = GameObject.Find("GrassFX") as GameObject;
        
    }


    void Update()
    {
        
    }
}
