using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningController : MonoBehaviour
{
    [Header("파란번개")]
    [SerializeField] GameObject LightningB;
    [Header("빨간번개")]
    [SerializeField] GameObject LightningR;
    [Header("번개 칠 위치")]
    [SerializeField] Transform hitSpot;
    [Header("번개 치는 주기")]
    [SerializeField] float hittingPeriod;
    [Header("번개 치는 주기")]
    [SerializeField] float strikeSpeed;
    [Header("번개 데미지")]
    [SerializeField] int lightningDamage;
    [Header("번개 랜덤")]
    [SerializeField] bool isRandom;

    PlayerController _player;


    [SerializeField] PlayerModel.Nature _lightingNature;

    private int _defaultLayer;
    private int _ignorePlayerLayer;
      


    private void Awake()
    {
        // Layer 추가
        _defaultLayer = gameObject.layer;
        _ignorePlayerLayer = LayerMask.NameToLayer("Ignore Player");
    }
    void Start()
    {
        if (_player != null)
        {
            return;
        }
        _player = Manager.Game.Player;
      //  _player.playerModel.OnPlayerTagged += SetActiveCollider;

    }


    void Update()
    {
        
    }


    /*
     * Tag 색상 != 번개색상, 회피가능
     * Tag 색상 == 번개색상, 피해받음
     */

    private void SetActiveCollider(PlayerModel.Nature nature)
    {
        if (nature == _lightingNature)
        {
            _player.playerModel.TakeDamageEvent(lightningDamage);

        }
    }
}
