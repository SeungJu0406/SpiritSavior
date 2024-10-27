using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaturePlatform : MonoBehaviour
{
    [Space(10)]
    [SerializeField] PlayerModel.Nature _platformNature;

    int _defaultLayer;
    int _ignorePlayerLayer;

    private void Awake()
    {
        InitLayer();
    }

    private void Start()
    { 

        PlayerController player = Manager.Game.Player;
        player.playerModel.OnPlayerTagged += SetActiveCollider;
    }

    private void SetActiveCollider(PlayerModel.Nature nature)
    {
        if(nature == _platformNature)
        {
            gameObject.layer = _defaultLayer;
        }
        else
        {
            gameObject.layer = _ignorePlayerLayer;
        }
    }

    void InitLayer()
    {
        _defaultLayer = LayerMask.NameToLayer("Default");
        _ignorePlayerLayer = LayerMask.NameToLayer("Ignore Player");
    }
}
