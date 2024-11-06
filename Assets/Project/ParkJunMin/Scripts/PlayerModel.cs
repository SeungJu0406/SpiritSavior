using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel
{
    public enum Nature {Red, Blue}
    public enum Ability
    {
        None = 0,
        Tag = 1 << 0,
        Dash = 1 << 1,
        WallJump = 1 << 2,
        DoubleJump = 1 << 3
    }

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

    public event Action OnPlayerWallGrabed;
    public event Action OnPlayerWallSlided;
    public event Action OnPlayerWallJumped;
    public event Action OnPlayerLanded;
    public event Action OnPlayerWakedUp;

    public event Action<Ability> OnAbilityUnlocked;

    public bool invincibility = false;
    public Nature curNature;
    public int hp;
    public int curMaxHP = 2; //임시값
    private int _MaxHP = 3;

    public PlayerModel()
    {
        hp = curMaxHP;
    }

    public void UnlockAbilityEvent(Ability _newAbility)
    {
        OnAbilityUnlocked?.Invoke(_newAbility);
    }

    public void TagPlayerEvent()
    {
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

    public void GrabWallEvent()
    {
        OnPlayerWallGrabed?.Invoke();
    }

    public void SlideWallEvent()
    {
        OnPlayerWallSlided?.Invoke();
    }

    public void JumpWallEvent()
    {
        OnPlayerWallJumped?.Invoke();
    }

    public void LandEvent()
    {
        OnPlayerLanded?.Invoke();
    }

    public void WakeUpEvent()
    {
        OnPlayerWakedUp?.Invoke();
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

