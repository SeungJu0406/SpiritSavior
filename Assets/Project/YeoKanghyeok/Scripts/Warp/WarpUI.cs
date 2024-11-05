using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class WarpUI : BaseUI
{
    public GameObject _warpStageUI;

    private void Start()
    {
        Manager.UI.WarpUI = this;
    }

    public void ActiveBind()
    {
        ClearDic();
        Bind();
    }
}
