using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public PlayerController Player;
    public Vector2 RespawnPoint;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        SetPlayer(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>());
        SetRespawnPoint(GameObject.FindGameObjectWithTag("Respawn").transform.position);
    }

    /// <summary>
    /// 게임매니저에 플레이어 정보 저장
    /// </summary>
    public void SetPlayer(PlayerController player)
    {
        Player = player;
    }

    /// <summary>
    /// 게임매니저에 최근 리스폰 지점 저장
    /// </summary>
    public void SetRespawnPoint(Vector2 respawnPos)
    {
        RespawnPoint = respawnPos;
    }
}
