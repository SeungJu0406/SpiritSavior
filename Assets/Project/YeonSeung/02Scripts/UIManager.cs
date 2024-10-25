using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    //죽음상태
    private bool _isDead;

    [Header ("HP Bar")]
    [SerializeField] float curHp;
    [SerializeField] float maxHp;
    [SerializeField] private Slider _hpBar;


    [Header("Live Count")]
    [SerializeField] GameObject[] lives;
    [SerializeField] public int life;
    [SerializeField] public int maxLife;


    public void GiveDamage()
    {
        curHp -= 33;
        // life -= 1;
    }

    public void GiveLife()
    {
        // HP BAR
        if(curHp >= 100)
        {
            curHp = 100;
        }
        else
        {
            curHp += 33;

        }

        // LIFE
        if(life >= maxLife)
        {
            life = maxLife;
            lives[life].SetActive (true);
        }
        else
        {
            
            lives[life].SetActive (true);
            life += 1;
        }
    }


    // 데미지 받는함수
    public void TakeDamage(int damage)
    {
        if (life >= 1)
        {
            life -= damage;

            //Destroy(lives[life].gameObject);
            lives[life].SetActive(false);
            if (life < 1)
            {
                _isDead = true;
            }
        }
    }

    // 위에 목숨 UI만
    public void LifeCount()
    {
        /* 데미지 받는거말고 그냥 하트수만 하면
        if (life < 1)
        {
            Destroy(lives[0].gameObject);
        }
        else if (life < 2)
        {
            Destroy(lives[1].gameObject);
        }
        else if (life < 3)
        {
            Destroy(lives[2].gameObject);
        } 
        */
    }


    void Start()
    {
        maxLife = lives.Length;
        life = lives.Length;
        curHp = maxHp;
    }


    void Update()
    {
        _hpBar.value = curHp / maxHp;
        //TakeDamage(1);
    }
}
