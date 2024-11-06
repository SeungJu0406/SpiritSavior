using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SwichInteractable : MonoBehaviour
{
    protected Switch _switch;

    public abstract void Interact();

    public void SetSwitch(Switch thisSwitch)
    {
        _switch = thisSwitch;
    }
}
