using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public partial class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public PlayerController Player;
    public Vector2 RespawnPoint;

    public Dictionary<int, bool> IsClearStageDIc = new Dictionary<int, bool>();
    public event UnityAction<int,bool> OnChangeIsClearStage;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        SetPlayer(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>());
        GameObject respawnPoint = GameObject.FindGameObjectWithTag("Respawn");
        if (respawnPoint != null)
        {
            SetRespawnPoint(respawnPoint.transform.position);
        }
        
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

    
    public bool GetIsClearStageDIcI(int key)
    {
        return IsClearStageDIc[key];
    }

    public void SetIsClearStageDIc(int key, bool value)
    {
        if (IsClearStageDIc.ContainsKey(key)) 
        {
            IsClearStageDIc[key] = value;
            OnChangeIsClearStage(key, value);
        }
        else
        {
            IsClearStageDIc.Add(key, value);
        }
    }
}
