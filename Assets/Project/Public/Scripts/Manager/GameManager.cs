using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public partial class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int MaxStage = 4;
    public PlayerController Player;
    public Transform RespawnPoint;
    public Vector2 RespawnPos;


    public bool IsClear;
    public event UnityAction OnClear;
    public Dictionary<int, bool> IsClearStageDIc = new Dictionary<int, bool>();
    public event UnityAction<int> OnChangeIsClearStage;

    public Dictionary<string, bool> DisPosableDic = new Dictionary<string, bool>(40);

    public Dictionary<string, int> InstanceDisposableDic = new Dictionary<string, int>();

    bool _isPlayClearSound;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        SetPlayer(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>());
        SetRespawnPoint(GameObject.FindGameObjectWithTag("Respawn").transform);
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
    public void SetRespawnPoint(Transform respawnPoint)
    {
        RespawnPoint = respawnPoint;  
        RespawnPos = respawnPoint.position;
    }

    
    public bool GetIsClearStageDIc(int key)
    {
        return IsClearStageDIc[key];
    }

    public void SetIsClearStageDIc(int key, bool value)
    {
        if (IsClearStageDIc.ContainsKey(key))
        {
            IsClearStageDIc[key] = value;
            OnChangeIsClearStage(key);
        }
        else
        {
            IsClearStageDIc.Add(key, value);
            OnChangeIsClearStage(key);
        }

        UpdateIsClear();
    }

    public int AddInstanceDisposableDic(string key, GameObject instance)
    {
        if(InstanceDisposableDic.ContainsKey(key) == false)
        {
            InstanceDisposableDic.Add(key, 0);
        }

        InstanceDisposableDic[key]++;
        return InstanceDisposableDic[key];
    }
    public void ClearInstanceDisposableDic(string key)
    {
        if (InstanceDisposableDic.ContainsKey(key) == false)
        {
            InstanceDisposableDic.Add(key, 0);
        }
        InstanceDisposableDic[key]= 0;
    }

    private void UpdateIsClear()
    {
        if(IsClearStageDIc.Count >= MaxStage)
        {
            IsClear = true;

            if (_isPlayClearSound == false)
            {
                OnClear?.Invoke();
                _isPlayClearSound = true;
            }
        }
    }
}
