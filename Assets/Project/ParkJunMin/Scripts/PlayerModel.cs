using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel
{
    public enum Nature {Red, Blue}
 
    public event Action<Nature> OnPlayerTagged;
    public event Action OnPlayerDamageTaken;
    public event Action OnPlayerHealth;
    public event Action OnPlayerMaxHpUp;

    public event Action OnPlayerJumped;
    public event Action OnPlayerRan;
    public event Action OnPlayerDoubleJumped;
    public event Action OnPlayerDashed;
    public event Action OnPlayerDied;
    public event Action OnPlayerSpawn;


    public bool invincibility = false;
    public Nature curNature;
    public int hp;
    public int curMaxHP = 2; //임시값
    private int _MaxHP = 3;

    public PlayerModel()
    {
        hp = curMaxHP;
        //curNature = Nature.Red;
        //curNature += 10;
    }
    public void TagPlayerEvent()
    {
        //curNature = (Nature)(((int)curNature + 1) % (int)Nature.Size);
        //curNature = (curNature + 1) % (int)Nature.Size;
        curNature = curNature == Nature.Red ? Nature.Blue : Nature.Red;
        OnPlayerTagged?.Invoke(curNature);
    }

    public void TakeDamageEvent(int damage)
    {
        if(!invincibility && hp > 0)
        {
            hp -= damage;
            OnPlayerDamageTaken?.Invoke();
        }
        else
        {
            Debug.Log("무적상태라 피격받지 않음");
        }
        // 예외상황 발생 우려에 따라 일단 주석 처리
        //else
        //{
        //    DiePlayer();
        //}
    }

    public void JumpPlayerEvent()
    {
        OnPlayerJumped?.Invoke();
    }

    public void DoubleJumpPlayerEvent()
    {
        OnPlayerDoubleJumped?.Invoke();
    }

    public void DashPlayerEvent()
    {
        OnPlayerDashed?.Invoke();
    }

    public void RunPlayerEvent()
    {
        OnPlayerRan?.Invoke();
    }

    

    public void HealPlayerEvent()
    {
        if (hp < curMaxHP)
            hp++;
        OnPlayerHealth?.Invoke();
    }

    public void AddMaxHPEvent()
    {
        if(curMaxHP < _MaxHP)
            curMaxHP++;
        // 최대체력 증가 아이템을 습득 시 체력이 모두 회복될 지는 추후 결정
        //hp = curMaxHP;
        OnPlayerMaxHpUp?.Invoke();
    }

    public void DiePlayerEvent()
    {
        OnPlayerDied?.Invoke();
    }

    public void SpawnPlayerEvent()
    {
        OnPlayerSpawn?.Invoke();
    }
}

