using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel
{
    public event Action OnPlayerDamageTaken;
    public event Action OnPlayerDied;

    public int hp;
    public int MaxHP = 3; //ÀÓ½Ã°ª

    public PlayerModel()
    {
        hp = 1;
    }

    
}

