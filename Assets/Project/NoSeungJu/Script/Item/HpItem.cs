using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpItem : Item
{
    protected override void Use(PlayerController player)
    {
        player.TakeDamage(3);
    }
}
