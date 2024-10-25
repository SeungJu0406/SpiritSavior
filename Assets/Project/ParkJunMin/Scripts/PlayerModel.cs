using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel
{
    public enum Nature {Red, Blue}
    public event Action OnPlayerChanged;
    public event Action OnPlayerDamageTaken;
    public event Action OnPlayerDied;
    public Nature curNature;
    public int hp;
    public int MaxHP = 3; //ÀÓ½Ã°ª

    public PlayerModel()
    {
        hp = 1;
        curNature = Nature.Red;
        curNature += 10;
    }
    public void TagElement()
    {
        //curNature = (Nature)(((int)curNature + 1) % (int)Nature.Size);
        //curNature = (curNature + 1) % (int)Nature.Size;
        curNature = curNature == Nature.Red ? Nature.Blue : Nature.Red;
    }

}

