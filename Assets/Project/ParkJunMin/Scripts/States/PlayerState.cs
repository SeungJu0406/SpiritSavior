using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : BaseState
{
    public PlayerController player;
    protected PlayerModel.Nature prevNature;
    public PlayerState(PlayerController player)
    {
        this.player = player;
    }
}
