using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel
{
    public enum Nature {Red, Blue}
    public event Action<Nature> OnPlayerTagged;
    public event Action OnPlayerDamageTaken;
    public event Action OnPlayerDied;
    public Nature curNature;
    public int hp;
    public int MaxHP = 3; //임시값

    public PlayerModel()
    {
        hp = 3;
        //curNature = Nature.Red;
        curNature += 10;
    }
    public void TagPlayer()
    {
        //curNature = (Nature)(((int)curNature + 1) % (int)Nature.Size);
        //curNature = (curNature + 1) % (int)Nature.Size;
        curNature = curNature == Nature.Red ? Nature.Blue : Nature.Red;
        OnPlayerTagged?.Invoke(curNature);
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        if(hp > 0)
        {
            OnPlayerDamageTaken?.Invoke();
        }

        // 예외상황 발생 우려에 따라 일단 주석 처리
        //else
        //{
        //    DiePlayer();
        //}
        
    }

    

    public void DiePlayer()
    {
        if(OnPlayerDied != null)
            OnPlayerDied?.Invoke();
    }
    
}

