using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class StageUI : BaseUI
{
    StringBuilder sb = new StringBuilder();
    private void Start()
    {
        SubscribesEvents();
        Init();
    }


    private void UpdateStage(int stage,bool isClear)
    {
        sb.Clear();
        sb.Append($"{stage}StageUnClear");
        GameObject unClearIcon = GetUI(sb.ToString());
        sb.Clear();
        sb.Append($"{stage}StageClear");
        GameObject clearIcon = GetUI(sb.ToString());

        if (isClear)
        {
            unClearIcon.SetActive(false);
            clearIcon.SetActive(true);
        }
        else
        {
            unClearIcon.SetActive(true);
            clearIcon.SetActive(false);
        }
    }

    private void Init()
    {
        GetUI("1StageUnClear").SetActive(true);
        GetUI("1StageClear").SetActive(false);

        GetUI("2StageUnClear").SetActive(true);
        GetUI("2StageClear").SetActive(false);

        GetUI("3StageUnClear").SetActive(true);
        GetUI("3StageClear").SetActive(false);

        GetUI("4StageUnClear").SetActive(true);
        GetUI("4StageClear").SetActive(false);
    }

    private void SubscribesEvents()
    {
        Manager.Game.OnChangeIsClearStage += UpdateStage;
    }

}
