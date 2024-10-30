using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpItem : Item
{
    [Header("Èú·® (±âº»Èú·® 1)")]
    [SerializeField] int _healAmount = 1;

    protected override void Use(PlayerController player)
    {
        player.TakeHeal(_healAmount);
    }
}
