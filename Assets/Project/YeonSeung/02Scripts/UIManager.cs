using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] float curHp;
    [SerializeField] float maxHp;
    [SerializeField] private Slider _hpBar;
    void Start()
    {
        curHp = maxHp;
    }


    void Update()
    {
        _hpBar.value = curHp / maxHp;
    }
}
