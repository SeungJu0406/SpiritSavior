using System.Collections;
using System.Collections.Generic;
using Project.ParkJunMin.Scripts;
using UnityEngine;

public class HpItem : Item
{
    [Header("���� (�⺻���� 1)")]
    [SerializeField] int _healAmount = 1;

    protected override void Use(PlayerController player)
    {
        //player.playerModel.HealPlayerEvent(_healAmount);
        for (int i = 0; i < _healAmount; i++)
        {
            player.playerModel.HealPlayerEvent();
        }
    }
}
