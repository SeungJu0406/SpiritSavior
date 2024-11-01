using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpUI : BaseUI
{
    private void Start()
    {
        Manager.UI.WarpUI = gameObject;
    }
}
