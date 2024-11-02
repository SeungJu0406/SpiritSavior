using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    [SerializeField] SceneField[] _respawnScenes;
    [SerializeField] SceneLoadTrigger _loadTrigger;

    private void Start()
    {
        Manager.Game.SetRespawnPoint(transform.position);
        Manager.Game.Player.playerModel.OnPlayerSpawn += Respawn;
    }

    public void Respawn()
    {
        StartCoroutine(RespawnRoutine());
    }

    IEnumerator RespawnRoutine()
    {
        SceneLoadTrigger instance = Instantiate(_loadTrigger, Manager.Game.RespawnPoint, Quaternion.identity);
        instance.AddSceneToLoad(_respawnScenes);
        yield return null;
        instance.Delete();
    }
}
