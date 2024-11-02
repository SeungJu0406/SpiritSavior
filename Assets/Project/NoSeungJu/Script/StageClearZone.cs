using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StageClearZone : Trap
{
    [SerializeField] int _stage;

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == Manager.Game.Player.tag)
        {
            Active();
        }
    }

    protected override void ProcessActive()
    {
        Manager.Game.SetIsClearStageDIc(_stage, true);
    }


}
