using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteractable : SwichInteractable
{
    public override void Interact()
    {
        Debug.Log("인터렉터블 상호작용");
    }
}