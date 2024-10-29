using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 무너지는 발판
/// </summary>
public class BreakPlatform : Trap
{
    [Header("발판 모델 오브젝트")]
    [SerializeField] GameObject _platform;

    [Header("돌 조각 그룹 오브젝트")]
    [SerializeField] GameObject _rockPieceGroup;

    [Header("파티클 그룹 오브젝트")]
    [SerializeField] GameObject _particleGroup;

    [Header("돌 조각 사라지는 시간")]
    [SerializeField] float _rockLifeTime;

    [Header("발판 재생성 시간")]
    [SerializeField] bool _canRespawn;
    [SerializeField] float _respawnTime;


    PolygonCollider2D _platformCollider;
    BrokenRockPiece[] _rockPieces;
    WaitForSeconds _respawnDelay;
     
    private void Awake()
    {
        _platformCollider = GetComponent<PolygonCollider2D>();   
        _respawnDelay = new WaitForSeconds(_respawnTime);
    }
    protected override void Start()
    {
        base.Start();
        InitRockPiecesLifeTime();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if (collision.gameObject.tag == "Player")
        {
            Break();
        }
    }

    void Break()
    {
        _platform.SetActive(false);
        _platformCollider.enabled =false;
        _particleGroup.SetActive(true);
        OnEnableRockPieces();

        if(_canRespawn)
            StartCoroutine(RespawnRoutine());
    }

    IEnumerator RespawnRoutine()
    {
        if(_isDisposable == true) yield break;

        yield return _respawnDelay;
        _platform.SetActive(true);
        _platformCollider.enabled = true;
        _particleGroup.SetActive(false);
        OnDiableRockPieces();
    }


    void InitRockPiecesLifeTime()
    {
        _rockPieces = _rockPieceGroup.GetComponentsInChildren<BrokenRockPiece>();
        WaitForSeconds rockLifeTime = new WaitForSeconds(_rockLifeTime);
        for (int i = 0; i < _rockPieces.Length; i++)
        {
            _rockPieces[i].SetLifeTime(rockLifeTime);
        }
        OnDiableRockPieces();
    }

    void OnEnableRockPieces()
    {
        foreach (BrokenRockPiece piece in _rockPieces) 
        {
            piece.gameObject.SetActive(true);
        }
    }

    void OnDiableRockPieces()
    {
        foreach (BrokenRockPiece piece in _rockPieces)
        {
            piece.gameObject.SetActive(false);
        }
    }
}
