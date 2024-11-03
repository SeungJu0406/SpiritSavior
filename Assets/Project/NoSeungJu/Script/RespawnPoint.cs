using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    [Header("리스폰 포인트 On/Off")]
    [SerializeField] bool _isRespawnPoint;
    [SerializeField] SceneField _respawnScene;
    [Space(20)]
    [SerializeField] SceneLoadTrigger _loadTrigger;


    List<SceneField> _respawnScenes = new List<SceneField>(1);
    private void Awake()
    {
        _respawnScenes.Add(_respawnScene);
    }

    private void Start()
    {
        if (_isRespawnPoint)
        {
            Manager.Game.SetRespawnPoint(transform.position);
            Manager.Game.Player.playerModel.OnPlayerSpawn += Respawn;
        }
    }

    public void Respawn()
    {
        StartCoroutine(RespawnRoutine());
    }

    IEnumerator RespawnRoutine()
    {
        SceneLoadTrigger instance = Instantiate(_loadTrigger, Manager.Game.RespawnPoint, Quaternion.identity);
        instance.AddSceneToLoad(_respawnScenes.ToArray());
        yield return null;
        instance.Delete();
    }
}
